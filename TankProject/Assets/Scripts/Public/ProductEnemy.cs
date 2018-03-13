using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//  这个是生产系统坦克的类。

public class ProductEnemy : NetworkBehaviour {

    public GameObject enemyPre;  //  地方坦克的预设体。

    public override void OnStartServer()
    {

        Product_Enemy();

    }

    public void Product_Enemy()
    {

        GameObject obj = Instantiate(this.enemyPre, this.transform.position, this.transform.rotation);
        NetworkServer.Spawn(obj);
        //  把身上的各脚本激活。
        obj.GetComponent<EnemyTankHealth>().enabled = true;
        obj.GetComponent<EnemyTankFiringBullet>().enabled = true;
        obj.GetComponent<EnemyTankTransforms>().enabled = true;
        obj.transform.Find("TankRenderers").GetComponent<EnemyBoxcollider>().enabled = true;
        obj.transform.Find("TankRenderers").GetComponent<BoxCollider>().enabled = true;


    }


}
