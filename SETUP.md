# Scene Setup Instructions

## Scene Hierarchy

Create the following hierarchy in your Main scene:

```
Main Scene
├── GameManager
│   └── GameManager.cs
├── TikTokLiveManager
│   └── TikTokLiveManager.cs
├── PhysicsManager
│   └── PhysicsManager.cs
├── CoinSpawner
│   ├── CoinSpawner.cs
│   └── SpawnPoint (child)
├── GameArea (BoxCollider2D - Trigger)
├── BottomCollider (BoxCollider2D - Barrier)
├── Canvas
│   ├── ScoreText
│   ├── RankText
│   ├── ModeIndicator
│   ├── SettingsButton
│   ├── TikTokEventPopup
│   └── SettingsPanel
├── AudioManager
│   └── AudioManager.cs
└── UIManager
    └── UIManager.cs
```

## Step-by-Step Setup

### 1. Create GameManager
1. Right-click in Hierarchy → Create Empty → Name: `GameManager`
2. Add Component → Script → `GameManager.cs`
3. Leave all fields as default

### 2. Create TikTokLiveManager
1. Create Empty → Name: `TikTokLiveManager`
2. Add Component → Script → `TikTokLiveManager.cs`

### 3. Create PhysicsManager
1. Create Empty → Name: `PhysicsManager`
2. Add Component → Script → `PhysicsManager.cs`
3. Set Gravity to 9.81
4. Set Bounce Damping to 0.7

### 4. Create CoinSpawner
1. Create Empty → Name: `CoinSpawner`
2. Add Component → Sprite Renderer (any yellow/gold sprite)
3. Add Component → Script → `CoinSpawner.cs`
4. Create child object → Name: `SpawnPoint`
5. In CoinSpawner.cs:
   - Drag Coin prefab to "Coin Prefab" field
   - Drag SpawnPoint to "Spawn Point" field
   - Set Spawn Height to 10
   - Set Horizontal Spread to 2

### 5. Create Coin Prefab
1. Create new scene or use existing
2. Create 2D → Circle → Name: `Coin`
3. Add Component → Rigidbody2D
   - Body Type: Dynamic
   - Gravity Scale: 1
   - Constraints: Freeze Rotation Z
4. Add Component → Circle Collider 2D (Radius: 0.5)
5. Add Component → Script → `CoinBehavior.cs`
   - Set Coin Value to 10
6. Drag to Assets/Prefabs → Name: `Coin`
7. Delete from scene

### 6. Create Collection Zones

**GameArea (Collect Zone):**
1. Create Empty → Name: `GameArea`
2. Add Component → Box Collider 2D
   - Is Trigger: TRUE
   - Size: (10, 2)
   - Position: (0, -3)
3. Add Tag → Create New Tag: `CollectZone`
4. Assign tag to GameArea

**BottomCollider (Barrier):**
1. Create Empty → Name: `BottomCollider`
2. Add Component → Box Collider 2D
   - Is Trigger: FALSE
   - Size: (20, 1)
   - Position: (0, -6)

### 7. Create UI Canvas

1. Right-click → UI → Canvas
2. Rename to `Canvas`
3. Select Canvas → Inspector → Canvas Scaler → Reference Resolution: 1920 x 1080

**Add Score Text:**
1. Right-click Canvas → UI → TextMeshPro - Text
2. Name: `ScoreText`
3. Position: (200, 100)
4. Size: (300, 100)
5. Text: "Score: 0"
6. Font Size: 80

**Add Rank Text:**
1. Duplicate ScoreText → Name: `RankText`
2. Position: (200, 0)
3. Text: "Rank: #1"

**Add Mode Indicator:**
1. Duplicate ScoreText → Name: `ModeIndicator`
2. Position: (1600, 100)
3. Size: (300, 100)
4. Text: "⚪ OFFLINE"
5. Font Size: 60

**Add Settings Button:**
1. Right-click Canvas → UI → Button
2. Name: `SettingsButton`
3. Position: (1800, 900)
4. Size: (80, 80)
5. Text: "⚙️"
6. Font Size: 60

**Add TikTok Event Popup:**
1. Right-click Canvas → UI → Panel
2. Name: `TikTokEventPopup`
3. Position: (0, 0)
4. Size: (400, 300)
5. Add Component → Canvas Group
6. Create children:
   - Image → Name: `ProfileImage` (200x200)
   - TextMeshPro → Name: `UsernameText` ("User")
   - TextMeshPro → Name: `NotificationText` ("+10 Coins")

**Add Settings Panel:**
1. Right-click Canvas → UI → Panel
2. Name: `SettingsPanel`
3. Position: (0, 0)
4. Size: Full screen
5. Create children for inputs:
   - Text: "Coins Per Gift"
   - InputField (default value: 10)
   - Text: "Coins Per Heart"
   - InputField (default value: 5)
   - Text: "Coins Per Share"
   - InputField (default value: 8)
   - Text: "Coins Per Comment"
   - InputField (default value: 3)
   - Text: "Coins Per Save"
   - InputField (default value: 6)
   - Text: "Coins Per Offline Test"
   - InputField (default value: 20)
   - Button → "Save Settings"
   - Button → "Reset Score"
   - Button → "Close"
   - Toggle → "Online Mode"
   - Text: "TikTok Live URL"
   - InputField
   - Button → "Connect"
   - Button → "Disconnect"

### 8. Create Managers

**AudioManager:**
1. Create Empty → Name: `AudioManager`
2. Add Component → Audio Source
3. Add Component → Script → `AudioManager.cs`
4. Create Resources/Audio folder
5. Add audio clips:
   - coin_drop.wav
   - bonus_sound.wav
   - UI_click.wav

**UIManager:**
1. Create Empty → Name: `UIManager`
2. Add Component → Script → `UIManager.cs`
3. Assign references:
   - Drag ScoreText to "Score Text"
   - Drag RankText to "Rank Text"
   - Drag ModeIndicator to "Mode Text"
   - Drag TikTokEventPopup to "Tiktok Event Popup"
   - Etc.

**InputHandler:**
1. Create Empty → Name: `InputHandler`
2. Add Component → Script → `InputHandler.cs`
3. Drag CoinSpawner to "Coin Spawner" field

## Testing Your Setup

1. Click Play
2. In Offline mode, click screen to spawn coins
3. Watch coins fall and collect
4. Score should increase
5. Open Settings panel
6. Adjust coin amounts
7. Test TikTok event simulation

## Common Issues

**Coins don't spawn?**
- Verify Coin prefab is assigned to CoinSpawner
- Check CoinSpawner script is attached

**Coins don't fall?**
- Verify Rigidbody2D Body Type is Dynamic
- Check Physics2D gravity in PhysicsManager

**Score doesn't update?**
- Verify GameArea has "CollectZone" tag
- Ensure Box Collider2D "Is Trigger" is TRUE
- Check CoinBehavior is calling AddScore()

**UI not visible?**
- Check Canvas render mode
- Verify text components have TextMeshPro components
- Ensure UI elements are within Canvas

---

**Scene setup complete! Ready for testing. 🎮**
