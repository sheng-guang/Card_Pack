using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using System;
using TMPro;






public partial class CardMesh : IFormGetter//form getter
{
    public bool Useful
    {
        get
        {
            if (skill == null) Debug.Log("CardMesh: "+unit + "skill null");
           return skill != null ? skill.HighLightTest() : false; 
        }
    }

    public InputForm GetCopyForm()
    {
        if (skill == null) return null;
        return skill.GetEmptyForm();
    }
}




    
public partial class CardMesh:IPointerEnterHandler,IPointerExitHandler//highlight
{
    public void OnHighLightChange()
    {
        InputHighLight.SetActive(unit.IsHighLightInput.Value);
    }
    public GameObject InputHighLight;

    public GameObject OnPointHighLight;
    private void OnEnable()
    {
        OnPointHighLight.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        OnMousePoint.SetTarget(unit);
        OnPointHighLight.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        OnPointHighLight.SetActive(false);
    }
}

public partial class CardMesh//awake
{   

    public override void Awake()
    {
        base.Awake();
        c.isTrigger = true;
    }
    public Collider c;


    public MeshRenderer ImgMesh;
    public float ImageW_H()
    {
        var scale = ImgMesh.transform.lossyScale;
        return scale.x / scale.y;
    }    
    InputSkill skill => unit.GetInputSkill(0);

    public override void Awake_OnSetMaster()
    {
        base.Awake_OnSetMaster();
        ImgMesh.LoadTexture("_MainTex", ImageW_H(), unit.FullName(), 0);


        unit.Event.StateCall.Listen(OnStateChange);
        unit.Event.SpaceCall_InHandCall.Listen(InHandChange);
        unit.Event.HandIndexCall.Listen(OnHandIndexChange);
        unit.Event.InputHighLigh.Listen(OnHighLightChange);

    }

}


public partial class CardMesh : LayerCompResMesh//call change
{
    public void OnStateChange()
    {
        gameObject.SetActive(UnitState.Card == unit.State.Value);
    }
    public void InHandChange()
    {
        bool istrigger = false;
        if (unit.SpaceIsInHand) { istrigger = true; }
        else { unit.transform.SetParent(null); }
        c.isTrigger = istrigger;
    }
    public float UnitWide = 0.07f;
    public void OnHandIndexChange()
    {

        if (unit.HandIndex.Value == -1) return;
        else {
            unit.transform.localPosition = Vector3.zero+new Vector3(UnitWide, 0,0)*unit.HandIndex.Value;
        }
    }

}
public partial class CardMesh:IPointerClickHandler//click
{
    public void OnPointerClick(PointerEventData eventData)
    {
        FormWriter.TrySetFormGetter(this);
        //FormWriter.SetForm(this);
    }
}
