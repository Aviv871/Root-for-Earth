using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
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
