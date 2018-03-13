using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//  这个是用来控制直升机的，主要是为了倒计时，是直升机每隔一段时间出现一次。

public class ControlPlane : NetworkBehaviour {

    private float time = 30f;
    public float countDown;  //  倒计时的秒数。
    public GameObject planePre;  //  飞机的预设体。
    public Vector3 posi1 = Vector3.zero;  //  物品下落的位置。
    public Vector3 posi2 = Vector3.zero;

    public bool isHavaGood = false;  //  判断当前场景中是不是有货物。  



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        time += Time.deltaTime;
        if (time >= this.countDown) {

            //  到倒计时到了之后，开始遍历所有游戏物体，看看有没有坦克，如果没有，说明
            //  当前没有玩家在线，所以就不用生成直升机。
            GameObject[] a = GameObject.FindGameObjectsWithTag("Tank");
            if (a.Length == 0)
            {

             //   Debug.Log("meiyou");
                time = 0f;
                return ;

            }
          //  Debug.Log("you tank");
            time = 0f;

            float f = Random.Range(0f, 2f);  //  生成两个随机数，判断大小从而生成对应的位置。
            if (f > 1f && this.isHavaGood == false)
            {

                GameObject obj = Instantiate(this.planePre, this.posi1, this.transform.rotation);
                //obj.GetComponent<Plane_Move>().distance = Random.Range(30f, 70f);
                //RpcInstantiatePlane(obj, obj.GetComponent<Plane_Move>().distance);
                //RpcInstantiatePlane();
                NetworkServer.Spawn(obj);
                this.isHavaGood = true;

            }
            else if(this.isHavaGood == false) {

                GameObject obj = Instantiate(this.planePre, this.posi2, this.transform.rotation);
                //obj.GetComponent<Plane_Move>().distance = Random.Range(30f, 70f);
                //RpcInstantiatePlane(obj, obj.GetComponent<Plane_Move>().distance);
                //RpcInstantiatePlane();
                NetworkServer.Spawn(obj);
                this.isHavaGood = true;
            }
                
            //RpcInstantiatePlane();

        }

	}

    //[ClientRpc]
    //public void RpcInstantiatePlane()
    //{
    //    if (this.isLocalPlayer == false) return;
    //    GameObject.FindGameObjectWithTag("AriPlane").GetComponent<Plane_Move>().distance = 0;

    //}
}
