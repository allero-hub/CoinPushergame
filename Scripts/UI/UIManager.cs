using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI rankText;
    [SerializeField] private TextMeshProUGUI modeText;
    [SerializeField] private CanvasGroup tikTokEventPopup;
    [SerializeField] private TextMeshProUGUI eventNotificationText;
    [SerializeField] private Image userProfileImage;
    [SerializeField] private TextMeshProUGUI usernameText;
    [SerializeField] private Button settingsButton;
    [SerializeField] private SettingsPanel settingsPanel;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        // Subscribe to events
        GameManager.Instance.OnScoreChanged += UpdateScoreUI;
        GameManager.Instance.OnModeChanged += UpdateModeUI;

        if (settingsButton != null)
            settingsButton.onClick.AddListener(OpenSettings);

        // Initial UI update
        UpdateScoreUI(GameManager.Instance.GetScore());
        UpdateModeUI(GameManager.Instance.IsOnlineMode());
    }

    // ===== SCORE & RANK UI =====
    private void UpdateScoreUI(int newScore)
    {
        if (scoreText != null)
            scoreText.text = $"Score: {newScore}";

        if (rankText != null)
            rankText.text = $"Rank: #{GameManager.Instance.GetPlayerRank()}";
    }

    private void UpdateModeUI(bool isOnline)
    {
        if (modeText != null)
            modeText.text = isOnline ? "🔴 LIVE" : "⚪ OFFLINE";
    }

    // ===== TIKTOK EVENT DISPLAY =====
    public void ShowTikTokEventUI(TikTokEvent tikEvent, int coinsAwarded)
    {
        StartCoroutine(DisplayTikTokEventPopup(tikEvent, coinsAwarded));
    }

    private IEnumerator DisplayTikTokEventPopup(TikTokEvent tikEvent, int coinsAwarded)
    {
        if (tikTokEventPopup == null) yield break;

        // Build notification text
        string eventTypeIcon = GetEventTypeIcon(tikEvent.eventType);
        string notificationMsg = $"{eventTypeIcon} {tikEvent.username}\n+{coinsAwarded} Coins";

        if (eventNotificationText != null)
            eventNotificationText.text = notificationMsg;

        if (usernameText != null)
            usernameText.text = tikEvent.username;

        // Load and display profile picture (placeholder for now)
        if (userProfileImage != null)
            userProfileImage.color = new Color(Random.value, Random.value, Random.value, 1f);

        // Fade in
        tikTokEventPopup.alpha = 0;
        for (float t = 0; t < 0.3f; t += Time.deltaTime)
        {
            tikTokEventPopup.alpha = Mathf.Lerp(0, 1, t / 0.3f);
            yield return null;
        }
        tikTokEventPopup.alpha = 1;

        // Wait
        yield return new WaitForSeconds(2f);

        // Fade out
        for (float t = 0; t < 0.3f; t += Time.deltaTime)
        {
            tikTokEventPopup.alpha = Mathf.Lerp(1, 0, t / 0.3f);
            yield return null;
        }
        tikTokEventPopup.alpha = 0;
    }

    private string GetEventTypeIcon(TikTokEventType eventType)
    {
        return eventType switch
        {
            TikTokEventType.Gift => "🎁",
            TikTokEventType.Heart => "❤️",
            TikTokEventType.Share => "📤",
            TikTokEventType.Comment => "💬",
            TikTokEventType.Save => "💾",
            _ => "⭐"
        };
    }

    // ===== SETTINGS =====
    private void OpenSettings()
    {
        if (settingsPanel != null)
            settingsPanel.gameObject.SetActive(true);
    }

    public void CloseSettings()
    {
        if (settingsPanel != null)
            settingsPanel.gameObject.SetActive(false);
    }
}
