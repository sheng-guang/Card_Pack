using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSpaceTest : MonoBehaviour, ICamSpaceObject
{
    private void Awake()
    {
        this.AddToCamSpace();
    }
    public Transform transf => transform;
    public float x=0.8f;
    public float PossX() => x;
    public float y=0.5f;
    public float PossY() => y;

    public float Depth_=1;
    public float Depth() { return Depth_; }

    public LineRenderer render;
    public void Fresh()
    {
        if (render == null) return;

    }

    //public Vector3[] posses=new Vector3[2];
    //public Vector3 poss1;
    //public Vector3 poss2;
    //public void OnValidate()
    //{
    //    posses[0] = poss1;
    //    posses[1] = poss2;
    //    render.SetPositions( posses);
    //}

}
//    if(test!=null)
//    {
//        test.poss1 = SpaceRoot.position;
//        var to= UIcamera.transform.InverseTransformPoint(tarV3);
//        float fovRMain = cam.MainCam.fieldOfView / 180 * Mathf.PI;
//        float scale= Mathf.Tan(angle / 2)/Mathf.Tan(fovRMain / 2) ;
//        to.x *= scale; 
//        to.y *= scale;
//        var to_world = UIcamera.transform.TransformPoint(to);
//        test.poss2=to_world;
//        test.OnValidate();
//    }