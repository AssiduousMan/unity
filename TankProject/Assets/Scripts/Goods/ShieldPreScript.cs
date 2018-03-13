using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPreScript : MonoBehaviour {

    public Camera cam;

    public void Start()
    {

        this.cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

    }

    //// Update is called once per frame
    //void Update()
    //{

    //    Vector3 v = this.cam.transform.position - this.transform.position;
    //    this.transform.up = v;

    //}

    public void OnTriggerEnter(Collider other)
    {

        if (other.tag == "EnemyTank") return;
        if (other.tag == "Bullet") return;  //  如果碰到了发射的子弹，返回。

        if (other.tag == "Tank")
        {
            //Debug.Log("shield");
            other.gameObject.GetComponent<TankInfomation>().AddShield();

        }
        Destroy(this.gameObject);
        GameObject.FindGameObjectWithTag("Product_Goods").GetComponent<Product_Goods>().Product_SHIELDPre();


    }

}
