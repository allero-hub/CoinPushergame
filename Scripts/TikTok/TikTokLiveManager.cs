using UnityEngine;
using System.Collections.Generic;

public class TikTokLiveManager : MonoBehaviour
{
    public static TikTokLiveManager Instance { get; private set; }

    [SerializeField] private bool isConnected = false;
    [SerializeField] private string liveStudioURL = "";
    [SerializeField] private Texture2D userProfilePlaceholder;

    // Live Event Queue
    private Queue<TikTokEvent> eventQueue = new Queue<TikTokEvent>();

    // Connected Users (for profiles)
    private Dictionary<string, UserProfile> activeUsers = new Dictionary<string, UserProfile>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    // ===== CONNECTION MANAGEMENT =====
    public void ConnectToTikTokLive(string studioURL)
    {
        liveStudioURL = studioURL;
        isConnected = true;
        GameManager.Instance.SetOnlineMode(true);

        Debug.Log($"Connected to TikTok Live: {studioURL}");
        SimulateConnection(); // For testing
    }

    public void DisconnectFromTikTokLive()
    {
        isConnected = false;
        GameManager.Instance.SetOnlineMode(false);
        activeUsers.Clear();
        eventQueue.Clear();

        Debug.Log("Disconnected from TikTok Live");
    }

    public bool IsConnected() => isConnected;

    // ===== EVENT PROCESSING =====
    public void EnqueueEvent(TikTokEvent tikTokEvent)
    {
        eventQueue.Enqueue(tikTokEvent);
    }

    public void ProcessQueuedEvents()
    {
        while (eventQueue.Count > 0)
        {
            TikTokEvent tikEvent = eventQueue.Dequeue();
            ProcessTikTokEvent(tikEvent);
        }
    }

    private void ProcessTikTokEvent(TikTokEvent tikEvent)
    {
        int coinsToAdd = 0;

        switch (tikEvent.eventType)
        {
            case TikTokEventType.Gift:
                coinsToAdd = GameManager.Instance.GetCoinsPerGift();
                break;
            case TikTokEventType.Heart:
                coinsToAdd = GameManager.Instance.GetCoinsPerHeart();
                break;
            case TikTokEventType.Share:
                coinsToAdd = GameManager.Instance.GetCoinsPerShare();
                break;
            case TikTokEventType.Comment:
                coinsToAdd = GameManager.Instance.GetCoinsPerComment();
                break;
            case TikTokEventType.Save:
                coinsToAdd = GameManager.Instance.GetCoinsPerSave();
                break;
        }

        // Register user if new
        if (!activeUsers.ContainsKey(tikEvent.userId))
        {
            activeUsers[tikEvent.userId] = new UserProfile
            {
                userId = tikEvent.userId,
                username = tikEvent.username,
                profilePicURL = tikEvent.profilePicURL,
                eventCount = 1
            };
        }
        else
        {
            activeUsers[tikEvent.userId].eventCount++;
        }

        // Spawn coins
        CoinSpawner coinSpawner = FindObjectOfType<CoinSpawner>();
        if (coinSpawner != null)
        {
            coinSpawner.SpawnCoins(coinsToAdd);
        }

        // Update UI
        UIManager.Instance?.ShowTikTokEventUI(tikEvent, coinsToAdd);

        Debug.Log($"TikTok Event: {tikEvent.username} - {tikEvent.eventType} - {coinsToAdd} coins");
    }

    // ===== USER PROFILE ACCESS =====
    public UserProfile GetUserProfile(string userId)
    {
        return activeUsers.ContainsKey(userId) ? activeUsers[userId] : null;
    }

    public List<UserProfile> GetAllActiveUsers()
    {
        return new List<UserProfile>(activeUsers.Values);
    }

    // ===== TESTING / SIMULATION =====
    private void SimulateConnection()
    {
        // For offline testing - simulate TikTok events
        Debug.Log("Simulating TikTok Live Connection...");
    }

    public void SimulateTikTokEvent(TikTokEventType eventType, string username = "TestUser")
    {
        TikTokEvent simulatedEvent = new TikTokEvent
        {
            eventType = eventType,
            userId = System.Guid.NewGuid().ToString(),
            username = username,
            profilePicURL = "",
            timestamp = System.DateTime.Now
        };

        EnqueueEvent(simulatedEvent);
        ProcessQueuedEvents();
    }
}

// ===== DATA STRUCTURES =====
public enum TikTokEventType
{
    Gift,
    Heart,
    Share,
    Comment,
    Save
}

[System.Serializable]
public class TikTokEvent
{
    public TikTokEventType eventType;
    public string userId;
    public string username;
    public string profilePicURL;
    public System.DateTime timestamp;
}

[System.Serializable]
public class UserProfile
{
    public string userId;
    public string username;
    public string profilePicURL;
    public int eventCount;
}
