#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;


    public class ForAnimator
    {
        public static void CopyAnimator(Animator old,Animator ne)
        {
            var oldController = old.runtimeAnimatorController as AnimatorController;
            ne.runtimeAnimatorController = CopyController(oldController);

            ne.avatar = old.avatar;
            ne.applyRootMotion = old.applyRootMotion;
            ne.cullingMode = old.cullingMode;
            ne.updateMode = old.updateMode;

            EditorUtility.SetDirty(ne.runtimeAnimatorController);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

        }
        public static AnimatorController CopyController(AnimatorController old)
        {
            var oldPath = AssetDatabase.GetAssetPath(old);
            //print(oldPath);
            var newPath = oldPath.Replace(".", ".copy.");
            AssetDatabase.CopyAsset(oldPath, newPath);
            var ne = AssetDatabase.LoadAssetAtPath<AnimatorController>(newPath);
            for (int i = 0; i < ne.layers.Length; i++)
            {
                CopyStateMachine(old.layers[i].stateMachine, ne.layers[i].stateMachine);
            }
            return ne;
        }

        public static void CopyStateMachine(AnimatorStateMachine old, AnimatorStateMachine ne)
        {
            for (int i = 0; i < old.stateMachines.Length; i++)
            {
                CopyStateMachine(old.stateMachines[i].stateMachine, ne.stateMachines[i].stateMachine);
            }
            for (int i = 0; i < old.states.Length; i++)
            {
                AnimatorState old_ = old.states[i].state;
                AnimatorState ne_ = ne.states[i].state;
                //Debug.Log("motion    " + old_.name);
                ne_.motion = CopyMotion(old_.motion, ne_.motion);
            }
        }
        public static Motion CopyMotion(Motion old, Motion ne)
        {
            if (old is AnimationClip) return CopyClip(old as AnimationClip);
            else if (old is BlendTree) CopyBlendTree(old as BlendTree, ne as BlendTree);
            return ne;
        }

        public static void CopyBlendTree(BlendTree old, BlendTree ne)
        {
            var NeList = ne.children;
            var OldList = old.children;
            for (int i = 0; i < OldList.Length; i++)
            {
                NeList[i].motion = CopyMotion(OldList[i].motion, NeList[i].motion);
            }
            ne.children = NeList;
        }
        public static AnimationClip CopyClip(AnimationClip old)
        {
            AnimationClipSettings clipSetting = AnimationUtility.GetAnimationClipSettings(old);
            string ResName = null;
            AnimationClip re = new AnimationClip();
            string PathStart = "Assets/Pack/_LogicAnims/";
            if (old.events.Length == 0)
            {
                ResName = old.length + "-" + clipSetting.loopTime + "-";
                var full = PathStart + ResName + ".anim";
                re = AssetDatabase.LoadAssetAtPath<AnimationClip>(full);
                if (re != null) return re;
                re = new AnimationClip();
                re.name = ResName;
                re.frameRate = old.frameRate;
                //float s = clipSetting.startTime;
                //float e = clipSetting.stopTime;
                re.SetCurve("-", typeof(ResTool), "-", new AnimationCurve(new Keyframe(0, 0), new Keyframe(old.length, 0)));
                AnimationUtility.SetAnimationClipSettings(re, clipSetting);
                AssetDatabase.CreateAsset(re, full);
                return re;
            }
            else
            {

            }


            return re;
        }
    }
#endif