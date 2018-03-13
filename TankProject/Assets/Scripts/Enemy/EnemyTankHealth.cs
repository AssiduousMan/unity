using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

//  这个是控制坦克血量的类，来控制坦克的各项属性。

public class EnemyTankHealth : NetworkBehaviour
{

    public float healthTotal = 100;
    //[SyncVar (hook = "OnChangeHealth")]
    public float currentHealth;  //  坦克的血量。
    public GameObject tankExplosionPre;//  坦克爆炸的预设体。
    public AudioClip tankExplosionAudio;  //  坦克爆炸的音效。
    public Slider slider;  //  血条。
    public float defense = 5f;
	public GameObject dropGood;  //  这个是表示坦克死亡之后掉落的物品。
   // public TankInfomation tankInformation;  //  坦克信息的类。

    private void Start()
    {
        //this.tankInformation = this.GetComponent<TankInfomation>();
        //this.healthTotal = this.tankInformation.total_Health;
        this.currentHealth = this.healthTotal;
        this.slider.enabled = false;

    }

    public void TakeDamage(float dama)
    {
        //Debug.Log("123");
        dama = dama - this. defense;   //  最后的伤害等于传过来的伤害减去当前坦克的防御。
        if (dama < 0) dama = 0;
        if (this.currentHealth <= 0) return;
        this.currentHealth -= dama;
        
        if (this.currentHealth <= 0)
        {
            //  说明已经死亡。
            this.currentHealth = 0;
         
            //  实例化爆照效果。
            GameObject.Instantiate(this.tankExplosionPre, this.transform.position, this.transform.rotation);
            //  播放坦克爆炸音效。
            AudioSource.PlayClipAtPoint(this.tankExplosionAudio, this.transform.position);
            GameObject.Destroy(this.gameObject);
            RpcTankBoom();  //  发送给各个客户端坦克爆炸的效果。
			DropGood();  //  当系统坦克死亡时，调用该方法，为了掉落物品。
            //  然后在规定的秒数之后在生成一个。
            GameObject.FindGameObjectWithTag("Product_Goods").GetComponent<Product_Goods>().Product_ENEMYTankePre();

        }
        this.slider.value = this.currentHealth / this.healthTotal;

        //if(this.isLocalPlayer == true)
        
        RpcOnChangeHealth(this.slider.value);

    }

	public void DropGood(){
	
		for (int i = 0; i < 15; i++) {
			
			GameObject o = Instantiate (this.dropGood, this.transform.position + new Vector3 (Random.Range (-5f, 5f), 0.5f, Random.Range (-5f, 5f)), this.transform.rotation);
			NetworkServer.Spawn (o);
			o.GetComponent<Grade_Trigger> ().isDropGood = true;  //  将此经验定义为掉落的经验，吃完之后不会在自动生成
			//o.AddComponent<Grade_Trigger>();
			o.GetComponent<Grade_Trigger> ().enabled = true;
			o.GetComponent<BoxCollider> ().enabled = true;

		}
	}

    [ClientRpc]
    public void RpcOnChangeHealth(float value)
    {  //  当health的值改变的时候会发送大各个客户端来调用这个方法。
        //Debug.Log(value);
        //if (this.tankInformation == null) return;
        //Debug.Log("asddd" + value);
        this.slider.value = value;
        

    }

    [ClientRpc]
    public void RpcTankBoom()
    {  //  坦克爆炸的特效。

        if (this.isServer == true) return;

        //  实例化爆照效果。
        GameObject.Instantiate(this.tankExplosionPre, this.transform.position, this.transform.rotation);
        //  播放坦克爆炸音效。
        AudioSource.PlayClipAtPoint(this.tankExplosionAudio, this.transform.position);
        GameObject.Destroy(this.gameObject);

    }

}
