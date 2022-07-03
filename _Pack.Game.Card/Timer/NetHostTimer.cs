using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mirror;
public class NetHostTimer :MonoBehaviour
{

    private void Update()
    {
        UpDate_.Update();
        Cam.UpdatePoss();
    }

    

    public int FixedCount=0;
   
    public float AnimFateTime=1f;
    public float PauseAnimSpeedMax = 0.1f;
    float FateLeft;
    private void FixedUpdate()
    {
        MsgManager.UseAllInput();
        var StackIsEnd = GameLogicFunctions.DoStack_IsEnd(out var count);

        if (count != 0) AfterSimulate.Update();
        if (StackIsEnd)
        {
            FateLeft = AnimFateTime;
            AnimatorTimer.SetTimeScale(1);

        }
        else
        {
            FateLeft -= Time.deltaTime;
            if(FateLeft < 0) FateLeft = 0;
            float percent = FateLeft / AnimFateTime;
            AnimatorTimer.SetTimeScale(PauseAnimSpeedMax *percent);
        }

        if (StackIsEnd)
        {
            bool is50 = TimeSetting.Test_and_UpdateTime(ref FixedCount);
            GameLogicFunctions.DoSkillList(is50);

            SendMsg(TimeSetting.FixedDeltaTime);
            Physics.Simulate(TimeSetting.FixedDeltaTime);
            Physics.SyncTransforms();
            BuffSys.Fresh();
            CallSys.DoCall(new Call(CallKind.Destory));

            AfterSimulate.Update();
            GameTime.AddTime(TimeSetting.FixedDeltaTime);

        }
        else
        {

            Physics.SyncTransforms();
            SendMsg(-1);
        
        }


    }
    public bool SendMag = true;
    public void SendMsg(float ToSimulate)
    {
        if (SendMag == false) return;
        SyncDataMsgExtra ChangedDatas = new SyncDataMsgExtra();
        ChangedDatas.msg = SyncController.GetAllChangedData();
        ChangedDatas.ToSimulate = ToSimulate;

        NetworkServer.SendToAll(ChangedDatas);
    }

}

public struct SyncDataMsgExtra : NetworkMessage
{
    public float ToSimulate;
    public SyncDataMsg msg;
}
