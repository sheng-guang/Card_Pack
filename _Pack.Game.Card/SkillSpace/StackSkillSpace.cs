using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public partial class StackSkillSpace : IResGetter<StackSkillSpace>, IRes
    {
        public string DirectoryName => "Assets/Space";

        public string PackName => "Space";
        public string KindName => GetType().Name;
        public StackSkillSpace GetNew(ResArgs a) { return Instantiate(this); }
        public object GetNewObject(ResArgs a) { return GetNew(a); }
    }
    public partial class StackSkillSpace : MonoBehaviour, ICamSpaceObject
    {
        static StackSkillSpace _ins;

        public Transform transf => transform;

        static StackSkillSpace ins()
        {
            if (_ins != null) return _ins;
            _ins = Creater<StackSkillSpace>.GetNew("Space'StackSkillSpace");
            _ins.AddToCamSpace();
            return _ins;
        }
        public static float Scale => ins().transform.localScale.x;
        public static void AddtoStackSpace(Transform t)
        {
            t.MoveTransfTo(ins().root);
        }
        public Transform root;
        public float deepth = 1;
        public float Depth() => deepth;
        public float x = 0.8f;
        public float PossX() => x;
        public float y = 0.5f;
        public float PossY() => y;


    }
