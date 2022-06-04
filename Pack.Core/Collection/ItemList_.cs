using Pack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemList_ : MonoBehaviour
{
    ItemList master;
    public void SetData(ItemData d,ItemList master)
    {
        this.master = master;
        ResName.text = d.FullName;
        
    }
    public Text ResName;
    public override string ToString()
    {
        return name;
    }
}



