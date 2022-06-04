    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Pack
{
    public interface IStackSkill
    {
        void SetExistStackAction(Action<object> a);
        void Stack_Start();
        bool Run_ToBreak();
        void SetStackIndex(int index,int total);
    }
    //public interface IStackSkill_Delegate
    //{
    //    SkillNodeGroupOnce A_StackStart { get; set; }
    //    SkillNodeGroupStack A_Run_ToBreak { get; set; }
    //}

}