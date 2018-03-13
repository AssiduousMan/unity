using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Grade_Trigger : NetworkBehaviour {

	public int number = -1;  //  这个是用来判断是大的经验还是小的经验。1表示小的经验，2表示大的经验。
	public bool isDropGood = false;  //  判断是否是掉落的经验。

    //public void Update()
    //{
    //    Debug.Log("hahahahaha");
    //}

    public void OnTriggerEnter(Collider other)
    {

		if (other.name == "TankRenderers")
			return;  //  如果碰到了敌方坦克的攻击范围的碰撞体，那么返回。
        if (other.tag == "EnemyTank") return;
        if (other.tag == "Bullet") return;  //  如果碰到了发射的子弹，返回。

        if (other.tag == "Shield")
        {  //  如果碰到是的护盾，那么返回。

            return;

        }

        //Debug.Log("pengzhuang");

        if (other.tag == "Tank") {

			if (this.number == 1) {  //  如果是小经验。
			
				//Debug.Log ("chijingyan");
				other.gameObject.GetComponent<TankInfomation> ().grade += 10;
				other.gameObject.GetComponent<TankInfomation> ().GetExperiencee (20);
				//if(this.isServer == false)
				other.gameObject.GetComponent<TankInfomation> ().RpcGetExperiencee (20);


			} else {  //  如果是大经验。
			
				other.gameObject.GetComponent<TankInfomation> ().grade += 30;
				other.gameObject.GetComponent<TankInfomation> ().GetExperiencee (60);
				//if(this.isServer == false)
				other.gameObject.GetComponent<TankInfomation> ().RpcGetExperiencee (60);
			
			}
            
            //other.gameObject.GetComponent<TankInfomation>().tankName = "asd";


        }

        //  只要碰上物体就销毁，然后重新生成一个。
        Destroy(this.gameObject);
		if(this.number == 1)  //  如果是小经验。
			GameObject.FindGameObjectWithTag("Product_Goods").GetComponent<Product_Goods>().Product_ExpPre();
		else  //  如果是大经验。
		{

			if (this.isDropGood) {
				//  如果是掉落的物品，那么就不用生成了。
				return;
			
			}
			GameObject.FindGameObjectWithTag("Product_Goods").GetComponent<Product_Goods>().Product_ExpBigPre();

		}
			
        //Cmdc();

    }

    //[Command]
    //public void Cmdc()
    //{



    //}


}
