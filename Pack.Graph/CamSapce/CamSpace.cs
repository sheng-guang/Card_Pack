using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public partial class CamSpace : MonoBehaviour//ins
    {
        static CamSpace _Ins { get; set; }
        public static CamSpace Ins
        {
            get
            {
                if (_Ins == null)
                {
                    print("Resources.Load [space_camera]");

                    _Ins = Instantiate(Resources.Load<CamSpace>("[space_camera]"));
                    _Ins.AddToCamFollowerList(true);
                    //print("create  CamSpace");
                }
                return _Ins;
            }
        }
    }
    public partial class CamSpace//follower (static list)
    {
        public static void AddFollower(ICamSpaceObject follower)
        {
            follower.transf.MoveTransfTo(Ins.transform);
            ObjList.Add(follower);
            //NewAdded.Add(follower);
        }
        static List<ICamSpaceObject> ObjList = new List<ICamSpaceObject>();
        //static List<ICamSpaceObject> NewAdded = new List<ICamSpaceObject>();
    }

    public partial class CamSpace : ICameraFollowe_update//fresh
    {
        public static Camera UIcamera => Cam.instance ? Cam.instance.MainCamera : null;
        public void FollowCamUpdate_()
        {
            {
                if (UIcamera == null) return;
                transform.position = UIcamera.transform.position;
                transform.rotation = UIcamera.transform.rotation;
            }
            FreshData_isNew();
            FreshAll();
        }

        VarChangeEvent<float> fovEvent = new VarChangeEvent<float>();
        //VarChangeEvent<float> deepthEvent = new VarChangeEvent<float>();
        VarChangeEvent<float> w_hEvent = new VarChangeEvent<float>();

        bool FreshData_isNew()
        {
            float UIFov = UIcamera.fieldOfView;
            bool NewData = false;
            fovEvent.NewData(UIFov, ref NewData);
            w_hEvent.NewData(1f * Screen.width / Screen.height, ref NewData);
            if (NewData == false) return false;
            //---------------------------------------------------------------------------
            var w_h = 1f * Screen.width / Screen.height;
            var Tan_Height = Mathf.Tan((UIFov / 180 * Mathf.PI) / 2);
            H_when_deep1 = Tan_Height * 1 * 2;
            W_when_deep1 = H_when_deep1 * w_h;
            return true;
        }
        float W_when_deep1 = 1;
        float H_when_deep1 = 1;
        public void FreshAll()
        {
            for (int i = 0; i < ObjList.Count; i++) FreshOne(ObjList[i]);
        }
        public float SpaceHeigth = 9f;
        public float SpaceWidth = 16f;
        public void FreshOne(ICamSpaceObject to)
        {
            Vector3 LocalPoss = Vector3.zero;
            LocalPoss.z = to.Depth();
            float height = H_when_deep1 * LocalPoss.z;
            float width = W_when_deep1 * LocalPoss.z;

            LocalPoss.x = width * (to.PossX() - 0.5f);
            LocalPoss.y = height * (to.PossY() - 0.5f);
            to.transf.localPosition = LocalPoss;
            to.transf.localScale = Vector3.one * (width / SpaceWidth);
        }

    }
    public partial class CamSpace //static func
    {
        public static float GetWidthAtPoint(Vector3 RealWorldPoss,float w)
        {
            var localPoss= UIcamera.transform.InverseTransformPoint(RealWorldPoss);
            float fullwidth = Ins.W_when_deep1 * localPoss.z;
            return  fullwidth / Ins.SpaceWidth*w;
        }
    }

    public static class CamSpaceExtra
    {
        public static void AddToCamSpace(this ICamSpaceObject o)
        {
            CamSpace.AddFollower(o);
        }
    }

