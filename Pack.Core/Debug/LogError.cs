using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogError : MonoBehaviour
{
    //[RuntimeInitializeOnLoadMethod]
    //static void CreatMainLoop()
    //{
    //    var ne = new GameObject();
    //    ne.AddComponent<LogError>();
    //    DontDestroyOnLoad(ne);
    //    ne.name = "[LogError]";
    //}

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Z)) Debug.LogError(1);
        //if (Input.GetMouseButtonDown(0)) Debug.LogError(0);
    }
}
