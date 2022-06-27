using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphSetting : MonoBehaviour
{
   
    public int targetFrameRate = 30;
    private void Start()
    {

        Fresh();
    }
    [ContextMenu("fresh")]
    public void Fresh()
    {
        Application.targetFrameRate = targetFrameRate;
    }
}
