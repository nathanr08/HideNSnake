using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    Stack<string> menuStack;
    MusicManager musicManager;


    // Use this for initialization
    void Start()
    {
        menuStack = new Stack<string>();
        musicManager = MusicManager.GetInstance();

        if (musicManager != null && musicManager.MenuMusic != null)
        {
            musicManager.MenuMusic.Play();
        }
        PushMenuEvent  .Register(PushMenuEventHandler  );
        PopMenuEvent   .Register(PopMenuEventHandler   );
        ChangeMenuEvent.Register(ChangeMenuEventHandler);
        InitGameEvent  .Register(InitGameEventHandler  );
        new PushMenuEvent( "mainMenu" );
    }

    public void PushMenuEventHandler(object sender, PushMenuEvent.PushMenuEventArgs e)
    {
        if( menuStack.Count > 0 )
            SceneManager.UnloadSceneAsync( menuStack.Peek() );
        menuStack.Push( e.MenuName );
        SceneManager.LoadSceneAsync(e.MenuName, LoadSceneMode.Additive);
    }
    public void PopMenuEventHandler(object sender, PopMenuEvent.PopMenuEventArgs e)
    {
        SceneManager.UnloadSceneAsync( menuStack.Pop() );
        if( menuStack.Count > 0 )
            SceneManager.LoadSceneAsync( menuStack.Peek(), LoadSceneMode.Additive );
    }
    public void ChangeMenuEventHandler( object sender, ChangeMenuEvent.ChangeMenuEventArgs e )
    {
        if( menuStack.Count > 0)
        {
            SceneManager.UnloadSceneAsync( menuStack.Pop() );
            menuStack.Clear();
        }
        if( "" != e.MenuName )
        {
            menuStack.Push( e.MenuName );
            SceneManager.LoadSceneAsync( e.MenuName, LoadSceneMode.Additive );
        }
    }
    public void InitGameEventHandler( object sender, InitGameEvent.InitGameEventArgs e )
    {
        if (musicManager != null && musicManager.MenuMusic != null)
        {
            musicManager.MenuMusic.Stop();
        }

        if (musicManager != null && musicManager.GameMusic != null)
        {
            musicManager.GameMusic.PlayDelayed(1f);
        }
        GameManager.Instance.InitGame(e);     
    }
}
