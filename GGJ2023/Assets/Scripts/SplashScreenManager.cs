using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Fader))]
public class SplashScreenManager : MonoBehaviour
{
    [SerializeField] private float time = 3f;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > time)
        {
            Fader.faderInstance.FadeTo("MainMenu");
        }
    }
}
