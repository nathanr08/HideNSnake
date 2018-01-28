using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class MainMenu : MonoBehaviour {

    static string mainMenuScene = "mainMenu";
    static string setupScene = "setup";

    public Button startGameButton;
    public Button exitButton;

    public Button highlightedButton;

    public BaseControllable controllable;

    public float selectTime = .5f;
    public float selectTimer = 0.0f;

    public void Start()
    {
        controllable = GetComponent<BaseControllable>();
        highlightedButton = startGameButton;
    }

    public void Update()
    {
        //if(selectTimer <= selectTime)
        //{
        //    selectTimer += Time.deltaTime;
        //}

        //if (Input.GetAxis(controllable.InputHandles.VerticalAxis) != 0.0f &&
        //    selectTimer >= selectTime)
        //{
        //    selectTimer = 0.0f;
        //    if(highlightedButton == startGameButton)
        //    {
        //        highlightedButton = exitButton;
        //    }
        //    else
        //    {
        //        highlightedButton = startGameButton;
        //    }
        //}
        //else
        //{
        //    if(Input.GetButtonDown(controllable.InputHandles.Submit))
        //    {
        //        highlightedButton.onClick.Invoke();
        //    }
        //}

        //highlightedButton.Select();
    }

    public void StartButtonClicked()
    {
        new PushMenuEvent("setup");
    }

    public void QuitButtonClicked()
    {
        Application.Quit();
    }
}
