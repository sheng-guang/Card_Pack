using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IStackSkill
{
    void SetExistStackAction(Action<object> a);
    void Stack_Start();
    bool Run_ToBreak();
    void  SetStackIndex(int index, int total);
    void EnsureStackComp();
}

