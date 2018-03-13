using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomPre : MonoBehaviour {

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

			other.gameObject.GetComponent<TankInfomation>().AddBoom();
				
		}
		Destroy(this.gameObject);
		//if(this.number == 1)
			GameObject.FindGameObjectWithTag("Product_Goods").GetComponent<Product_Goods>().Product_BOOMPre();
		//if(this.number == 2)
			//GameObject.FindGameObjectWithTag("Product_Goods").GetComponent<Product_Goods>().Product_BLOODBIGPre();

	}
}
