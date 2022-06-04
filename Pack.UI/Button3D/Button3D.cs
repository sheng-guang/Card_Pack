using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Button3D : MonoBehaviour,IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler,IPointerExitHandler
{
    public Color NormalColor=Color.white;
    public Color HighlightedColor=new Color(0.96f, 0.96f, 0.96f);
    public Color DownColor = new Color(0.78f, 0.78f, 0.78f);

    [Header("target mesh")]
    public MeshRenderer r;
    public string ColorName = "_Color";
    private void OnEnable()
    {
        OnPoint = false;
        Down = false;
        Fresh();
    }

    private void Awake()
    {
        //只能在unity事件中创建否则会报错
        materialPropertyBlock = new MaterialPropertyBlock();
    }
    public MaterialPropertyBlock materialPropertyBlock;
    public void Fresh()
    {
        Color targetColor = NormalColor;
        if (OnPoint )
        {
            if (Down)
            {
                targetColor = DownColor;
            }
            else 
            {
                targetColor = HighlightedColor;
            }
        }
        r.GetPropertyBlock(materialPropertyBlock);
        materialPropertyBlock.SetColor(ColorName, targetColor);
        r.SetPropertyBlock(materialPropertyBlock);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) { gameObject.SetActive(false); }   
    }
    [Header("state")]
    public bool Down = false;
    public bool OnPoint = false;
    public void OnPointerEnter(PointerEventData eventData)
    {
        OnPoint = true;
        //print("enter");
        Fresh();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        OnPoint = false;
        //print("exist");
        Fresh();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        //print("down");
        Down = true;
        Fresh();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Down = false;
        //print("up");
        Fresh();
    }


}
