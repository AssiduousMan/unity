using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  canvas监听，为了随时监听鼠标的位置。

public class Canvas_Monitor : MonoBehaviour {

    public RectTransform scrollRectTransform_Move;  //  移动的虚拟摇杆的位置。
    public RectTransform scrollRectTransform_Fire;  //  射击的虚拟摇杆的位置。
    public int i = 0;  //  i = 1来控制移动的虚拟摇杆位置，2为控制射击的虚拟摇杆的位置。
    public bool isUpdate_Move = true;
    public bool isUpdate_Fire = true;
    float x;

    public void Start()
    {
        x = this.gameObject.GetComponent<RectTransform>().sizeDelta.x;
    }

    // Update is called once per frame
    void Update () {
        
        if (this.isUpdate_Move && Input.mousePosition.x < x / 2)
        {

            Vector2 v = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            this.scrollRectTransform_Move.anchoredPosition = v;

        }
        else if (this.isUpdate_Fire && Input.mousePosition.x > x / 2) {

            Vector2 v = new Vector2(-(this.x - Input.mousePosition.x), Input.mousePosition.y);
            this.scrollRectTransform_Fire.anchoredPosition = v;

        }


        //if (i == 1 && this.isUpdate_Move)
        //{
        //    //Debug.Log();
        //    //Debug.Log(Input.mousePosition);


        //}
        //else if (i == 2 && this.isUpdate_Fire) {

        //    Vector2 v = new Vector2(this.x - Input.mousePosition.x, Input.mousePosition.y);
        //    this.scrollRectTransform_Fire.anchoredPosition = v;

        //}

    }
}
