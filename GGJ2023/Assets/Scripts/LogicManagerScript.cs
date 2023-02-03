using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogicManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private List<GameObject> startingPositions;
    public List<GameObject> players;
    public float totalScore;
    public Text scoreText;

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
            player.GetComponentInChildren<HeadScript>().transform.position = startingPositions[i].transform.position;
            player.GetComponentInChildren<HeadScript>().movementControls = (MovementControls)i;
            player.GetComponentInChildren<HeadScript>().transform.rotation = Quaternion.Inverse(startingPositions[i].transform.rotation);
            players.Add(player);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        generatePlayers(2);
    }

    // Update is called once per frame
    void Update()
    {
        // update total score
        totalScore = 0;
        foreach (GameObject player in players)
        {
            totalScore += player.GetComponent<PlayerScript>().score;
        }
        scoreText.text = "Score: " + (int)System.Math.Round(totalScore);
    }
    public void GameOver() {
        Debug.Log("Game Over");
    }
}
