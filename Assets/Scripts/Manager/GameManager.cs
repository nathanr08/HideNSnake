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

    public bool gameInProgress = false;
    public string matchResults = "";

    public GameObject victoryPanel;
    public Image victoryImage;
    public Sprite snakeImage;
    public Sprite hamsterImage;

    public GameObject snakePrefab;
    public GameObject hamsterPrefab;

    public PlayerIcon[] playerIcons;
    public GameObject playerIconPanel;

    public static string snakePlayer = "Snake";
    public static string hamsterPlayer = "Hamster";
    public static string animStartCountdownTrigger = "StartCountdown";
    public static string animStartMatchTrigger = "StartMatch";
    public static string animEndMatchTrigger = "EndMatch";
    public static string animBackToMenuTrigger = "BackToMenu";

    public float gameTimeRemaining;
    public float gameStartCountdown;

    // Use this for initialization
    public override void Start () {
        base.Start();
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
	}

    public void InitGame(InitGameEvent.InitGameEventArgs e)
    {        
        // Play music!
        if (MusicManager.GetInstance() != null && MusicManager.GetInstance() != null)
        {
            MusicManager.GetInstance().MenuMusic.Stop();
            MusicManager.GetInstance().GameMusic.Play();
        }

        // Remove the stand in/title screen characters
        DespawnAll();

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
                    Color pColor = GetPlayerColor(i + 1);
                    player.GetComponentInChildren<Text>().enabled = true;
                    player.GetComponentInChildren<Text>().color = pColor;
                    ++hamsterSpawnCount;
                }
                BaseControllable control = player.GetComponent<BaseControllable>();
                control.SetPlayerInputNumber(i + 1);
            }

            // set icons
            playerIcons[i].SetDead(false);
            playerIcons[i].SetPlayerRole(e.playerDataList[i]);
            playerIconPanel.SetActive(true);
        }
        animator.SetTrigger(animStartCountdownTrigger);
    }

    public void DespawnAll()
    {
        // despawn
        GameObject[] hamsters = GameObject.FindGameObjectsWithTag(hamsterPlayer);
        foreach(GameObject hamster in hamsters)
        {
            Destroy(hamster);
        }
        GameObject[] snakes = GameObject.FindGameObjectsWithTag(snakePlayer);
        foreach(GameObject snake in snakes)
        {
            Destroy(snake);
        }

    }

    public void SetSnakePlayerInput(bool enabled)
    {
        GameObject[] snakes = GameObject.FindGameObjectsWithTag(snakePlayer);
        foreach (GameObject snake in snakes)
        {
            if (snake.GetComponent<SnakeGuy>())
            {
                snake.GetComponent<SnakeGuy>().enabled = enabled;
                snake.GetComponent<Animator>().enabled = enabled;
            }
        }
    }

    public bool DidSnakeWin()
    {
        bool snakeWon = false;
        GameObject[] hamsters = GameObject.FindGameObjectsWithTag(hamsterPlayer);
        if(hamsters.Length == 0)
        {
            snakeWon = true;
        }
        return snakeWon;
    }

    public void BackToMenu()
    {
        animator.SetTrigger(animBackToMenuTrigger);
        playerIconPanel.SetActive(false);
    }

    public Color GetPlayerColor(int playerNumber)
    {
        Color pColor = new Color(1, 1, 1);
        switch (playerNumber)
        {
            case 1: // RED
                pColor = Color.red;
                break;
            case 2: // BLUE
                pColor = Color.blue;
                break;
            case 3: // GREEN
                pColor = Color.green;
                break;
            case 4: // YELLOW
                pColor = Color.yellow;
                break;
        }
        return pColor;
    }
}
