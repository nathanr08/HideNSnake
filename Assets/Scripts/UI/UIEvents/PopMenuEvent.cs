using System;

public class PopMenuEvent {
    
    public static event EventHandler<PopMenuEventArgs> eventHandler;
    public static void Register( EventHandler<PopMenuEventArgs> callback )
    {
        eventHandler += callback;
    }
    public static void Unregister( EventHandler<PopMenuEventArgs> callback )
    {
        eventHandler -= callback;
    }

    public PopMenuEvent( string menuName )
    {
        PopMenuEventArgs args = new PopMenuEventArgs();

        EventHandler<PopMenuEventArgs> handler = eventHandler;
        if( null != handler )
        {
            handler(this, args);
        }
    }

	
    public class PopMenuEventArgs : EventArgs
    {
    }
}
