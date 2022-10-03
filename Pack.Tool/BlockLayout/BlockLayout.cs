using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockLayout : MonoBehaviour
{
    public Transform pre;
    public List<Transform> created;
    public int x;
    public int z;
    public float width=1;
    public IEnumerator des(GameObject go)
    {
        yield return 1;
        DestroyImmediate(go);
    }
    [ContextMenu("ClearAll")]

    public void ClearAll()
    {
        foreach (var item in created)
        {
            StartCoroutine(des(item.gameObject));
        }
        created.Clear();
    }
    [ContextMenu("create")]
    public void Create()
    {
        //pre.gameObject.SetActive(false);
        ClearAll();
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < z; j++)
            {
               var ne= Instantiate(pre,transform);
                created.Add(ne);
                ne.localPosition = new Vector3(i * width, 0, j * width);
                ne.gameObject.SetActive(true);

            }
        }
    }
}
