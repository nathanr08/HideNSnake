using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelect : MonoBehaviour {

    public GameObject joinGamePanel;
    public GameObject roleSelectPanel;
    public Button joinGameButton;
    public Button hamsterButton;
    public Button snakeButton;
    public Image confirmImage;

    public Sprite hamsterConfirm;
    public Sprite snakeConfirm;

    public float scrollSpeed = .5f;

    private bool joinedGame;
    private bool finishedSelecting;
    private bool isSnake;
    private float scrollTimer = 0.0f;

    private BaseControllable baseControllable;

    void Start()
    {
        baseControllable = GetComponent<BaseControllable>();

        joinedGame = false;
        finishedSelecting = false;
        isSnake = false;
    }

    // Update is called once per frame
    void Update () {
		if(!joinedGame)
        {
            if(Input.GetButton(baseControllable.InputHandles.Submit))
            {
                joinGameButton.onClick.Invoke();
            }
        }
        else
        {
            if(!finishedSelecting)
            {
                if(scrollTimer < scrollSpeed)
                    scrollTimer += Time.deltaTime;
                if (Input.GetAxis(baseControllable.InputHandles.HorizontalAxis) != 0.0f &&
                    scrollTimer >= scrollSpeed)
                {
                    SwitchRole();
                    scrollTimer = 0.0f;
                }
                if(Input.GetButtonDown(baseControllable.InputHandles.Submit))
                {
                    if (snakeButton.IsActive())
                        snakeButton.onClick.Invoke();
                    else
                        hamsterButton.onClick.Invoke();
                }
                if(Input.GetButtonDown(baseControllable.InputHandles.Cancel))
                {
                    LeaveGame();
                }
            }
            else
            {
                if(Input.GetButtonDown(baseControllable.InputHandles.Cancel))
                {
                    UnConfirmRole();
                }
            }
        }
	}

    /// <summary>
    /// called to add a player to the game
    /// </summary>
    public void JoinGame()
    {
        UISoundManager.GetInstance().ConfirmSound.Play();
        joinedGame = true;
        joinGamePanel.SetActive(false);
        roleSelectPanel.SetActive(true);
        snakeButton.gameObject.SetActive(false);
        hamsterButton.gameObject.SetActive(true);
    }

    /// <summary>
    /// called to have a player leave the game
    /// </summary>
    public void LeaveGame()
    {
        joinedGame = false;
        joinGamePanel.SetActive(true);
        roleSelectPanel.SetActive(false);
    }

    /// <summary>
    /// called to switch between hamster and snake
    /// </summary>
    public void SwitchRole()
    {
        UISoundManager.GetInstance().NavigateSound.Play();
        if (isSnake)
        {
            snakeButton.gameObject.SetActive(false);
            hamsterButton.gameObject.SetActive(true);
        }
        else
        {
            snakeButton.gameObject.SetActive(true);
            hamsterButton.gameObject.SetActive(false);
        }
        isSnake = !isSnake;
    }

    /// <summary>
    /// called when you confirm your role as hamster or snake
    /// </summary>
    public void ConfirmRole()
    {     
        if (isSnake)
        {
            confirmImage.sprite = snakeConfirm;
            UISoundManager.GetInstance().ConfirmSound.Play();
        }
        else
        {
            UISoundManager.GetInstance().CharSelectSound.Play();
            confirmImage.sprite = hamsterConfirm;
        }
        confirmImage.gameObject.SetActive(true);
        finishedSelecting = true;
    }

    public void UnConfirmRole()
    {
        finishedSelecting = false;
        confirmImage.gameObject.SetActive(false);
        JoinGame();
    }

    public bool HasJoinedGame()
    {
        return joinedGame;
    }

    public bool IsReadyToStart()
    {
        bool ready = false;
        if(joinedGame == false)
        {
            ready = true;
        }
        else if(finishedSelecting)
        {
            ready = true;
        }
        return ready;
    }

    public string GetRole()
    {
        string role = "";
        if(IsReadyToStart())
        {
            if(joinedGame == true)
            {
                if (isSnake)
                    role = GameManager.snakePlayer;
                else
                    role = GameManager.hamsterPlayer;
            }
        }
        
        return role;
    }
}
