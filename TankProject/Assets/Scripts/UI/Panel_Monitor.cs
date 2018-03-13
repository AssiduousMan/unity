using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//  面板监听，为了让虚拟摇杆根据点击位置来进行初始化。

public class Panel_Monitor : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler{

    public int i;  //  i = 1 为左面板， i = 2 为右面板
    //public Canvas_Monitor can;
    public Joy joy_Move;
    public Joy joy_Fire;
    float width;
    public RectTransform canvas;

    public void Start()
    {

        this.width = this.canvas.sizeDelta.x;

    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        //Debug.Log(eventData.position);

        if (this.i == 1)
        {

            this.joy_Move.transform.parent.GetComponent<RectTransform>().anchoredPosition = eventData.position;
            this.joy_Move.OnBeginDrag(eventData);

        }
        else if (this.i == 2) {

            this.joy_Fire.transform.parent.GetComponent<RectTransform>().anchoredPosition = new Vector2(-(this.width - eventData.position.x), eventData.position.y);
            this.joy_Fire.OnBeginDrag(eventData);

        }

    }

    public void OnDrag(PointerEventData eventData)
    {

        if (this.i == 1) {

            this.joy_Move.OnDrag(eventData);

        }
        else if (this.i == 2)
        {

            this.joy_Fire.OnDrag(eventData);

        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {

        if (this.i == 1) {

            this.joy_Move.OnEndDrag(eventData);

        }
        else if (this.i == 2)
        {

            this.joy_Fire.OnEndDrag(eventData);

        }

    }
}
