using System;
using System.Collections.Generic;

public class InitGameEvent {
    
    public static event EventHandler<InitGameEventArgs> eventHandler;
    public static void Register( EventHandler<InitGameEventArgs> callback )
    {
        eventHandler += callback;
    }
    public static void Unregister( EventHandler<InitGameEventArgs> callback )
    {
        eventHandler -= callback;
    }

    public InitGameEvent( List<string> playerDataList )
    {
        InitGameEventArgs args = new InitGameEventArgs();
        args.playerDataList = playerDataList;

        EventHandler<InitGameEventArgs> handler = eventHandler;
        if( null != handler )
        {
            handler(this, args);
        }
    }

	
    public class InitGameEventArgs : EventArgs
    {
        public List<string> playerDataList;
    }
}
