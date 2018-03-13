using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//  这个类是用来管理飞机上掉下来的物品的，作用是当飞机上的物体掉落时判断是否接触到了地面，从而生成新的物体。

public class PlaneGoods : MonoBehaviour {

    public GameObject goodPre;  //  物品的预设体。

    public void Start()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Plane") {

            Transform trans = this.transform;
            bool flag = GameObject.FindGameObjectWithTag("AirPlane").GetComponent<NetworkIdentity>().isServer;
            //bool flag = true;

            Destroy(this.gameObject);


            

            if (flag == true) {
                GameObject obj = Instantiate(this.goodPre, new Vector3(trans.position.x, 0.3f, trans.position.z), trans.rotation);
                NetworkServer.Spawn(obj);
                obj.GetComponent<BoxCollider>().enabled = true;
                obj.GetComponent<Good>().enabled = true;


            }
           

        }

    }


}
