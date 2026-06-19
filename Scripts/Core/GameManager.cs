using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private int score = 0;
    [SerializeField] private int playerRank = 1;
    [SerializeField] private bool isOnlineMode = false;
    [SerializeField] private bool isOfflineMode = true;

    // Coin Settings
    [SerializeField] private int coinsPerGift = 10;
    [SerializeField] private int coinsPerHeart = 5;
    [SerializeField] private int coinsPerShare = 8;
    [SerializeField] private int coinsPerComment = 3;
    [SerializeField] private int coinsPerSave = 6;
    [SerializeField] private int coinsPerOfflineTest = 20;

    // Events
    public delegate void ScoreChanged(int newScore);
    public event ScoreChanged OnScoreChanged;

    public delegate void ModeChanged(bool isOnline);
    public event ModeChanged OnModeChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadSettings();
    }

    // ===== SCORE MANAGEMENT =====
    public void AddScore(int amount)
    {
        score += amount;
        OnScoreChanged?.Invoke(score);
        Debug.Log($"Score Updated: {score}");
    }

    public int GetScore() => score;
    public int GetPlayerRank() => playerRank;

    public void UpdateRank(int newRank)
    {
        playerRank = newRank;
    }

    // ===== MODE MANAGEMENT =====
    public void SetOnlineMode(bool isOnline)
    {
        isOnlineMode = isOnline;
        isOfflineMode = !isOnline;
        OnModeChanged?.Invoke(isOnline);
        Debug.Log($"Mode Changed: {(isOnline ? "ONLINE" : "OFFLINE")}");
    }

    public bool IsOnlineMode() => isOnlineMode;
    public bool IsOfflineMode() => isOfflineMode;

    // ===== TIKTOK EVENT COIN GETTERS =====
    public int GetCoinsPerGift() => coinsPerGift;
    public int GetCoinsPerHeart() => coinsPerHeart;
    public int GetCoinsPerShare() => coinsPerShare;
    public int GetCoinsPerComment() => coinsPerComment;
    public int GetCoinsPerSave() => coinsPerSave;
    public int GetCoinsPerOfflineTest() => coinsPerOfflineTest;

    // ===== TIKTOK COIN SETTERS (for Settings Panel) =====
    public void SetCoinsPerGift(int amount) => coinsPerGift = amount;
    public void SetCoinsPerHeart(int amount) => coinsPerHeart = amount;
    public void SetCoinsPerShare(int amount) => coinsPerShare = amount;
    public void SetCoinsPerComment(int amount) => coinsPerComment = amount;
    public void SetCoinsPerSave(int amount) => coinsPerSave = amount;
    public void SetCoinsPerOfflineTest(int amount) => coinsPerOfflineTest = amount;

    // ===== SETTINGS =====
    public void SaveSettings()
    {
        PlayerPrefs.SetInt("CoinsPerGift", coinsPerGift);
        PlayerPrefs.SetInt("CoinsPerHeart", coinsPerHeart);
        PlayerPrefs.SetInt("CoinsPerShare", coinsPerShare);
        PlayerPrefs.SetInt("CoinsPerComment", coinsPerComment);
        PlayerPrefs.SetInt("CoinsPerSave", coinsPerSave);
        PlayerPrefs.SetInt("CoinsPerOfflineTest", coinsPerOfflineTest);
        PlayerPrefs.Save();
        Debug.Log("Settings Saved");
    }

    private void LoadSettings()
    {
        coinsPerGift = PlayerPrefs.GetInt("CoinsPerGift", 10);
        coinsPerHeart = PlayerPrefs.GetInt("CoinsPerHeart", 5);
        coinsPerShare = PlayerPrefs.GetInt("CoinsPerShare", 8);
        coinsPerComment = PlayerPrefs.GetInt("CoinsPerComment", 3);
        coinsPerSave = PlayerPrefs.GetInt("CoinsPerSave", 6);
        coinsPerOfflineTest = PlayerPrefs.GetInt("CoinsPerOfflineTest", 20);
    }

    public void ResetScore()
    {
        score = 0;
        OnScoreChanged?.Invoke(score);
    }
}
