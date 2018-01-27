using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MainMenu : MonoBehaviour {

    static string mainMenuScene = "mainMenu";
    static string setupScene = "setup";

    public void StartButtonClicked()
    {
        SceneManager.LoadScene(setupScene);
    }

    public void QuitButtonClicked()
    {
        Application.Quit();
    }
}
