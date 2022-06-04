using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Pack;
public class NetClientTimer : MonoBehaviour
{

    private void OnEnable()
    {
        NetworkClient.RegisterHandler<SyncDataMsgExtra>(ClientGetSyncMsg);
    }
    private void Update()
    {
        UpDate_.Update();

    }
    private void FixedUpdate()
    {
        ApplyDatas();
    }
    void ClientGetSyncMsg(SyncDataMsgExtra d)
    {
        datas.AddLast(d);
    }
    LinkedList<SyncDataMsgExtra> datas = new LinkedList<SyncDataMsgExtra>();

    public void ApplyDatas()
    {
        if (datas.Count != 0) return;
        
        while (datas.Count != 0)
        {
            var to = datas.First.Value;
            datas.RemoveFirst();
            SyncController.ApplySyncData(to.msg);
            if (to.ToSimulate > 0)
            {
                Physics.Simulate(to.ToSimulate);
                Physics.SyncTransforms();
                UpDate_.Update();
            }
            else
            {
                Physics.SyncTransforms();
            }
        }
    }


}
