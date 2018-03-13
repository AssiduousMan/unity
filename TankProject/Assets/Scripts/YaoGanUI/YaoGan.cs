using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  这个是控制摇杆范围的类。

public class YaoGan : MonoBehaviour {

    public RectTransform fanWei;  //  这个是控制要看范围的那个矩形。
    public RectTransform joy;  //  这个是摇杆的recttransform组件。
    public float radius;  //  摇杆控制的范围的半径。

	// Use this for initialization
   	void Start () {

        this.radius = this.fanWei.sizeDelta.x / 2;  //  矩形来个对角的顶点相见，.x就是那个线的水平的长度，/2就行该矩形的边长的一半。
       // Debug.Log(this.radius);

	}
	
	// Update is called once per frame
	public void Controll_YaoGan () {

        //  如果这个摇杆的拉伸长度大于了规定的半径，那么就应该限制他。
        if (this.joy.anchoredPosition.magnitude > this.radius) {

            this.joy.anchoredPosition= this.joy.anchoredPosition.normalized * this.radius;

        }

	}
}
