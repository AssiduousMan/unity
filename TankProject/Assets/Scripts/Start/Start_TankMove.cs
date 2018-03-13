using System.Collections;
using System.Collections.Generic;
using UnityEngine;

  //  这个类是用来在开始界面的时候，让坦克移动的。
public class Start_TankMove : MonoBehaviour {

    public Vector3 target;
    public bool isArrive = false;  //  判断是否到达目的地的标志。
    public int i = -1; //  这个是用来激活账号登录界面的标志，一个坦克就够了，当坦克到达指定位置的时候就可以激活登录界面。1表示该坦克有该权限。

    private void Update()
    {
        if(this.isArrive == false)
            Tank_Move();

    }

    private void Tank_Move() {

        float distance = Vector3.Distance(this.transform.position, this.target);
        //if(this.i == 1)
        //Debug.Log(distance);
        if (distance <= 2.1f)
        {

            this.isArrive = true;
            this.transform.position = this.target;
            if (this.i == 1) {

                //  把登录界面激活。
                GameObject.FindGameObjectWithTag("Canvas").transform.Find("Image/Login").gameObject.SetActive(true);

            }

        }
        else {
            //Debug.Log(Time.deltaTime * 20);
            //this.transform.position += this.transform.forward * Time.deltaTime * 20;
            this.transform.position = Vector3.Lerp(this.transform.position, this.target, 0.1f);

        }
    }

}
