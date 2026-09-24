using UnityEngine;

public class LvlSet : MonoBehaviour
{
    public float ySpacing;
    public float xSpacing;
    public Transform cam;
    public Rigidbody2D ball;
    public GameObject bumper;
    public Vector3 offset;

    private void Start()
    {
        for (int i = 0; i < 5; i++) 
        {
            int rand = Random.Range(0, 10);
            float scalar = Random.Range(0.5f, 1f);
            if (rand < 5 || i == 0)
            {
                Instantiate(bumper, new Vector3(-xSpacing * scalar, i * ySpacing, 0f) + offset, Quaternion.identity);
                Instantiate(bumper, new Vector3(xSpacing * scalar, i * ySpacing, 0f) + offset, Quaternion.identity);
                Instantiate(bumper, new Vector3(0f, i * ySpacing, 0f) + offset, Quaternion.identity);
            }
            else
            {
                Instantiate(bumper, new Vector3(-xSpacing * scalar, i * 5f, 0f) + offset, Quaternion.identity);
                Instantiate(bumper, new Vector3(xSpacing * scalar, i * 5f, 0f) + offset, Quaternion.identity);
            }
        }
    }
    private void Update()
    {
        cam.position = Vector3.Lerp(cam.position, ball.position + ball.linearVelocity.normalized * 2.5f, 15f * Time.deltaTime);
        cam.position = new Vector3(0f, Mathf.Clamp(cam.position.y, 0f, 32f), -10f);
    }
}
