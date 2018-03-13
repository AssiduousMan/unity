using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodPre : MonoBehaviour {

	public int number = 1;  //  预设体的编号，1代表小的，2代表大的血量。 

	// Use this for initialization
	void Start () {
		
	}
	
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

		if (other.tag == "Tank")
		{
			//Debug.Log("def");
			if(this.number == 1)  //  如果是小的加血。
	 			other.gameObject.GetComponent<TankInfomation>().AddBlood(20f);
			if(this.number == 2)  //  如果是大的加血。
				other.gameObject.GetComponent<TankInfomation>().AddBlood(1000f);  //  这样的话可以直接满血。

		}
		Destroy(this.gameObject);
		if(this.number == 1)
			GameObject.FindGameObjectWithTag("Product_Goods").GetComponent<Product_Goods>().Product_ATKPre();
		if(this.number == 2)
			GameObject.FindGameObjectWithTag("Product_Goods").GetComponent<Product_Goods>().Product_BLOODBIGPre();

	}
}
