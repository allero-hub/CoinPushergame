using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private CoinSpawner coinSpawner;

    private void Update()
    {
        // Offline Mode: Manual Coin Drop
        if (GameManager.Instance.IsOfflineMode())
        {
            HandleOfflineInput();
        }
    }

    private void HandleOfflineInput()
    {
        if (Input.GetMouseButtonDown(0)) // Left Mouse Click / Touch
        {
            int coinAmount = GameManager.Instance.GetCoinsPerOfflineTest();
            coinSpawner.SpawnCoins(coinAmount);
            
            Debug.Log($"Test Drop: {coinAmount} coins spawned");
        }
    }
}
