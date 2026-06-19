using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private Toggle onlineModeToggle;
    [SerializeField] private InputField tiktokURLInput;
    [SerializeField] private Button connectButton;
    [SerializeField] private Button disconnectButton;

    // Coin Settings
    [SerializeField] private InputField coinsPerGiftInput;
    [SerializeField] private InputField coinsPerHeartInput;
    [SerializeField] private InputField coinsPerShareInput;
    [SerializeField] private InputField coinsPerCommentInput;
    [SerializeField] private InputField coinsPerSaveInput;
    [SerializeField] private InputField coinsPerOfflineTestInput;

    [SerializeField] private Button saveSettingsButton;
    [SerializeField] private Button resetScoreButton;
    [SerializeField] private Button closeButton;

    private void Start()
    {
        // Setup listeners
        onlineModeToggle.onValueChanged.AddListener(OnModeToggled);
        connectButton.onClick.AddListener(OnConnectClicked);
        disconnectButton.onClick.AddListener(OnDisconnectClicked);
        saveSettingsButton.onClick.AddListener(OnSaveSettingsClicked);
        resetScoreButton.onClick.AddListener(OnResetScoreClicked);
        closeButton.onClick.AddListener(OnCloseClicked);

        // Load current settings
        LoadSettings();
    }

    private void LoadSettings()
    {
        coinsPerGiftInput.text = GameManager.Instance.GetCoinsPerGift().ToString();
        coinsPerHeartInput.text = GameManager.Instance.GetCoinsPerHeart().ToString();
        coinsPerShareInput.text = GameManager.Instance.GetCoinsPerShare().ToString();
        coinsPerCommentInput.text = GameManager.Instance.GetCoinsPerComment().ToString();
        coinsPerSaveInput.text = GameManager.Instance.GetCoinsPerSave().ToString();
        coinsPerOfflineTestInput.text = GameManager.Instance.GetCoinsPerOfflineTest().ToString();

        onlineModeToggle.isOn = GameManager.Instance.IsOnlineMode();
    }

    private void OnModeToggled(bool isOn)
    {
        GameManager.Instance.SetOnlineMode(isOn);
        Debug.Log($"Mode Toggled: {(isOn ? "ONLINE" : "OFFLINE")}");
    }

    private void OnConnectClicked()
    {
        string url = tiktokURLInput.text;
        if (!string.IsNullOrEmpty(url))
        {
            TikTokLiveManager.Instance.ConnectToTikTokLive(url);
            Debug.Log("Connected to TikTok Live!");
        }
    }

    private void OnDisconnectClicked()
    {
        TikTokLiveManager.Instance.DisconnectFromTikTokLive();
        Debug.Log("Disconnected from TikTok Live");
    }

    private void OnSaveSettingsClicked()
    {
        // Parse and set coin values
        if (int.TryParse(coinsPerGiftInput.text, out int giftCoins))
            GameManager.Instance.SetCoinsPerGift(giftCoins);

        if (int.TryParse(coinsPerHeartInput.text, out int heartCoins))
            GameManager.Instance.SetCoinsPerHeart(heartCoins);

        if (int.TryParse(coinsPerShareInput.text, out int shareCoins))
            GameManager.Instance.SetCoinsPerShare(shareCoins);

        if (int.TryParse(coinsPerCommentInput.text, out int commentCoins))
            GameManager.Instance.SetCoinsPerComment(commentCoins);

        if (int.TryParse(coinsPerSaveInput.text, out int saveCoins))
            GameManager.Instance.SetCoinsPerSave(saveCoins);

        if (int.TryParse(coinsPerOfflineTestInput.text, out int offlineCoins))
            GameManager.Instance.SetCoinsPerOfflineTest(offlineCoins);

        GameManager.Instance.SaveSettings();
        Debug.Log("Settings Saved!");
    }

    private void OnResetScoreClicked()
    {
        GameManager.Instance.ResetScore();
        Debug.Log("Score Reset");
    }

    private void OnCloseClicked()
    {
        gameObject.SetActive(false);
    }
}
