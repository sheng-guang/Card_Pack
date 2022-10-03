
using System;
using Mirror;


    public static class ServerEvent
    {
        public static Action<NetworkConnection> OnConnectionConnected;

        public static void Event_OnSConnected(this NetworkConnection c)
        {
            OnConnectionConnected?.Invoke(c);
        }
    }
