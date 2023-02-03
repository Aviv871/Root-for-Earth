using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fader : MonoBehaviour
{
    [SerializeField] private Image[] images;
    [SerializeField] private Text[] texts;
    [SerializeField] private AudioSource music;

    public static Fader faderInstance;

    private void Awake()
    {
        if (faderInstance != null) Debug.LogError("More than 1 fader in scene! Use only one");
        faderInstance = this;
    }

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    private IEnumerator FadeOut(string scene)
    {
        // foreach (Image image in images)
        // {
        //     image.enabled = true;
        // }
        // foreach (Text text in texts)
        // {
        //     text.enabled = true;
        // }
        // if (music != null) music.enabled = false;

        float a = 1f;
        while (a > 0f)
        {
            foreach (Image image in images)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, a);
            }
            foreach (Text text in texts)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, a);
            }

            a -= Time.deltaTime;
            yield return 0;
        }

        SceneManager.LoadScene(scene);
    }

    private IEnumerator FadeIn()
    {
        foreach (Image image in images)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        }
        foreach (Text text in texts)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        }

        float a = 0f;

        while (a < 1f)
        {
            foreach (Image image in images)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, a);
            }
            foreach (Text text in texts)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, a);
            }
            a += Time.deltaTime;
            yield return 0;
        }
    }
}
