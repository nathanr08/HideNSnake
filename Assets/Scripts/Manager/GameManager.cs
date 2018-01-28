using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : StateController {

    public static GameManager Instance;

    public float gameLength;
    public float gameStartCountdownLength;

    public GameObject snakeSpawn;
    public GameObject[] hamsterSpawns;

    public Text startCountdownText;
    public Text matchTimerText;

    public GameObject snakePrefab;
    public GameObject hamsterPrefab;

    public static string snakePlayer = "Snake";
    public static string hamsterPlayer = "Hamster";

    private float gameTimeRemaining { get; set; }
    private float gameStartCountdown { get; set; }

    private bool gameStarted;

    // Use this for initialization
    void Start () {
        if (Instance == null)
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
        if (gameStarted)
        {
            gameStartCountdown -= Time.deltaTime;
            startCountdownText.text = ((int)gameStartCountdown).ToString();
            if (gameStartCountdown <= 0.0f)
            {
                // game has started
                startCountdownText.gameObject.SetActive(false);
                gameTimeRemaining -= Time.deltaTime;
                matchTimerText.text = ((int)gameTimeRemaining).ToString();
                if (gameTimeRemaining <= 0.0f)
                {
                    EndGame();
                }
            }
        }
	}

    public void InitGame(InitGameEvent.InitGameEventArgs e)
    {
        gameStarted = true;
        // spawn characters
        int hamsterSpawnCount = 0;
        for (int i = 0; i < 4; ++i)
        {
            GameObject player;
            if (e.playerDataList[i] != "")
            {
                if (e.playerDataList[i] == "Snake")
                {
                    player = Instantiate(snakePrefab);
                    player.transform.position = snakeSpawn.transform.position;
                }
                else
                {
                    player = Instantiate(hamsterPrefab);
                    player.transform.position = hamsterSpawns[hamsterSpawnCount].transform.position;
                    ++hamsterSpawnCount;
                }
                BaseControllable control = player.GetComponent<BaseControllable>();
                control.SetPlayerInputNumber(i + 1);
            }
            
        }
        // start timer
        gameTimeRemaining = gameLength;
        gameStartCountdown = gameStartCountdownLength;
        startCountdownText.gameObject.SetActive(true);
        startCountdownText.text = ((int)gameStartCountdown).ToString();
        matchTimerText.text = ((int)gameTimeRemaining).ToString();
        matchTimerText.gameObject.SetActive(true);
    }

    public void EndGame()
    {
        gameStarted = false;
        // despawn
        GameObject[] hamsters = GameObject.FindGameObjectsWithTag("hamster");
        foreach(GameObject hamster in hamsters)
        {
            Destroy(hamster);
        }
        GameObject[] snakes = GameObject.FindGameObjectsWithTag("snake");
        foreach(GameObject snake in snakes)
        {
            Destroy(snake);
        }
        // show win screen
    }
}
