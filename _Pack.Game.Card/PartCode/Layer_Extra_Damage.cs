using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using System.Linq;
using System;

using Pack;

public static partial class eve//damage
{
    public static void DoDamage(Unit u, int damage)
    {
        if (u == null) return;
        if (damage < 0) return;
        if (u.HP.Value.HasValue == false) return;
        u.HP.Value -= damage;
    }
}
