using Unity.VisualScripting;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    public Transform bg;
    public GameObject txt;
    public TMPro.TextMeshProUGUI scoreTxt;
    public AudioSource resetSnd;

    private void OnEnable()
    {
        bg.localScale = Vector3.zero;
        startBlinking = false;
    }

    bool startBlinking;
    private void Update()
    {
        bg.localScale = Vector3.Lerp(bg.localScale, Vector3.one * 35f, 5f * Time.deltaTime);

        if (blinkTimes == 0)
        {
            if(bg.localScale.x > 34)
            {
                txt.SetActive(true);

                if (Input.GetButtonDown("Jump"))
                {
                    BlinkTxt();
                    resetSnd.Play();
                }
            }
            else if (bg.localScale.x > 30)
            {
                scoreTxt.gameObject.SetActive(true);
                scoreTxt.text = "FINAL SCORE: " + GameManager.score;
            }
        }
    }

    int blinkTimes;
    private void BlinkTxt() 
    {
        txt.SetActive(!txt.activeInHierarchy);
        blinkTimes++;
        if (blinkTimes < 10)
        {
            Invoke(nameof(BlinkTxt), 0.1f);
        }
        else
        {
            gameObject.SetActive(false);
            GameManager.Reset();
        }
    }
    private void OnDisable()
    {
        bg.localScale = Vector3.zero;
        txt.SetActive(false);
        scoreTxt.gameObject.SetActive(false);
        startBlinking = false;
        blinkTimes = 0;
    }
}
