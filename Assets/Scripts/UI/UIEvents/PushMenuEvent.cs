using System;

public class PushMenuEvent {
    
    public static event EventHandler<PushMenuEventArgs> eventHandler;
    public static void Register( EventHandler<PushMenuEventArgs> callback )
    {
        eventHandler += callback;
    }
    public static void Unregister( EventHandler<PushMenuEventArgs> callback )
    {
        eventHandler -= callback;
    }

    public PushMenuEvent( string menuName )
    {
        PushMenuEventArgs args = new PushMenuEventArgs();
        args.MenuName = menuName;

        EventHandler<PushMenuEventArgs> handler = eventHandler;
        if( null != handler )
        {
            handler(this, args);
        }
    }

	
    public class PushMenuEventArgs : EventArgs
    {
        public string MenuName;
    }
}
