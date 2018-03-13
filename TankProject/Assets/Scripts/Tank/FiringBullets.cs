using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//  这个是发射子弹的类。

public class FiringBullets : NetworkBehaviour {

    public Transform firePosition;
    public GameObject bulletPre;  //  子弹的预设体。
    public float firePower = 5;  //  发射子弹的力。
    public AudioClip bulletFiringAudio;  //  子弹发射的声音。
    public Joy joy;  //  摇杆的脚本。
    public TankInfomation tankInformation;
    [SyncVar (hook = "OnChangeBullerScale")]
    public float buller_Scale;  //  发射的子弹的大小。
    //[SyncVar (hook = "OnChangeGameObject")]
    public GameObject obj;
    [SyncVar (hook = "OnChangeTransform")]
    public Transform bulletTransform;

    public void Start()
    {

        this.firePosition = this.transform.Find("TankRenderers/TankTurret/firePosition").GetComponent<Transform>();
        this.tankInformation = this.GetComponent<TankInfomation>();
        this.buller_Scale = 1f;
        if (this.isLocalPlayer == true) { 
        //  这个主要是要控制坦克joy来动态的给属性firingBullet赋值，因为会同时存在多个坦克，使得joy不知道该是哪个坦克身上的脚本。
        this.joy = GameObject.Find("Canvas").transform.Find("YaoGan_ControllFaShe/Joy").GetComponent<Joy>() ;
        this.joy.GetComponent<Joy>().firingBullet = this;
            
        }
        
        Network.sendRate = 60;
    }
    
    [Command]
    public void CmdFire() {  //  在服务器上运行这个方法。

        //this.firePosition = this.transform.Find("TankRenderers/TankTurret/firePosition").GetComponent<Transform>();

        //Debug.Log(this.firePosition.position);
        //AudioSource.PlayClipAtPoint(this.bulletFiringAudio, this.transform.position);
        RpcBoom();
        //GameObject obj = Instantiate(this.bulletPre, this.firePosition.position, this.firePosition.localRotation);
        obj = Instantiate(this.bulletPre, this.firePosition.position, this.firePosition.rotation);
        this.bulletTransform = obj.transform;
        this.bulletTransform.localScale = new Vector3(this.buller_Scale, this.buller_Scale, this.buller_Scale);
        this.bulletTransform.GetComponent<Bullet>().number = this.tankInformation.tank_Number;
        obj.GetComponent<Bullet>().damage = this.tankInformation.attack;
        obj.GetComponent<Rigidbody>().velocity = obj.transform.forward * firePower;
		obj.GetComponent<Bullet> ().isBoom = this.tankInformation.isBoom;  //  给子弹添加是否破坏墙壁的效果。
        
        NetworkServer.Spawn(obj);  //  然后将子弹发送至各个客户端。
        obj.GetComponent<CapsuleCollider>().enabled = true;  //  将collider激活
       // RpcBullerScale();  //  向各个客户端调用，用来设置子弹的scale

    }

    [ClientRpc]
    public void RpcBoom()
    {  //  子弹爆炸的特效，传送到各个客户端。
       // Debug.Log("BulletBoom");
        if (this.isServer == true) return;
        AudioSource.PlayClipAtPoint(this.bulletFiringAudio, this.transform.position);
        //Instantiate(this.bulletExplosionPre, this.transform.position, this.transform.rotation);

    }

    public void OnChangeTransform(Transform tran)
    {  //  当transform改变时要让各个客户端都跟着改变。

        if (tran == null) Debug.Log("text1");
        //Debug.Log("iii");
        this.bulletTransform = tran;
        if (this.bulletTransform == null) Debug.Log("text2");
        if (this.isServer) return;   //  如果这个客户端又是服务器端，那么就返回。
        this.bulletTransform.localScale = new Vector3(this.buller_Scale, this.buller_Scale, this.buller_Scale);
        //this.bulletTransform.GetComponent<Bullet>().number = this.tankInformation.tank_Number;

    }

    public void OnChangeBullerScale(float f) {

       // Debug.Log("scale");
        this.buller_Scale = f;

    }

    //[ClientRpc]
    //public void RpcBullerScale() {
    //    //Debug.Log("111");
    //    if (obj == null) Debug.Log("222");
    //    this.bulletTransform.localScale = new Vector3(this.buller_Scale, this.buller_Scale, this.buller_Scale);
    //    //obj.transform.localScale = new Vector3(1f, 1f, 1f);


    //}

    public void Fire() {
        //Debug.Log(this.isLocalPlayer);
        if (this.isLocalPlayer == false) return;
        CmdFire();

    }

}
