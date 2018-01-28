using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public float gameLength;
    public float gameStartCountdownLength;

    public GameObject snakePrefab;
    public GameObject hamsterPrefab;

    public static string snakePlayer = "Snake";
    public static string hamsterPlayer = "Hamster";

    private float gameTimeRemaining { get; set; }
    private float gameStartCountdown { get; set; }
    // Use this for initialization
    void Start () {
        if (Instance = null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        gameTimeRemaining = gameLength;
        gameStartCountdown = gameStartCountdownLength;
	}
	
	// Update is called once per frame
	void Update () {
        gameStartCountdown -= Time.deltaTime;
		if(gameStartCountdown <= 0.0f)
        {
            // game has started
            gameTimeRemaining -= Time.deltaTime;
            if (gameTimeRemaining <= 0.0f)
            {
                EndGame();
            }
        }
	}

    public void InitGame(InitGameEvent.InitGameEventArgs e)
    {
        // spawn characters
        for (int i = 0; i < 4; ++i)
        {
            GameObject player;
            if(e.playerDataList[i] == "Snake")
            {
                player = Instantiate(snakePrefab);
            }
        }
        // start timer
    }

    public void EndGame()
    {
        // despawn
        // show win screen
    }
}
