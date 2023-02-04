using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private List<GameObject> startingPositions;
    public List<GameObject> players;
    public float totalScore;
    public float factoryScore;
    public TextMeshProUGUI scoreText;
    public int waterCount;
    public int rockCount;
    public float undergroundRadiusObejcts = 2.8f; // radius from center to generate in
    public float undergroundDistanceObejcts = 1f; // raius clean of object
    public GameObject gameOverText;
    public GameObject playAgainButton;
    public GameObject mainMenuButton;
    public GameObject waterObject;
    public GameObject rockObject;
    private bool isGameOver = false;

    public Color[] colors;

    void generatePlayers(int playerCount) {
        int partialDegrees = 360 / playerCount;
        for (int i = 0; i < startingPositions.Count; i++)
        {
            startingPositions[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < playerCount; i++) {
            Debug.Log("Generating player " + i);
            startingPositions[i].gameObject.SetActive(true);
            GameObject player = Instantiate(playerPrefab);
            Color myColor = colors[i];
            player.GetComponent<PlayerScript>().color = myColor;
            HeadScript headScript = player.GetComponentInChildren<HeadScript>();
            headScript.gameObject.GetComponent<SpriteRenderer>().color = myColor;
            headScript.transform.position = startingPositions[i].transform.position;
            headScript.transform.rotation = Quaternion.Euler(0,0,180) * (startingPositions[i].transform.rotation);
            headScript.movementControls = (MovementControls)i;

            TailScript tailScript = player.GetComponentInChildren<TailScript>();
            tailScript.gameObject.GetComponent<Renderer>().material.color = myColor;
            
            // Assign the originTree from the starting point to the tail in order to be destroyed later
            player.GetComponentInChildren<TailScript>().originTree = startingPositions[i].GetComponentInChildren<SpriteRenderer>().gameObject;
            players.Add(player);
        }
    }

    void GenerateUndergroundObjects(int amount2Generate, GameObject component2Generate)
    {
        int amountGenerated = 0;
        int amountFails = 0;

        while(amountGenerated < amount2Generate)
        {
            Vector3 location = Random.insideUnitCircle * undergroundRadiusObejcts;
            Collider2D collisionWithAnother = Physics2D.OverlapCircle(location, undergroundDistanceObejcts, LayerMask.GetMask("ObstacleLayer"));
            //If the Collision is empty then, we can instantiate
            if (collisionWithAnother == false)
            {
                Instantiate(component2Generate, location, Quaternion.identity);
                amountGenerated++;
            }
            else
            {
                amountFails++;
                if (amountFails > 100)
                {
                    Debug.Log("Failed to generate objects too many times");
                    break;
                }
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (ButtonBehaviour.playerCount <= 0) {
            ButtonBehaviour.playerCount = 2;
        }
        if (ButtonBehaviour.playerCount == 0) {
            Debug.Log("Player count is 0");
            ButtonBehaviour.playerCount = 2;
        }
        generatePlayers(ButtonBehaviour.playerCount);
        GenerateUndergroundObjects(waterCount, waterObject);
        GenerateUndergroundObjects(rockCount, rockObject);
        gameOverText.SetActive(false);
        playAgainButton.SetActive(false);
        mainMenuButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // update total score
        if (isGameOver)
        {
            return;
        }
        totalScore = 0;
        bool isGameoverInner = true;
        foreach (GameObject player in players)
        {
            var player_script = player.GetComponent<PlayerScript>();
            totalScore += player_script.score;
            if (player_script.isAlive)
            {
                isGameoverInner = false;
            }
        }
        scoreText.text = "Score: " + (int)System.Math.Round(totalScore);
        if (isGameoverInner)
        {
            GameOver();
        }

        if (Input.GetKey(KeyCode.Escape)) {
            SceneManager.LoadScene("mainmenu");
        }
    }
    public void GameOver() {
        Debug.Log("Game Over");
        isGameOver = true;
        gameOverText.SetActive(true);
        playAgainButton.SetActive(true);
        mainMenuButton.SetActive(true);
    }
}
