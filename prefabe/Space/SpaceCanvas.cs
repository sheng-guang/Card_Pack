using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public enum canvas_kind : int
    {
        middle = 0,
        SkillStack = 1,
        hand = 2,
        Space=3,
    }
    public class SpaceCanvas : MonoBehaviour
    {
        private void Awake()
        {

            if (s_canvas == null) { s_canvas = this; return; }
            if (s_canvas == this) return;
            Destroy(gameObject);
        }

        static SpaceCanvas s_canvas;
        public static SpaceCanvas instance
        {
            get
            {
                if (s_canvas == null) s_canvas = Instantiate(Resources.Load<SpaceCanvas>("[space_canvas]"));
                return s_canvas;
            }
        }
        [Header("middel0_SkillStack1_Hand2_Space3")]
        public List<RectTransform> UISpaces;
        //public SkillLayOutExtra SkillLayout;
        public static void MoveToCanvasSpace(Transform child, canvas_kind k)
        {
            //print(instance);
            Component tr = null;
            int to = (int)k;
            if (to >= 0 && instance.UISpaces.Count > to)
                tr = instance.UISpaces[(int)k];
            if (tr == null) tr = instance;
            child.MoveTransfTo(tr.transform);
            //tr.transform.localPosition = Vector3.zero;
        }
    }
