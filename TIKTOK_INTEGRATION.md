# TikTok Live Integration Guide

## Overview

This guide explains how to integrate TikTok Live events to trigger coin drops.

## How It Works

### Event Types

| Event | Coins | Icon |
|-------|-------|------|
| Gift | 10 | 🎁 |
| Heart | 5 | ❤️ |
| Share | 8 | 📤 |
| Comment | 3 | 💬 |
| Save | 6 | 💾 |

### Workflow

1. **Connection**: Viewer connects to TikTok Live
2. **Event Occurs**: Viewer sends gift/heart/shares/comments/saves
3. **Event Received**: TikTok API sends event to game
4. **Queue Event**: Event added to processing queue
5. **Process**: Coins spawn, score updates, UI shows event
6. **Display**: User profile popup shows briefly

## Setup Steps

### 1. TikTok Developer Account

1. Go to https://developer.tiktok.com/
2. Sign in or create account
3. Create new application
4. Set application name: "CoinPusherGame"
5. Select "Web" platform
6. Add redirect URL: `http://localhost:3000/callback` (for testing)

### 2. Get API Credentials

1. In TikTok Developer Dashboard
2. Go to Application Settings
3. Copy:
   - Client ID
   - Client Secret
4. Save securely (never commit to GitHub)

### 3. Authentication Flow

The game needs to authenticate with TikTok:

```csharp
// In SettingsPanel.cs when "Connect" is clicked:
public void OnConnectClicked()
{
    string url = tiktokURLInput.text;
    if (!string.IsNullOrEmpty(url))
    {
        // Authenticate with TikTok
        TikTokLiveManager.Instance.ConnectToTikTokLive(url);
    }
}
```

### 4. Event Listening

Once authenticated, listen for events:

```csharp
// In TikTokLiveManager.cs
public void ReceiveWebSocketEvent(string eventJson)
{
    // Parse incoming TikTok event
    TikTokEvent tikEvent = JsonUtility.FromJson<TikTokEvent>(eventJson);
    EnqueueEvent(tikEvent);
}

// In Update() or FixedUpdate():
if (IsConnected())
{
    ProcessQueuedEvents();
}
```

### 5. Processing Events

```csharp
private void ProcessTikTokEvent(TikTokEvent tikEvent)
{
    int coinsToAdd = 0;

    switch (tikEvent.eventType)
    {
        case TikTokEventType.Gift:
            coinsToAdd = GameManager.Instance.GetCoinsPerGift();
            break;
        // ... other cases
    }

    // Spawn coins
    CoinSpawner coinSpawner = FindObjectOfType<CoinSpawner>();
    coinSpawner.SpawnCoins(coinsToAdd);

    // Display user profile
    UIManager.Instance?.ShowTikTokEventUI(tikEvent, coinsToAdd);
}
```

## Testing Without Live Connection

### Simulate TikTok Events

Use the built-in simulation method:

```csharp
// In Console or Inspector:
TikTokLiveManager.Instance.SimulateTikTokEvent(
    TikTokEventType.Gift, 
    "TestViewer"
);
```

This creates a fake event for testing.

### Test All Event Types

```csharp
// Simulate each event type
TikTokLiveManager.Instance.SimulateTikTokEvent(TikTokEventType.Gift, "Viewer1");
TikTokLiveManager.Instance.SimulateTikTokEvent(TikTokEventType.Heart, "Viewer2");
TikTokLiveManager.Instance.SimulateTikTokEvent(TikTokEventType.Share, "Viewer3");
TikTokLiveManager.Instance.SimulateTikTokEvent(TikTokEventType.Comment, "Viewer4");
TikTokLiveManager.Instance.SimulateTikTokEvent(TikTokEventType.Save, "Viewer5");
```

## User Profile Display

### Profile Picture

The game displays the user's profile picture from TikTok:

```csharp
public void ShowTikTokEventUI(TikTokEvent tikEvent, int coinsAwarded)
{
    // Load profile picture from URL
    StartCoroutine(LoadProfilePicture(tikEvent.profilePicURL));
}
```

### Display Duration

Profile popup displays for 2 seconds then fades out.

## Customizing Coin Amounts

Edit in Settings Panel:

1. Open game
2. Click Settings button ⚙️
3. Adjust coin values:
   - Coins per Gift: 10 (default)
   - Coins per Heart: 5 (default)
   - Coins per Share: 8 (default)
   - Coins per Comment: 3 (default)
   - Coins per Save: 6 (default)
4. Click "Save Settings"

Values are saved to PlayerPrefs and persist across sessions.

## API Integration (Advanced)

### WebSocket Connection

For real-time events, use WebSocket:

```csharp
using System.Net.WebSockets;
using System.Text;

private ClientWebSocket webSocket;

public async void ConnectToTikTokLive(string studioURL)
{
    webSocket = new ClientWebSocket();
    await webSocket.ConnectAsync(
        new Uri("wss://tiktok-live-api.example.com/stream"),
        CancellationToken.None
    );
    
    // Start listening for events
    ListenForEvents();
}
```

### REST API Fallback

If WebSocket unavailable, poll REST API:

```csharp
private IEnumerator PollForEvents()
{
    while (isConnected)
    {
        // GET /api/events
        yield return StartCoroutine(FetchLatestEvents());
        yield return new WaitForSeconds(1f);
    }
}
```

## Security Considerations

⚠️ **Never commit API credentials:**

1. Create `.env` file (add to `.gitignore`)
2. Store credentials in environment variables
3. Use secure authentication flow
4. Validate all incoming events
5. Rate-limit event processing

## Troubleshooting

**Events not received?**
- Verify TikTok account is authenticated
- Check WebSocket connection status
- Verify API credentials are correct
- Check firewall/network settings

**Profile pictures not loading?**
- Verify image URL is valid
- Check internet connection
- Use placeholder image if failed

**Coins spawning unexpectedly?**
- Check event queue is not backed up
- Verify ProcessQueuedEvents() is called only once per frame
- Add debug logging to track events

## Next Steps

1. ✅ Set up TikTok Developer Account
2. ✅ Get API Credentials
3. ✅ Implement WebSocket connection
4. ✅ Test with simulated events
5. ✅ Go live on TikTok!

---

**TikTok integration complete! 🚀📱**
