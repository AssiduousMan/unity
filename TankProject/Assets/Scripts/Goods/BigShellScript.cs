using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigShellScript : MonoBehaviour {

    public AnimationCurve curve;
    public Product_Goods product;

	
	// Update is called once per frame
	void Update () {

        //Debug.Log(this.curve.Evaluate(Time.time));
        this.transform.Rotate(Vector3.up * 45 * Time.deltaTime);
        this.transform.localScale = new Vector3((1 + curve.Evaluate(Time.time)) * 1f, (1 + curve.Evaluate(Time.time)) * 1f, (1 + curve.Evaluate(Time.time)) * 1f);

	}

    public void OnTriggerEnter(Collider other)
    {

        if (other.tag == "EnemyTank") return;
        if (other.tag == "Bullet") return;  //  如果碰到了发射的子弹，返回。

        if (other.tag == "Shield")
        {  //  如果碰到是的护盾，那么返回。

            return;

        }

        if (other.tag == "Tank")
        {

            other.gameObject.GetComponent<FiringBullets>().buller_Scale += 0.2f;
            //if (other.gameobject.getcomponent<tankinfomation>().islocalplayer == true) {  //  如果是本地的玩家，那么攻击++，ui更新，别的客户端就不用，应为不用同步别的客户端。
            //    other.gameobject.getcomponent<tankinfomation>().attack *= 1.2f;
            //    other.gameobject.getcomponent<tankinfomation>().update_ui();
            ////other.gameobject.getcomponent<tankinfomation>().tankname = "asd";
            //}
            


        }
        Destroy(this.gameObject);
        GameObject.FindGameObjectWithTag("Product_Goods").GetComponent<Product_Goods>().Product_BigBulletPre();


    }

}
