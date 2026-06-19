# Coin Pusher Game - Complete Development Guide

## Table of Contents
1. [Project Setup](#project-setup)
2. [Core Scripts](#core-scripts)
3. [TikTok Integration](#tiktok-integration)
4. [UI Setup](#ui-setup)
5. [Scene Configuration](#scene-configuration)
6. [Testing & Debugging](#testing--debugging)

---

## Project Setup

### Prerequisites
- Unity 2022 LTS or newer
- TextMesh Pro package
- Physics 2D (built-in)
- Input System (new)

### Create Project
1. Open Unity Hub → **New Project**
2. Select **2D Core** template
3. Name: `CoinPusherGame`
4. Create project

### Folder Structure
```
Assets/
├── Scripts/
│   ├── Core/
│   │   ├── GameManager.cs
│   │   ├── PhysicsManager.cs
│   │   └── InputHandler.cs
│   ├── Coins/
│   │   ├── CoinSpawner.cs
│   │   └── CoinBehavior.cs
│   ├── UI/
│   │   ├── UIManager.cs
│   │   ├── SettingsPanel.cs
│   │   └── LeaderboardDisplay.cs
│   ├── TikTok/
│   │   ├── TikTokLiveManager.cs
│   │   └── EventHandler.cs
│   └── Audio/
│       └── AudioManager.cs
├── Prefabs/
│   ├── Coin.prefab
│   ├── UI_Canvas.prefab
│   └── TikTok_EventUI.prefab
├── Scenes/
│   ├── Main.unity
│   ├── Settings.unity
│   └── Game.unity
├── Audio/
│   ├── coin_drop.wav
│   ├── bonus_sound.wav
│   └── UI_click.wav
└── Resources/
    └── Settings/
        └── GameConfig.json
```

---

## Core Scripts

### 1. GameManager.cs
**Purpose:** Central game logic, score tracking, mode management

**Key Methods:**
- `AddScore(int amount)` - Add points
- `SetOnlineMode(bool isOnline)` - Switch modes
- `GetCoinsPerGift()` / `SetCoinsPerGift(int)` - Customize coin amounts
- `SaveSettings()` / `LoadSettings()` - Persist player preferences

**Events:**
- `OnScoreChanged` - Fired when score updates
- `OnModeChanged` - Fired when mode switches

### 2. CoinSpawner.cs
**Purpose:** Spawn coins with randomized trajectories

**Key Methods:**
- `SpawnCoins(int coinCount)` - Spawn multiple coins
- `SpawnSingleCoin()` - Spawn individual coin with physics

**Configuration:**
- `spawnHeight` - How high coins spawn
- `horizontalSpread` - Random horizontal offset range

### 3. CoinBehavior.cs
**Purpose:** Individual coin logic, collection detection, rewards

**Key Methods:**
- `CollectCoin()` - Award points & trigger effects
- `OnTriggerEnter2D()` - Collision detection

**Features:**
- Particle effects on collection
- Sound playback
- Haptic feedback (mobile)
- Score addition

### 4. InputHandler.cs
**Purpose:** Handle player input for offline mode

**Controls:**
- **Left Mouse Click / Touch** - Spawn coins in offline mode

### 5. PhysicsManager.cs
**Purpose:** Configure Physics2D for realistic coin movement

**Settings:**
- `gravity` - Pull force (9.81 default)
- `bounceDamping` - Bounce restitution (0.7 default)
- `friction` - Surface friction (0.4 default)

### 6. AudioManager.cs
**Purpose:** Centralized audio playback

**Sounds:**
- `coin_collect` - Coin pickup sound
- `bonus` - Bonus reward sound
- `ui_click` - UI interaction sound

---

## TikTok Integration

### 7. TikTokLiveManager.cs
**Purpose:** Handle TikTok Live events

**Key Methods:**
- `ConnectToTikTokLive(string url)` - Establish connection
- `DisconnectFromTikTokLive()` - Close connection
- `EnqueueEvent(TikTokEvent)` - Add event to queue
- `ProcessQueuedEvents()` - Process all queued events
- `SimulateTikTokEvent()` - Test events offline

**Event Types:**
- `Gift` - 10 coins
- `Heart` - 5 coins
- `Share` - 8 coins
- `Comment` - 3 coins
- `Save` - 6 coins

**Data Structures:**
```csharp
public enum TikTokEventType
{
    Gift,
    Heart,
    Share,
    Comment,
    Save
}

public class TikTokEvent
{
    public TikTokEventType eventType;
    public string userId;
    public string username;
    public string profilePicURL;
    public System.DateTime timestamp;
}

public class UserProfile
{
    public string userId;
    public string username;
    public string profilePicURL;
    public int eventCount;
}
```

---

## UI Setup

### 8. UIManager.cs
**Purpose:** Update UI in real-time, display TikTok events

**Key Methods:**
- `UpdateScoreUI(int)` - Display score
- `UpdateModeUI(bool)` - Display ONLINE/OFFLINE indicator
- `ShowTikTokEventUI()` - Show donor profile & coins earned

**UI Elements:**
- Score Display (top-left)
- Rank Display (top-left)
- Mode Indicator (top-right)
- TikTok Event Popup (center)
- Settings Button (corners)

### 9. SettingsPanel.cs
**Purpose:** Configure game settings

**Settings:**
- Online/Offline mode toggle
- TikTok Live URL input
- Coin amount sliders:
  - Coins per Gift
  - Coins per Heart
  - Coins per Share
  - Coins per Comment
  - Coins per Save
  - Coins per Offline Test
- Save/Reset buttons

---

## Scene Configuration

### Hierarchy Structure
```
Main Scene
├── GameManager (empty GameObject)
│   └── GameManager.cs
├── TikTokLiveManager (empty GameObject)
│   └── TikTokLiveManager.cs
├── PhysicsManager (empty GameObject)
│   └── PhysicsManager.cs
├── CoinSpawner (GameObject with SpriteRenderer)
│   ├── CoinSpawner.cs
│   └── SpawnPoint (empty child)
├── GameArea (BoxCollider2D trigger)
│   └── Tag: "CollectZone"
├── BottomCollider (barrier)
│   └── BoxCollider2D
├── Canvas (UI Canvas)
│   ├── ScoreText (TextMeshPro)
│   ├── RankText (TextMeshPro)
│   ├── ModeIndicator (TextMeshPro)
│   ├── SettingsButton (Button)
│   ├── TikTokEventPopup (CanvasGroup)
│   │   ├── ProfileImage (Image)
│   │   ├── UsernameText (TextMeshPro)
│   │   └── NotificationText (TextMeshPro)
│   └── SettingsPanel (Panel)
│       ├── Inputs for coin amounts
│       ├── Connect/Disconnect buttons
│       └── Close button
├── AudioManager (GameObject)
│   ├── AudioManager.cs
│   └── AudioSource (prefab)
└── UIManager (GameObject)
    └── UIManager.cs
```

### Coin Prefab Setup

1. **Create Coin GameObject**
   - Name: `Coin`
   - Add Sprite Renderer (yellow/gold circle)

2. **Add Physics**
   - Rigidbody2D
     - Body Type: Dynamic
     - Gravity Scale: 1
     - Constraints: Freeze Rotation Z
   - Circle Collider 2D
     - Radius: 0.5

3. **Add Script**
   - CoinBehavior.cs
   - Set coinValue to 10

4. **Save as Prefab**
   - Drag to `Assets/Prefabs/Coin.prefab`

---

## Testing & Debugging

### Offline Mode Test
1. Launch game
2. Ensure mode is set to OFFLINE
3. Click screen multiple times
4. Watch coins spawn and fall
5. Verify score increases when coins collect

### Online Mode Test
1. Open Settings panel
2. Enter TikTok Live Studio URL
3. Click Connect button
4. Verify mode shows "🔴 LIVE"
5. Use SimulateTikTokEvent() to test:
   ```csharp
   TikTokLiveManager.Instance.SimulateTikTokEvent(
       TikTokEventType.Gift, 
       "TestUser"
   );
   ```

### Common Issues

| Problem | Cause | Solution |
|---------|-------|----------|
| Coins don't fall | Physics2D not configured | Check PhysicsManager gravity |
| Score doesn't update | CoinBehavior not calling AddScore | Add debug log in CollectCoin() |
| TikTok events not firing | ProcessQueuedEvents() not called | Add call in Update() |
| No haptic feedback | Running in editor | Test on actual mobile device |
| Audio not playing | Missing audio clips | Verify Resources/Audio/ folder |
| UI not showing | Canvas not set to Screen Space - Overlay | Fix Canvas render mode |

---

## Performance Optimization

- **Object Pooling:** Cache coins instead of instantiating
- **Physics Updates:** Use FixedUpdate for physics calls
- **UI Updates:** Only update UI when values change
- **Audio:** Limit simultaneous audio sources
- **Memory:** Destroy coins after 30 seconds

---

## Deployment

### Build for Mobile
1. File → Build Settings
2. Add Main scene
3. Select iOS or Android platform
4. Configure player settings
5. Build and deploy

### Key Settings
- Resolution: 1080x1920 (portrait)
- Orientation: Portrait
- API Level: Android 8.0+ (API 26+)

---

**Happy Game Development! 🚀**
