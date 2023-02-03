using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{
    public static int playerCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TwoPlayerGame()
    {
        playerCount = 2;
        SceneManager.LoadScene("Main");
    }

    public void ThreePlayerGame()
    {
        playerCount = 3;
        SceneManager.LoadScene("Main");
    }

    public void FourPlayerGame()
    {
        playerCount = 4;
        SceneManager.LoadScene("Main");
    }

    public void Instructions()
    {
        // LoadScene();
    }

    public void Scoreboard()
    {
        // LoadScene();
    }

    public void Exit()
    {
        Application.Quit();
    }
    
}
