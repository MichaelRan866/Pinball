using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform camHolder;
    private Camera camComponent;
    public static Rigidbody2D ball;
    public TMPro.TextMeshProUGUI scoreTxt;
    public GameObject resetScreen;
    public static Transform scoreTxt_;
    public float camShakeSpd = 1f;
    public ParticleSystem deathVFX;
    public AudioClip deathSnd;
    public static AudioSource scoreSnd;
    public static float camXShake;
    public static int score;

    private void Start()
    {
        score = 0;
        camComponent = camHolder.GetComponentInChildren<Camera>();
        scoreSnd = GetComponent<AudioSource>();
        ball = GameObject.FindWithTag("Ball").GetComponent<Rigidbody2D>();
        scoreTxt_ = scoreTxt.transform;
    }

    private void Update()
    {
        camHolder.position = Vector3.Lerp(camHolder.position, ball.position + ball.linearVelocity / 10f, 5f * Time.deltaTime);
        camHolder.position = new Vector3(0f, Mathf.Clamp(camHolder.position.y, 0f, 32f), -10f);
        camComponent.transform.localPosition = Vector3.Lerp(camComponent.transform.localPosition, Vector3.right * Mathf.Sin(Time.time * camShakeSpd * Mathf.Deg2Rad) * camXShake, 10f * Time.deltaTime);
        camComponent.orthographicSize = Mathf.Lerp(camComponent.orthographicSize, Mathf.Lerp(5f, 7.5f, Mathf.InverseLerp(0f, 32f, camHolder.position.y)), 2f * Time.deltaTime);
        camXShake = Mathf.Lerp(camXShake, 0f, 5f * Time.deltaTime);

        scoreTxt.text = score.ToString();
        scoreTxt_.localScale = Vector3.Lerp(scoreTxt_.localScale, Vector3.one * 7f, 15f * Time.deltaTime);

        if (ball.gameObject.activeInHierarchy && ball.position.y < -7.5f)
        {
            ball.gameObject.SetActive(false);
            deathVFX.Play();
            AudioSource.PlayClipAtPoint(deathSnd, Vector3.zero);
            Invoke(nameof(OpenResetScreen), 1f);
            camXShake += 10f;
        }
        scoreSnd.pitch = Mathf.Lerp(scoreSnd.pitch, 1f, Time.deltaTime);
    }
    private void OpenResetScreen() { resetScreen.SetActive(true); }
    public static void AddScore(int amt)
    {
        score += amt;
        scoreTxt_.localScale = new Vector3(3f, 10f, 7f);
        scoreSnd.Play();
        scoreSnd.pitch = Mathf.Clamp(scoreSnd.pitch + 0.2f, 0f, 2f);
    }

    public static void Reset()
    {
        score = 0;
        ball.transform.position = Vector3.zero;
        ball.linearVelocity = Vector3.zero;
        ball.gameObject.SetActive(true);
    }
}
