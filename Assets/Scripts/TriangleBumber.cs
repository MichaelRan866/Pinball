using UnityEngine;

public class TriangleBumber : MonoBehaviour
{
    public Transform normalDir;
    public Transform bounceFX;
    public AudioClip triggerSnd;
    public float boostForce;

    private void Update()
    {
        bounceFX.localScale = Vector3.Lerp(bounceFX.localScale, new Vector3(1f, 0f, 1f), 10f * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.name.Contains("Ball") && bounceFX.localScale.y < 0.1f)
        {
            Vector3 otherPos = (other.transform.position  - transform.position).normalized;
            if (Vector2.Dot(otherPos, normalDir.up.normalized) < 0f)
                return;

            AudioSource.PlayClipAtPoint(triggerSnd, transform.position);
            other.rigidbody.linearVelocity = Vector2.Lerp(normalDir.up, Vector2.Reflect(other.rigidbody.linearVelocity.normalized, normalDir.up), 0.45f).normalized * (other.rigidbody.linearVelocity.magnitude + boostForce);
            bounceFX.localScale = Vector3.one;
            GameManager.camXShake += 1f;
        }
    }
}
