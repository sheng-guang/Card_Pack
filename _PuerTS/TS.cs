using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pack;
public static class ts
{
    public static Unit CreateUnitAsTemplate(string name)
    {
        var re = Creater<Unit>.GetNew(name);
        return re;
    }
}
