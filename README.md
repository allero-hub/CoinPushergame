# 🎮 Coin Pusher Game - Unity 3D

A **TikTok Live-integrated Coin Pusher game** with offline testing mode, real-time physics, haptic feedback, and dynamic leaderboard updates.

## Features ✨

✅ Physics-based coin dropping with realistic gravity & bounce  
✅ TikTok Live integration (Gifts, Hearts, Shares, Comments, Saves)  
✅ Real-time score & rank updates  
✅ Offline mode for testing without TikTok  
✅ Customizable coin amounts per action  
✅ Haptic feedback on mobile devices  
✅ User profile display with each TikTok event  
✅ Dynamic reward animations & sound effects  

## Quick Start

1. Open Unity 2022 LTS or newer
2. Create new 2D Core project
3. Copy all scripts from `Scripts/` folder
4. Set up scene hierarchy (see SETUP.md)
5. Attach prefabs and test!

## Documentation

- **[FULL_GUIDE.md](./FULL_GUIDE.md)** - Complete development guide
- **[SETUP.md](./SETUP.md)** - Scene setup instructions
- **[TIKTOK_INTEGRATION.md](./TIKTOK_INTEGRATION.md)** - TikTok setup guide

## Project Structure

```
Scripts/
├── Core/
│   ├── GameManager.cs
│   ├── PhysicsManager.cs
│   └── InputHandler.cs
├── Coins/
│   ├── CoinSpawner.cs
│   └── CoinBehavior.cs
├── UI/
│   ├── UIManager.cs
│   └── SettingsPanel.cs
├── TikTok/
│   └── TikTokLiveManager.cs
└── Audio/
    └── AudioManager.cs
```

## How It Works

### Offline Mode 🎮
- Click/Tap screen to spawn coins
- Coins fall with physics simulation
- Collect coins to earn points
- Adjust settings before testing

### Online Mode 📱
- Connect to TikTok Live Studio
- Viewers trigger coin drops:
  - 🎁 Gift = 10 coins
  - ❤️ Heart = 5 coins
  - 📤 Share = 8 coins
  - 💬 Comment = 3 coins
  - 💾 Save = 6 coins
- Score updates in real-time
- User profiles display with events

## Customization

Edit coin amounts in **Settings Panel**:
- Coins per Gift
- Coins per Heart
- Coins per Share
- Coins per Comment
- Coins per Save
- Coins per Offline Test

## Testing Checklist

- [ ] Coins spawn and fall realistically
- [ ] Score updates on coin collection
- [ ] Offline mode manual spawning works
- [ ] Settings panel is fully functional
- [ ] TikTok event UI displays correctly
- [ ] Mode toggle works (ONLINE ↔ OFFLINE)
- [ ] Haptic feedback on mobile devices
- [ ] Audio effects play correctly
- [ ] Leaderboard updates in real-time

## Developer Notes

- **Unity Version:** 2022 LTS+
- **Physics:** Rigidbody2D + Physics2D
- **UI:** TextMesh Pro + Canvas
- **Platform:** PC, Mobile (iOS/Android)

## Troubleshooting

**Coins don't fall?**
- Check Physics2D gravity in PhysicsManager
- Verify Rigidbody2D is set to Dynamic

**Score not updating?**
- Ensure CoinBehavior calls GameManager.AddScore()
- Check CollectZone tag is set correctly

**TikTok events not firing?**
- Verify TikTokLiveManager.ProcessQueuedEvents() is called
- Check internet connection to TikTok Live Studio

## License

Free to use for personal & commercial projects

---

**Happy Game Development! 🚀💰**
