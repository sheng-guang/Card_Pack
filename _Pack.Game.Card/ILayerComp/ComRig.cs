using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pack;
[RequireComponent(typeof(Rigidbody))]
public class ComRig : LayerComp
{
    Rigidbody rig;
    public override void Awake_OnSetMaster()
    {
        base.Awake_OnSetMaster();
        rig = GetComponent<Rigidbody>();
        unit.Event.SpaceCall.Listen(FreshSpace);
        unit.Event.UPIDCall_IsFollowingCall.Listen(FreshSpace);
        rig.centerOfMass = Vector3.zero;
    }
    [Header("Poss[2,4,8|14]   Ro[16,32,64|112]   [126]")]
    public int UnLock ;
    public int Locked=126;


    public void FreshSpace()
    {
        if (unit.Space.Value == UnitSpace.Space && unit.IsFollowing() == false) rig.constraints =(RigidbodyConstraints)UnLock;
        else rig.constraints = (RigidbodyConstraints)Locked;
    }




}
