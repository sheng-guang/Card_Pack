#if MIRROR

using System;
using Mirror;

namespace Pack
{
    public static class ServerEvent
    {
        public static Action<NetworkConnection> OnConnectionConnected;

        public static void Event_OnSConnected(this NetworkConnection c)
        {
            OnConnectionConnected?.Invoke(c);
        }
    }
}
#endif