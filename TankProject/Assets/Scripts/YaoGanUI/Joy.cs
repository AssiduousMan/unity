using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


//  这个是摇杆中间的摇杆的类，为了当拖拽结束时发射子弹。

public class Joy : MonoBehaviour, /*IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler, IPointerDownHandler,IPointerUpHandler,*/IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public bool isDrag = false;  //  判断是否进行拖拽。
    public bool isEndDrag = true;  //  判断是否结束拖拽。
    public FiringBullets firingBullet;
    public ScrollRect scrollRect;  //  他的父亲，拖动组件。
    public int i;  //  i = 1为移动的摇杆，2为射击的摇杆
    //public Canvas_Monitor can;

    public void Start()
    {
        this.scrollRect = this.transform.parent.GetComponent<ScrollRect>();
        
    }

    //  调用父类的方法，然后传参，因为重写了父类的方法。
    public void OnBeginDrag(PointerEventData eventData)
    {
        this.scrollRect.OnBeginDrag(eventData);
      //  if (i == 1) {
//
         //   Debug.Log("1");
            //this.can.isUpdate_Move = false;

       // }
       // if (i == 2)
        //{

            //Debug.Log("1");
            //this.can.isUpdate_Fire = false;

       // }
        // Debug.Log("kaishituozhuai");

    }

    public void OnDrag(PointerEventData eventData)
    {
        this.scrollRect.OnDrag(eventData);
       // Debug.Log("zhengzaituozhuai");
        
    }


    public void OnEndDrag(PointerEventData eventData)
    {

        //can.i = 0;
        //if (i == 1) this.can.isUpdate_Move = true;
        if (i == 2) { 
        this.scrollRect.OnEndDrag(eventData);
        //if (this.isLocalPlayer == false) return;
        this.firingBullet.Fire();
            //this.can.isUpdate_Fire = true;
            //Debug.Log("jieshutuozhuai");
        }

        this.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        this.scrollRect.GetComponent<RectTransform>().anchoredPosition = new Vector2(-50f, -50f);

    }



    //public void OnDrag(PointerEventData eventData)
    //{

    //}

    //public void OnEndDrag(PointerEventData eventData)
    //{
    //    Debug.Log("111");
    //    this.firingBullet.Fire();

    //}

    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    Debug.Log("dianleyixia");
    //}

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    Debug.Log("dianji");
    //}

    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    Debug.Log("jinru");
    //}

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    Debug.Log("likai");
    //}

    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    Debug.Log("taiqi");
    //}

    //public void DianJi() {

    //    Debug.Log("www");

    //}

}
