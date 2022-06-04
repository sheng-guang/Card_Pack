using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pack;
public class AnimatorTimer : MonoBehaviour
{
    static VarChangeEvent<float> speed=new VarChangeEvent<float>();
    public static void SetTimeScale(float s)
    {
       if( speed.NewData(s)==false)return;
        scale = s;
        foreach (var item in timerList)
        {
            item.anim.speed= scale;
        }
    }
    static float scale;
    static List<AnimatorTimer> timerList=new List<AnimatorTimer>();
    public static void AddNewAnimator(AnimatorTimer a)
    {
        timerList.Add(a);
        a.anim.speed = scale;
    }
    
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        if (anim == null) return;
        AddNewAnimator(this);
    }
}
