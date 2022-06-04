using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pack
{


    //stack
    public class SkillNodeGroupStack : SkillNodeGroup
    {
        public virtual bool Invoke()
        {
            if (OnPoint == null) { return false; }
            bool ToBreak = false;
            int count = 0;
            while (OnPoint != null)
            {

                var Result = OnPoint.Fix_1Exit_2ToNext_4Break();
                ToBreak = Result.MaskContain(4);
                if (Result.MaskContain(1)) { Exist(); break; }
                if (Result.MaskContain(2)) { ToNext(); }
                if (ToBreak) break;
                if (count >= 5) Debug.Log(OnPoint +"[ not null: "+OnPoint!=null);
                if (++count % 30 == 0)
                { Debug.Log("error: SkillNode Over 30 node: "+OnPoint+"  skill: "+ self + "  count:"+count+" result:"+ Result); break; }
            }

            return ToBreak;
            //防止没exist卡住
        }
        public virtual void ToNext()
        {
            if (OnPoint != null) OnPoint = OnPoint.Next;
            if (OnPoint == null)
            {
                Exist();
            }
        }


    }


    //long skill
    public class SkillNodeGroupStep : SkillNodeGroup
    {

        public virtual void Invoke()
        {
            while (OnPoint != null)
            {
                if (OnPoint == null) break;
                //Debug.Log(OnPoint);
                var option = OnPoint.Fix_1Exit_2ToNext_4Break();
                if (option.MaskContain(1)) { Exist(); break; }
                if (option.MaskContain(2)) { ToNext(); }
                if (option.MaskContain(4)) break;
            }
        }
        void ToNext()
        {
            if (OnPoint != null) OnPoint = OnPoint.Next;
            if (OnPoint == null)
            {
                Exist();
            }
        }
    }
    //becall skill
    public class SkillNodeGroupLoop : SkillNodeGroup
    {
        public virtual void Invoke()
        {
            OnPoint = First;
            while (OnPoint != null)
            {
                if (OnPoint == null) break;
                //Debug.Log(OnPoint);
                var option = OnPoint.Fix_1Exit_2ToNext_4Break();
                //todo 为什么下一行会被注释
                //if (option.MaskContain(1)) { Exist(); }
                if (option.MaskContain(2)) { ToNext(); }
                if (option.MaskContain(4)) break;
            }
        }
        void ToNext()
        {
            if (OnPoint != null) OnPoint = OnPoint.Next;
            if (OnPoint == null)
            {
                Exist();
            }
        }



    }

    //Once  skill
    public class SkillNodeGroupOnce : SkillNodeGroup
    {
        public virtual void Invoke()
        {
            while (OnPoint != null)
            {
                var re = OnPoint.Fix_1Exit_2ToNext_4Break();
                if (re.MaskContain(1)) { Exist(); break; }
                if (re.MaskContain(2)) { ToNext(); }
            }
            //Debug.Log("Once skill   [" + self + "]  exist");
        }
        public void ToNext()
        {
            if (OnPoint != null) OnPoint = OnPoint.Next;
        }
    }



}
