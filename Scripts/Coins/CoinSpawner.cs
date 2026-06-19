using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnHeight = 10f;
    [SerializeField] private float horizontalSpread = 2f;

    private void Start()
    {
        if (spawnPoint == null)
        {
            spawnPoint = transform;
        }
    }

    public void SpawnCoins(int coinCount)
    {
        for (int i = 0; i < coinCount; i++)
        {
            SpawnSingleCoin();
        }
    }

    private void SpawnSingleCoin()
    {
        // Random horizontal offset
        float randomX = Random.Range(-horizontalSpread, horizontalSpread);
        Vector3 spawnPos = spawnPoint.position + new Vector3(randomX, spawnHeight, 0);

        // Instantiate coin
        GameObject coin = Instantiate(coinPrefab, spawnPos, Quaternion.identity);

        // Add initial random velocity
        Rigidbody2D rb = coin.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            float randomForce = Random.Range(2f, 8f);
            rb.velocity = new Vector2(Random.Range(-3f, 3f), -randomForce);
        }

        // Destroy after 30 seconds (coin fell off screen)
        Destroy(coin, 30f);
    }
}
