using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Api]
public static class eveTS
{
    //public static Unit CreateUnitAsTemplate(string name)
    //{
    //    var re = 

    //    return re;
    //}
    public static Unit NewUnitTemplate_ReName(string name,string NewFullName)
    {
        var re = Creater<Unit>.GetNew(name);
        if (re == null) return re;
        (re as ISetFullName).FullName_ = NewFullName;
        return re;
    }
}
