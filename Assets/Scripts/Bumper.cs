using UnityEngine;

public class Bumper : MonoBehaviour
{
    public float boost;
    public Transform edge;

    private void Update()
    {
        edge.localScale = Vector3.Lerp(edge.localScale, Vector3.one, 15f * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.name.Contains("Ball"))
        {
            Vector2 dir = other.transform.position - transform.position;
            dir.Normalize();
            other.rigidbody.linearVelocity = Vector2.Reflect(other.rigidbody.linearVelocity.normalized, dir) * (other.rigidbody.linearVelocity.magnitude + boost);

            edge.localScale *= 1.5f;
        }
    }
}
