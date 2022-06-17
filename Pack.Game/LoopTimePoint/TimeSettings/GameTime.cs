using UnityEngine.VFX;

    public static class GameTimeExtra
    {
        public static void FreshGameTime(this VisualEffect e)
        {
            e.SetFloat("GameTime", GameTime.Time);
        }
    }

    public  static partial class GameTime
    {
        static GameTime()
        {
            NewGameClear.AddToNewGameClearList(clear);
        }
        static void clear()
        {
            Time = 0;
        }
        public static float Time { get; private set; } =0;
        public static void AddTime(float f)
        {
            Time += f;
        }
    }


    public static partial class GameTime
    {
        public static BuffSysMonitor<float> TimeMonitor
= new BuffSysMonitor<float>(nameof(GameTime.Time), () => Time);
    }

    
