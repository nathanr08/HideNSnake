using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    Stack<string> menuStack;

    // Use this for initialization
    void Start()
    {
        PushMenuEvent  .Register(PushMenuEventHandler  );
        PopMenuEvent   .Register(PopMenuEventHandler   );
        ChangeMenuEvent.Register(ChangeMenuEventHandler);
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
        menuStack.Push( e.MenuName );
        SceneManager.LoadSceneAsync( e.MenuName, LoadSceneMode.Additive );
    }
}
