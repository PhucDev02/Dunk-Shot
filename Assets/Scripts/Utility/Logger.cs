
using UnityEngine;

public class Logger
{
    // Start is called before the first frame update
    public static bool enable = true;
    public static void Log(string message)
    {
        if (enable)
            Debug.Log(message);
    }
    public static void Warning(string message)
    {
        if (enable)
            Debug.Log(message);
    }
}
