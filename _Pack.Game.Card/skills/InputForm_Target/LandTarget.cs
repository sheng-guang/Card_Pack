using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

    public class LandTarget : MonoBehaviour,  IPointerEnterHandler,ITarget
    {
        static InputDataPoint data = new InputDataPoint();
        public IInputData GetData()
        {
            data.Point = OnMousePoint.Mouse3DPosition;
            return data;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnMousePoint.SetTarget (this);
        }
    }

