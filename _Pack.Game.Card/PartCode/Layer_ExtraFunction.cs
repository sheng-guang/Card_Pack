using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using System.Linq;
using System;
using Pack;

partial class eve//根据id获取玩家
{
    public static Player getPlayer_offSet(int baseID, int offset)
    {
        int which = -1;
        for (int i = 0; i < GameList.playerList.Count; i++)
        {
            var to = GameList.playerList[i];
            if (to.ID != baseID) continue;
            which = i;
            break;
        }
        if (which == -1) return null;
        int toIndex = which + offset;
        while (toIndex >= GameList.playerList.Count)
        {
            toIndex -= GameList.playerList.Count;
        }
        return GameList.playerList[toIndex];
    }
}

partial class eve//向量计算
{
    public static Vector3 GetMoveVector(Player p, int ws, int da)
    {
        var right = p.CamFoward.Value.GetRight_normal();
        var foward = p.CamFoward.Value.SetY(0).normalized;
        var re = right * da + foward * ws;
        return re.normalized;
    }
    public static float Distance(this LayerID a, LayerID b)
    {
        return Vector3.Distance(a.RealPoss, b.RealPoss);
    }
}
partial class eve//物理计算
{
    //phy
    static Collider[] c = new Collider[1024];
    public static void OverlapUnit(Vector3 point, float r, Action<Unit> WriteResult)
    {
        int count = Physics.OverlapSphereNonAlloc(point, r, c);
        if (count >= 1024) Debug.LogWarning("overlap Out of index  now count" + count);
        for (int i = 0; i < count; i++)
        {
            var Getter = c[i].ToUnit();
            //todogc
            if (Getter.IsNull_or_EqualNull()) continue;
            WriteResult(Getter.u);
        }
    }

    public static void OverLapCollider(Vector3 point, float r, Action<Collider> WriteResult)
    {
        int count = Physics.OverlapSphereNonAlloc(point, r, c);
        if (count >= 1024) Debug.LogWarning("overlap Out of index  now count" + count);
        for (int i = 0; i < count; i++)
        {
            var coll = c[i];
            WriteResult(coll);
        }
    }

}
partial class eve//修改玩家属性
{
    //player
    public static void SetHero(Player p, Unit u)
    {
        if (u.CanBeHero == false) return;
        p.MainHero = u.CanBeHero;
    }
}
partial class eve//修改unit 属性
{
   //unit
    public static void UseMana(Unit u, Player p)
    {
        if (u.ManaCost.Value_Buffed.HasValue == false) return;
        p.Mana.Value -= u.ManaCost.Value_Buffed.Value;
    }
    public static void SetState(Unit u, int s)
    {
        u.State.Value = s;
    }
    public static void SetSpace(Unit u, int toSpace)
    {
        int lastSpace = u.Space.Value;
        int State = u.State.Value;

        if (lastSpace == toSpace) return;
        if (toSpace == UnitSpace.Hand && State == UnitSpace.Hand)
        {
            Debug.LogWarning(u.name + " not card but in hand");
            return;
        }
        u.up.RemoveUnitFollower(u);
        if (toSpace == UnitSpace.Hand || toSpace == UnitSpace.Space)
            GameList.RemoveFromCallList(u);
        if (lastSpace == UnitSpace.Space) 
            GameList.RemoveFromSpaceList(u);

        u.Space.Value = toSpace;

        if (toSpace == UnitSpace.Hand || toSpace == UnitSpace.Space) 
            GameList.AddToCallList(u);
        if (toSpace == UnitSpace.Space) 
            GameList.AddToSpaceList(u);

        u.up.AddUnitFollower(u);
    }
    public static void SetUP(Unit u, LayerID l)
    {
        u.up.RemoveUnitFollower(u);
        u.up = l;
        l.AddUnitFollower(u);
    }

    public static void AddPoss(Unit u, Vector3 poss)
    {
        u._SetPoss(u.RealPoss + poss);
    }
    public static void SetPoss(Unit u, Vector3 poss)
    {
        u._SetPoss(poss);
    }
    public static void SetFoward(Unit u, Vector3 f)
    {
        u.transf.forward = f;
    }
    public static void SetV(Unit u, Vector3 v)
    {
        u.rig.velocity = v;
    }
}
partial class eve//初始化
{  
    //layer
    public static void AwakeLoad(LayerID l)
    {
        if (l.AsIAfterSimulate.NotNull_and_NotEqualNull())
        {
            AfterSimulate.collection.AddToList(l.AsIAfterSimulate);
        }
    }

}
partial class eve//遍历 unity
{ 
    //loop
    //todo use job system
    public static void ForEachCallListUnit(Action<Unit> act)
    {
        GameList.ForEachCallList(act);
    }

}


public static partial class eve//状态判断 关系判断                 isTest
{
    public static bool IsFollowing(this Unit u)
    {
        return u.up != u.player && u.up != GameList.NowHost;
    }
    public static bool IsCardInHand(this Unit u)
    {
        if (u == null) return false;
        return u.State.Value == UnitState.Card && u.Space.Value == UnitSpace.Hand;
    }
    public static bool IsSpaceInSpace(this Unit u)
    {
        if (u == null) return false;
        return u.State.Value == UnitState.Space && u.Space.Value == UnitSpace.Space;
    }
    public static bool IsSpace(this Unit u, int space)
    {
        if (u == null) return false;
        return u.Space.Value == space;
    }
    public static bool IsState(this Unit u, int state)
    {
        if (u == null) return false;
        return u.State.Value == state;
    }
    public static bool IsFriend(this LayerID l1, LayerID l2)
    {
        if (l1 == null || l2 == null) return false;
        return l1.player == l2.player;
    }

}


