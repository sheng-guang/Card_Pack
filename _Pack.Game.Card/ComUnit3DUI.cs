using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pack;
public class ComUnit3DUI : LayerCompResUI, ICameraFollowe_update
{
    public override void Awake_OnSetMaster()
    {
        base.Awake_OnSetMaster();
        Cam.AddToFollowerList(this);
        unit.Event.StateCall.Listen(OnStateChange);
        center.transform.localPosition=new Vector3(0, unit.UIHeight,0);
    }

    public Transform center;
    public void FollowCamUpdate_()
    {
       center.eulerAngles = Cam.eularangle_xyz();
        transform.position = unit.transf.position;
    }

    public void OnStateChange()
    {
        gameObject.SetActive(unit.State.Value == UnitState.Space);
    }


}
