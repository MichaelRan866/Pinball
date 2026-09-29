using UnityEngine;

public class Flipper : MonoBehaviour
{
    public float endRot;
    public float flipSpd;
    public float boostForce;
    public float side = 1; // 1 means left, -1 means right
    public AudioClip pressSnd;
    private Rigidbody2D rb;
    private float defaultRot;

    private void Start()
    {
        defaultRot = transform.eulerAngles.z;
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        bool keyPressed = side >= 0 ? Input.GetButton("PosX") : Input.GetButton("NegX");

        if (Input.GetButtonDown("PosX") && side >= 0 || Input.GetButtonDown("NegX") && side < 0)
            AudioSource.PlayClipAtPoint(pressSnd, transform.position);
        if (keyPressed)
        {
            float rot = transform.eulerAngles.z;
            if (rot >= 180f)
                rot -= 360f;
            if (rot < endRot)
                rb.angularVelocity = -flipSpd * side;
            else
                rb.angularVelocity = 0f;
        }
        else
        {
            rb.angularVelocity = 0f;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, transform.eulerAngles.y, defaultRot), 15f * Time.deltaTime);
        }
    }
    private void OnCollisionStay2D(Collision2D coll) 
    {
        if (coll.collider.CompareTag("Ball") && rb.angularVelocity > 0f) 
        {
            coll.collider.GetComponent<Rigidbody2D>().linearVelocity += (Vector2)transform.up * boostForce;
        }
    }
}
