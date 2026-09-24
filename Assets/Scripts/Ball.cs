using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    public float stretchFactor;
    public Transform ballSprite;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        ballSprite.localScale = Vector3.Lerp(ballSprite.localScale, 
            Vector3.Lerp(Vector3.one, new Vector3(1f / stretchFactor, stretchFactor, 1f), rb.linearVelocity.magnitude / 20f), 
            10f * Time.deltaTime);
        ballSprite.rotation = Quaternion.FromToRotation(Vector2.up, rb.linearVelocity.normalized);
    }
}
