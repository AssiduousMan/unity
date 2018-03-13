using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  这个是飞机掉落物品的类，不过这个是地面上的物品，说明已经掉落到地面上了。

public class Good : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Tank") {

            //Debug.Log("tankechidiao");
            Destroy(this.gameObject);

            //  经验， 攻击， 防御， 射程， 速度， 无敌
            int random = Random.Range(0, 6);  //  生成0 - 5
            //int random = 0;
            TankInfomation tankInformation = other.GetComponent<TankInfomation>();

            switch (random) {

                case 0:
                    tankInformation.GetExperiencee(200f);
                    tankInformation.RpcGetExperiencee(200f);
                    tankInformation.grade += 100;
                    break;
                case 1:
                    tankInformation.AddAtk(10f);
                    break;
                case 2:
                    tankInformation.AddDef(8f);
                    break;
                case 3:
                    tankInformation.AddRange(10f);
                    break;
                case 4:
                    tankInformation.AddSpeed(0.4f);
                    break;
                case 5:
                    tankInformation.AddShield();
                    break;

            }

            GameObject.Find("ControlPlane").GetComponent<ControlPlane>().isHavaGood = false;  //  将物品吃完之后在将其重新赋值。


        }

    }

}
