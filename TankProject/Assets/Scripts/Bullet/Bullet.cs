using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//  这个是子弹的类，来控制子弹的爆炸效果和销毁,伤害。

public class Bullet : NetworkBehaviour {

    public GameObject bulletExplosionPre;  //  子弹爆炸的预设体。
    public float damage = 10;  //  坦克的伤害，默认是10。
    public AudioClip bulletExplosionAudio;  //  子弹爆炸的音效。
    public int number;  //  子弹的编号，如果发出子弹的坦克编号是2，那么子弹的编号就是2.
	public bool isBoom = false;  //  判断该子弹是否能打坏墙壁。

    public void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Goods") return;  //  如果子弹打中的物品，那么返回。

        if (other.tag == "Shield") {  //  如果打中的是护盾，那么判断这个护盾是不是自己的，如果是，就返回。

            //Debug.Log(other.transform.parent.gameObject.GetComponent<TankInfomation>().tank_Number);
           // Debug.Log(this.number);
            if (other.transform.parent.gameObject.GetComponent<TankInfomation>().tank_Number == this.number) {

                return;

            }

        }

		if (other.tag == "Wall") {  //  如果大中的是墙体。
		
			if (this.isBoom) {
                //  说明可以打坏墙壁。
                //NetworkServer.Destroy(other.gameObject);
				RpcWallBoom(other.name);
				Destroy (other.gameObject);
			
			}

		}

		if (other.tag == "EnemyTank") {
		
			other.gameObject.GetComponent<EnemyTankHealth> ().TakeDamage (this.damage);
		
		}

        AudioSource.PlayClipAtPoint(this.bulletExplosionAudio, this.transform.position);
        Instantiate(this.bulletExplosionPre, this.transform.position, this.transform.rotation);
        RpcBoom();

        if (other.tag == "Tank") {

			if (other.gameObject.GetComponent<TankInfomation> ().isWuDi == true) {
				//  如果坦克处于无敌状态。
				Destroy (this.gameObject);  //  然后销毁子弹。
				return;
			}
            //Debug.Log("dazhongtanke");
            if (other.gameObject.GetComponent<TankInfomation>().tank_Number == this.number) {
                //  如果是该坦克是自己，那么将不造成伤害。
                return;

            }
            //other.SendMessage("TakeDamage", this.damage);
            other.gameObject.GetComponent<Tank_Health>().TakeDamage(this.damage, this.number);
            
        }

        Destroy(this.gameObject);  //  然后销毁子弹。

    }

    [ClientRpc]
    public void RpcBoom() {  //  子弹爆炸的特效，传送到各个客户端。
       // Debug.Log("BulletBoom");
        if (this.isServer == true) return;
        AudioSource.PlayClipAtPoint(this.bulletExplosionAudio, this.transform.position);
        Instantiate(this.bulletExplosionPre, this.transform.position, this.transform.rotation);

    }

	[ClientRpc]
	public void RpcWallBoom(string name) {  //  墙体被破坏。

       // Debug.Log("打到的Cube名字为：" + name);
        Transform trans;
        switch (name[0]) {

            case 'C':
                //  先根据获得的Cube的名字来判断是哪一部分的Cube
                trans = GameObject.FindGameObjectWithTag("CenterCube").transform;
                Destroy(trans.Find(name).gameObject);
                break;
            case 'B':
                trans = GameObject.FindGameObjectWithTag("BottomCube").transform;
                Destroy(trans.Find(name).gameObject);
                break;
            case 'T':
                trans = GameObject.FindGameObjectWithTag("TopCube").transform;
                Destroy(trans.Find(name).gameObject);
                break;
            case 'L':
                trans = GameObject.FindGameObjectWithTag("LeftCube").transform;
                Destroy(trans.Find(name).gameObject);
                break;
            case 'R':
                trans = GameObject.FindGameObjectWithTag("RightCube").transform;
                Destroy(trans.Find(name).gameObject);
                break;
            case 'O':
                trans = GameObject.FindGameObjectWithTag("OtherCube").transform;
                Destroy(trans.Find(name).gameObject);
                break;
           
        }

        //Destroy(GameObject.FindGameObjectWithTag("WallBox").transform.Find(name).gameObject);
               
        //Debug.Log ("meiyou");
        
	}

}
