using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pack;
public class Timer : MonoBehaviour
{
    private void Awake()
    {
        Physics.autoSimulation = false;
        Physics.autoSyncTransforms = false;


        hostTimer.enabled = false;
        ClientTimer.enabled = false;
    }
    public NetHostTimer hostTimer;
    public NetClientTimer ClientTimer;

    public void AsHost()
    {
        hostTimer.enabled = true;
        ClientTimer.enabled = false;
    }
    //public static void 
    public void AsClient()
    {
        ClientTimer.enabled = true;
        hostTimer.enabled = false;
    }


}
