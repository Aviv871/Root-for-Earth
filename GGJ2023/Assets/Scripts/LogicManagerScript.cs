using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private List<GameObject> startingPositions;
    public List<GameObject> players;
    void generatePlayers(int playerCount) {
        for (int i = 0; i < playerCount; i++) {
            Debug.Log("Generating player " + i);
            GameObject player = Instantiate(playerPrefab);
            player.transform.position = startingPositions[i].transform.position;
            player.GetComponentInChildren<HeadScript>().movementControls = (MovementControls)i;
            player.GetComponentInChildren<HeadScript>().transform.rotation = Quaternion.Euler(0, 0, 90 + 90 * i);
            players.Add(player);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        generatePlayers(4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GameOver() {
        Debug.Log("Game Over");
    }
}
