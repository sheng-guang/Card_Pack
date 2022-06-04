using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pack;
using UnityEngine.UI;
using System;
public class ItemList : MonoBehaviour
{
    private void Awake()    {        pre.gameObject.SetActive(false);    }

    private void OnValidate()
    {
        {
            var ret = pre.GetComponent<RectTransform>();
            ret.sizeDelta = cellSize;
        }
        //foreach (var item in CreatedList)
        //{
        //    var ret = item.GetComponent<RectTransform>();
        //    ret.sizeDelta = cellSize;
        //}
        //group.cellSize = cellSize;
        //group.spacing = spacing;
        //group.padding.top = spacing.y;
        //group.padding.left = spacing.x;
    }
     List<ItemList_> CreatedList = new List<ItemList_>();

    public ItemList_ pre;

    public Vector2Int cellSize = new Vector2Int(120, 120);
    public Vector2Int spacing = new Vector2Int(5, 5);

    public RectTransform Content;
    public int OneLineCount=3;
    public int LineH => cellSize.y + spacing.y;
    public int ColemW => cellSize.x + spacing.x;
    public int ShowLine = 15;
    public int ShowLineUp=5;
    public void Update()
    {
        int possY = (int)Content.localPosition.y;
        int HideLineCount = possY / LineH;

        NowStartIndex = HideLineCount * OneLineCount - ShowLineUp * OneLineCount;

        NowEndIndex = HideLineCount * OneLineCount + ShowLine * OneLineCount - 1;
        DB<ItemData>.Datas().FixWindowIndex(ref NowStartIndex,ref NowEndIndex);

        Created.RollTo(NowStartIndex, NowEndIndex, CreatNew,SetIndex,Remove);
        int LastLineCount = (NowEndIndex + 1) % OneLineCount;
        int LineCount = (NowEndIndex + 1) / OneLineCount+(LastLineCount==0?0:1);
        int yl = LineCount * LineH;
        //Debug.Log(LineCount);
        //Debug.Log(LineCount * LineH);
        //Content.offsetMax = new Vector2(OneLineCount * ColemW + spacing.x, 0);
        Content.offsetMin = new Vector2(0,-(LineCount * LineH-possY));
    }
    public int NowStartIndex = 0;
    public int NowEndIndex = 0;
    public IndexRoundList<ItemList_> Created = new IndexRoundList<ItemList_>();

    public ItemList_ CreatNew()
    {
        var ne = Instantiate(pre, pre.transform.parent);
        ne.gameObject.SetActive(true);
        ne.name = "";
        CreatedList.Add(ne);
        return ne;

    }
    //index             0 1 2   3 4 5   6 7
    //Inlineindex  0 1 2   0 1 2   0 1
    //Line index    0 0 0 1  1  1   2 2
    public void SetIndex(ItemList_ l,int index)
    {
        int InLineIndex = (index) % OneLineCount;
        int LineIndex = (index ) / OneLineCount;
        //Debug.Log("index " + index + " inline " + InLineIndex + "   line index " + LineIndex);
        l.transform.localPosition = new Vector3(spacing.x + InLineIndex*ColemW, -spacing.y - LineIndex*LineH);
        l.gameObject.SetActive(true);
        l.name=""+ index;
        var d =DB<ItemData>.Datas().GetData(index);
        l.SetData(d, this);
    }

    public void Remove(ItemList_ l)
    {
        Debug.Log("remove");
        Destroy(l.gameObject);
        CreatedList.Remove(l);
        //l.gameObject.SetActive(false);
    }
}

public class ItemData : IDBdata<ItemData>
{
    public string DataKind => "Res";

    public string FullName { get; set; }
    public string Line { get; set; }
    public static ItemData Creat(string s)
    {
        if (s.StartsWith("//")) return null;
        if (string.IsNullOrWhiteSpace(s)) return null;

        var re = new ItemData();
        re.Line = s;
        var ss = s.Split_();
        try
        {
            re.FullName = ss[0];
        }
        catch (Exception) { }
        return re;
    }
    public ItemData CreatNew(string s)
    {
        return Creat(s);
    }
}