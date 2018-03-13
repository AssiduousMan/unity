using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//  这个是系统坦克用来发射子弹的类。

public class EnemyTankFiringBullet : NetworkBehaviour {

    public Transform firePosition;
    public GameObject bulletPre;  //  子弹的预设体。
    public float firePower = 5;  //  发射子弹的力。
    public AudioClip bulletFiringAudio;  //  子弹发射的声音。
    [SyncVar(hook = "OnChangeBullerScale")]
    public float buller_Scale;  //  发射的子弹的大小。
    //[SyncVar (hook = "OnChangeGameObject")]
    public GameObject obj;
    [SyncVar(hook = "OnChangeTransform")]
    public Transform bulletTransform;
    public float attack = 30f;

    public void Start()
    {

        this.firePosition = this.transform.Find("TankRenderers/TankTurret/firePosition").GetComponent<Transform>();
        this.buller_Scale = 1f;
        

        Network.sendRate = 60;
    }

    //[Command]
    public void CFire()
    {  //  在服务器上运行这个方法。

        //this.firePosition = this.transform.Find("TankRenderers/TankTurret/firePosition").GetComponent<Transform>();

        //Debug.Log(this.firePosition.position);
        AudioSource.PlayClipAtPoint(this.bulletFiringAudio, this.transform.position);
        //GameObject obj = Instantiate(this.bulletPre, this.firePosition.position, this.firePosition.localRotation);
        obj = Instantiate(this.bulletPre, this.firePosition.position, this.firePosition.rotation);
        this.bulletTransform = obj.transform;
        this.bulletTransform.localScale = new Vector3(this.buller_Scale, this.buller_Scale, this.buller_Scale);
        this.bulletTransform.GetComponent<Bullet>().number = 100;
        obj.GetComponent<Bullet>().damage = this.attack;
        obj.GetComponent<Rigidbody>().velocity = obj.transform.forward * firePower;


        NetworkServer.Spawn(obj);  //  然后将子弹发送至各个客户端。
        obj.GetComponent<CapsuleCollider>().enabled = true;  //  将collider激活
                                                             // RpcBullerScale();  //  向各个客户端调用，用来设置子弹的scale

    }

    public void OnChangeTransform(Transform tran)
    {  //  当transform改变时要让各个客户端都跟着改变。

        if (tran == null) Debug.Log("text1");
      //  Debug.Log("iii");
        this.bulletTransform = tran;
        if (this.bulletTransform == null) Debug.Log("text2");
        if (this.isServer) return;   //  如果这个客户端又是服务器端，那么就返回。
        this.bulletTransform.localScale = new Vector3(this.buller_Scale, this.buller_Scale, this.buller_Scale);
        //this.bulletTransform.GetComponent<Bullet>().number = this.tankInformation.tank_Number;

    }

    public void OnChangeBullerScale(float f)
    {

       // Debug.Log("scale");
        this.buller_Scale = f;

    }



    public void Fire()
    {
        //Debug.Log(this.isLocalPlayer);
        if (this.isServer == false) return;
        CFire();

    }
}
