using System;

public class ChangeMenuEvent {
    
    public static event EventHandler<ChangeMenuEventArgs> eventHandler;
    public static void Register( EventHandler<ChangeMenuEventArgs> callback )
    {
        eventHandler += callback;
    }
    public static void Unregister( EventHandler<ChangeMenuEventArgs> callback )
    {
        eventHandler -= callback;
    }

    public ChangeMenuEvent( string menuName )
    {
        ChangeMenuEventArgs args = new ChangeMenuEventArgs();
        args.MenuName = menuName;

        EventHandler<ChangeMenuEventArgs> handler = eventHandler;
        if( null != handler )
        {
            handler(this, args);
        }
    }

	
    public class ChangeMenuEventArgs : EventArgs
    {
        public string MenuName;
    }
}
