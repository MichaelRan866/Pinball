using UnityEngine;

public class Bumper : MonoBehaviour
{
    public int score = 100;
    public float boost;
    public float bumpShake;
    public AudioClip triggerSnd;
    public Transform edge;

    private void Update()
    {
        edge.localScale = Vector3.Lerp(edge.localScale, Vector3.one, 15f * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.name.Contains("Ball") && edge.localScale.x <= 1.1f)
        {
            Vector2 dir = other.transform.position - transform.position;
            dir.Normalize();
            other.rigidbody.linearVelocity = Vector2.Reflect(other.rigidbody.linearVelocity.normalized, dir) * (other.rigidbody.linearVelocity.magnitude + boost);

            AudioSource.PlayClipAtPoint(triggerSnd, transform.position);
            edge.localScale *= 1.5f;
            GameManager.camXShake = Mathf.Clamp(GameManager.camXShake + bumpShake, 0f, 10f);
            GameManager.AddScore(score);
        }
    }
}
