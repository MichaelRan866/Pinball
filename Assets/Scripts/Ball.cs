using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    public float stretchFactor;
    public Transform ballSprite;
    public TrailRenderer trail;
    public AudioSource hitSnd;

    private void OnEnable()
    {
        if(rb == null)
            rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * 15f);
        if (Random.Range(0f, 10f) < 5f)
            rb.AddForce(Vector2.right * 35f);
        else
            rb.AddForce(-Vector2.right * 35f);
    }
    private void Update()
    {
        ballSprite.localScale = Vector3.Lerp(ballSprite.localScale, 
            Vector3.Lerp(Vector3.one, new Vector3(1f / stretchFactor, stretchFactor, 1f), rb.linearVelocity.magnitude / 20f), 
            10f * Time.deltaTime);
        ballSprite.rotation = Quaternion.FromToRotation(Vector2.up, rb.linearVelocity.normalized);
        float lerpT = Mathf.Clamp(rb.linearVelocity.magnitude / 25f, 0f, 1f);
        trail.time = Mathf.Lerp(0.25f, 0.05f, lerpT);
        trail.widthMultiplier = Mathf.Min(ballSprite.localScale.x, ballSprite.localScale.y) * transform.localScale.x;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (rb.linearVelocity.magnitude > 2f)
        {
            hitSnd.pitch = Random.Range(0.75f, 1.25f);
            hitSnd.volume = Mathf.Lerp(0f, 1f, rb.linearVelocity.magnitude / 10f) * 0.5f;
            hitSnd.Play();
        }
    }
}
