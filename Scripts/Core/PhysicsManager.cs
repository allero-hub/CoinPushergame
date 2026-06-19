using UnityEngine;

public class PhysicsManager : MonoBehaviour
{
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float linearDrag = 0.1f;
    [SerializeField] private float angularDrag = 0.05f;
    [SerializeField] private float bounceDamping = 0.7f;

    private void Start()
    {
        // Configure Physics 2D
        Physics2D.gravity = new Vector2(0, -gravity);
        Physics2D.defaultMaterial.bounciness = bounceDamping;
        Physics2D.defaultMaterial.friction = 0.4f;
    }

    public void SetGravity(float newGravity)
    {
        gravity = newGravity;
        Physics2D.gravity = new Vector2(0, -gravity);
    }

    public float GetGravity() => gravity;
}
