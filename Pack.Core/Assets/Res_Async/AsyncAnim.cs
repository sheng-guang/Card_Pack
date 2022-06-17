using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class AsyncAnim : MonoBehaviour, IOnAsyncLoaded, IBeforeAsPrefabe
    {
        public Animator OriginAnim;
        public Animator AnimCopy;
        public string ChildPath;

        public Animator CurrentAnim => OriginAnim ?? AnimCopy;

        public void BeforeAsPrefabe1(IRes res)
        {
            if (res == this.getFirstComponetInParent<IRes>()) Copy();
        }
        public void BeforeAsPrefabe2(IRes res)
        {
            if (res == this.getFirstComponetInParent<IRes>() == false) return;
        }

        [ContextMenu("copy")]
        public void Copy()
        {
#if UNITY_EDITOR
            if (OriginAnim == null) { Debug.Log("OnAsPrefabe    No    Origin Animator"); return; }
            if (AnimCopy == null) { Debug.Log("OnAsPrefabe    No   Copy Animator"); return; }
            ChildPath = transform.GetChildrenPath(OriginAnim);
            ForAnimator.CopyAnimator(OriginAnim, AnimCopy);

#endif
        }



        public static void CopyAnimatorState(Animator old, Animator ne)
        {
            for (int i = 0; i < old.layerCount; i++)
            {
                var info = old.GetCurrentAnimatorStateInfo(i);
                ne.Play(info.fullPathHash, i, info.normalizedTime);
            }
            
        }
        public static void Copyparameters(Animator old, Animator ne)
        {
            var to = old.parameters;
            foreach (var item in to)
            {
                switch (item.type)
                {
                    case AnimatorControllerParameterType.Float:
                        ne.SetFloat(item.name,old.GetFloat(item.name));
                        break;

                    case AnimatorControllerParameterType.Int:
                        ne.SetInteger(item.name, old.GetInteger(item.name));
                        break;

                    case AnimatorControllerParameterType.Bool:
                        ne.SetBool(item.name, old.GetBool(item.name));
                        break;
                }
            }

        }

        public void OnLoaded()
        {
            if (AnimCopy == null) return;
            var TarTrans = transform.Find(ChildPath);
            OriginAnim = TarTrans.GetComponent<Animator>();
            if (OriginAnim == null) return;
            CopyAnimatorState(AnimCopy, OriginAnim);
            Copyparameters(AnimCopy, OriginAnim);
            OriginAnim.Update(0);
        }


    }
