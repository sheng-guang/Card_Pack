//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CamPoint : MonoBehaviour
//{

//    void Update()
//    {

//        if (Input.GetMouseButtonDown(2)) SetTar();
//    }


//    private void SetTar()
//    {
//        //var en = FormWriter.Ensure;
//        var tar = OnMousePoint.Target.GetData();


//        if (tar.kind>= TargetKind.unit)
//        {
//            var to= tar.LayerId.Value.ToLayer();
//            print("cam set tar " + to);
//            Cam.Tar_realposs = to;
            
//            return;
//        }
//        else if(tar.kind>= TargetKind.point)
//        {
//            var to = v3;
//            v3.RealPoss = tar.Point.Value;
//            Cam.Tar_realposs = v3;
//            print("cam set tar point");
//            return;
//        }
//    }
//    static V3 v3 = new V3();


//}
