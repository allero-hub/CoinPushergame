using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    [SerializeField] private int coinValue = 10;
    [SerializeField] private ParticleSystem collectParticles;
    private bool hasBeenCollected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasBeenCollected) return;

        if (collision.CompareTag("CollectZone"))
        {
            CollectCoin();
        }
    }

    private void CollectCoin()
    {
        hasBeenCollected = true;

        // Add score
        GameManager.Instance.AddScore(coinValue);

        // Play particle effect
        if (collectParticles != null)
        {
            Instantiate(collectParticles, transform.position, Quaternion.identity);
        }

        // Play sound
        AudioManager.Instance?.PlaySound("coin_collect");

        // Haptic feedback
        #if UNITY_IOS || UNITY_ANDROID
        Handheld.Vibrate();
        #endif

        // Destroy coin
        Destroy(gameObject);
    }

    public void SetCoinValue(int value)
    {
        coinValue = value;
    }
}
