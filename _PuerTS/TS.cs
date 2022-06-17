using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ts
{
    public static Unit CreateUnitAsTemplate(string name)
    {
        var re = Creater<Unit>.GetNew(name);
        return re;
    }
}
