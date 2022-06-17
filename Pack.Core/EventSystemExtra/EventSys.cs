using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


    public static class OnMousePoint
    {
        class Empty : ITarget, IInputData
        {
            public N<Vector3> Point => null;
            public N<int> LayerId => null;
            public IInputData GetData() { return this; }
        }
        static Empty empty = new Empty();

        public static ITarget Target { get; private set; } = empty;
        public static void SetTarget(ITarget t) { Target = t; if (t == null || t.Equals(null)) Target = empty; }


        public static void Fresh3DPoint()
        {
            RaycastHit hit = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool hited = Physics.Raycast(ray, out hit);
            if (hited)
            {
                Mouse3DPosition = hit.point;
                Mouse3DPositionReal = hit.point;

            }
            else
            {

                Mouse3DPosition = null;
                Mouse3DPositionReal = null;
            }
            //EventSys.Fresh(0);
        }
        public static N<Vector3> Mouse3DPosition { get; set; }
        public static N<Vector3> Mouse3DPositionReal { get; set; }

    }

    public class EventSys : StandaloneInputModule
    {
        [RuntimeInitializeOnLoadMethod]
        public static void Load()
        {
            GameObject go = new GameObject();
            ins = go.AddComponent<EventSys>();
            go.transform.SetAsFirstSibling();
            go.name = "[EventSys]";
        }
        public static EventSys ins;
        public static GameObject OnPoint;
        public static N<Vector3> OnPointV3;

        public static int DownKind;
        public static void Fresh(int Kind)
        {
            PointerEventData lastPointer = ins.GetLastPointerEventData(kMouseLeftId);
            var hit = lastPointer.pointerCurrentRaycast;
            if (lastPointer.enterEventCamera != null)
            {

                OnPoint = hit.gameObject;
                OnPointV3 = hit.worldPosition;
            }
            else
            {
                OnPoint = null;
                OnPointV3 = null;
            }
            
        }

    }

