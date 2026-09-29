using UnityEngine;

public class LvlSet : MonoBehaviour
{
    public float ySpacing;
    public float xSpacing;
    public Transform cam;
    private Camera camComponent;
    public Rigidbody2D ball;
    public GameObject bumper;
    public Vector3 offset;

    private void Start()
    {
        camComponent = cam.GetComponent<Camera>();
    }
    /*private void Start()
    {
        for (int i = 0; i < 7; i++)
        {
            if (i % 2 != 0)
            {
                Instantiate(bumper, new Vector3(0f, i * ySpacing, 0f) + offset, Quaternion.identity);
                for (int j = 1; j <= 2; j++)
                {
                    Instantiate(bumper, new Vector3(-xSpacing * j, i * ySpacing, 0f) + offset, Quaternion.identity);
                    Instantiate(bumper, new Vector3(xSpacing * j, i * ySpacing, 0f) + offset, Quaternion.identity);
                }
            }
            else
            {
                xSpacing *= 3f;
                for (int j = 1; j <= 3; j++)
                {
                    Instantiate(bumper, new Vector3(-xSpacing * j / 2f, i * ySpacing, 0f) + offset, Quaternion.identity);
                    Instantiate(bumper, new Vector3(xSpacing * j / 2f, i * ySpacing, 0f) + offset, Quaternion.identity);
                }
                xSpacing /= 3f;
            }
        }
    }*/
    private void Update()
    {
        cam.position = Vector3.Lerp(cam.position, ball.position + ball.linearVelocity / 10f, 15f * Time.deltaTime);
        cam.position = new Vector3(0f, Mathf.Clamp(cam.position.y, 0f, 32f), -10f);
        camComponent.orthographicSize = Mathf.Lerp(5f, 7.5f, Mathf.InverseLerp(0f, 32f, cam.position.y));
    }
}
