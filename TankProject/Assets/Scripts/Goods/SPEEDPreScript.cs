using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPEEDPreScript : MonoBehaviour {

    public Camera cam;

    public void Start()
    {

        this.cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

    }

    // Update is called once per frame
    //void Update()
    //{

    //    Vector3 v = this.transform.position - this.cam.transform.position;
    //    this.transform.forward = v;

    //}

    public void OnTriggerEnter(Collider other)
    {

        if (other.tag == "EnemyTank") return;
        if (other.tag == "Bullet") return;  //  如果碰到了发射的子弹，返回。

        if (other.tag == "Shield")
        {  //  如果碰到是的护盾，那么返回。

            return;

        }

        if (other.tag == "Tank")
        {
           // Debug.Log("speed");
            other.gameObject.GetComponent<TankInfomation>().AddSpeed(0.2f);

        }
        Destroy(this.gameObject);
        GameObject.FindGameObjectWithTag("Product_Goods").GetComponent<Product_Goods>().Product_SPEEDPre();


    }
}
