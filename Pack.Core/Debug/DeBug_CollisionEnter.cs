using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeBug_CollisionEnter : MonoBehaviour
{
    public bool log = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (log == false) return;
        string re = "[collision]:  ";
        Transform tr = collision.collider.transform;
        while (tr)
        {
            re += tr.name+" | ";
            tr = tr.parent;
        }
        Debug.Log(re);
        //Debug.Log(name+" collision with"+ collision.rigidbody.name);

    }
}
