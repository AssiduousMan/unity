using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Product_Goods : NetworkBehaviour {

    public GameObject expPre;
	public GameObject expBigPre;
    public GameObject bigBullerPre;
    public GameObject atkPre;
    public GameObject defPre;
    public GameObject rangePre;
    public GameObject speedPre;
    public GameObject shieldPre;
	public GameObject bloodSmallPre;
	public GameObject bloodBigPre;
    public GameObject boomPre;
    public GameObject viewPre;
    public GameObject enemyTankPre;  //  生成系统的坦克。
    public GameObject wallPre;
    public GameObject testPre;


    public int expPre_Count;
	public int expBigPre_Count;
    public int bigBullet_Count;
    public int atkPre_Count;
    public int defPre_Count;
    public int rangePre_Count;
    public int speedPre_Count;
    public int shieldPre_Count;
	public int bloodSmallPre_Count;
	public int bloodBigPre_Count;
	public int boomPre_Count;
    public int viewPre_Count;

   // public GameObject


    public void Start()
    {

        //Cmdc();
        //this.StartCoroutine(Product());
        //GameObject o = Instantiate(this.testPre, this.testPre.transform.position, this.testPre.transform.rotation);
        //NetworkServer.Spawn(o);

        Product_WALLPre();

        for (int i = 0; i < this.bigBullet_Count; i++)
        {  //  生产大子弹。

            Product_BigBulletPre();

        }

        for (int i = 0; i < this.expPre_Count; i++)
        {  //   生产经验。

            Product_ExpPre();

        }

        for (int i = 0; i < this.expBigPre_Count; i++)
        {  //  生产大子弹。

            Product_ExpBigPre();

        }

        for (int i = 0; i < this.atkPre_Count; i++)
        {

            Product_ATKPre();

        }

        for (int i = 0; i < this.defPre_Count; i++)
        {

            Product_DEFPre();

        }

        for (int i = 0; i < this.rangePre_Count; i++)
        {

            Product_RANGEPre();

        }

        for (int i = 0; i < this.speedPre_Count; i++)
        {

            Product_SPEEDPre();

        }

        for (int i = 0; i < this.shieldPre_Count; i++)
        {

            Product_SHIELDPre();

        }

        for (int i = 0; i < this.bloodSmallPre_Count; i++)
        {

            Product_BLOODSMALLPre();

        }

        for (int i = 0; i < this.bloodBigPre_Count; i++)
        {

            Product_BLOODBIGPre();

        }

        for (int i = 0; i < this.boomPre_Count; i++)
        {

            Product_BOOMPre();

        }

        for (int i = 0; i < this.viewPre_Count; i++)
        {

            Product_VIEWPre();

        }

    }
    //[Command]
    //public void Cmdc() { }

    // Update is called once per frame
    IEnumerator Product() {

        yield return new WaitForSeconds(2f);
        for (int i = 0; i < this.bigBullet_Count; i++)
        {  //  生产大子弹。

            Product_BigBulletPre();

        }

        for (int i = 0; i < this.expPre_Count; i++)
        {  //   生产经验。

            Product_ExpPre();

        }

        for (int i = 0; i < this.expBigPre_Count; i++)
        {  //  生产大子弹。

            Product_ExpBigPre();

        }

        for (int i = 0; i < this.atkPre_Count; i++)
        {

            Product_ATKPre();

        }

        for (int i = 0; i < this.defPre_Count; i++)
        {

            Product_DEFPre();

        }

        for (int i = 0; i < this.rangePre_Count; i++)
        {

            Product_RANGEPre();

        }

        for (int i = 0; i < this.speedPre_Count; i++)
        {

            Product_SPEEDPre();

        }

        for (int i = 0; i < this.shieldPre_Count; i++)
        {

            Product_SHIELDPre();

        }

        for (int i = 0; i < this.bloodSmallPre_Count; i++)
        {

            Product_BLOODSMALLPre();

        }

        for (int i = 0; i < this.bloodBigPre_Count; i++)
        {

            Product_BLOODBIGPre();

        }

        for (int i = 0; i < this.boomPre_Count; i++)
        {

            Product_BOOMPre();

        }

    }

    //public override void OnStartServer()
    //{
    //    //Debug.Log(111);
    //}

    //[Command]
    public void Product_ExpPre() {

        //Debug.Log("111");
        GameObject o = Instantiate(this.expPre, this.transform.position + new Vector3(Random.Range(-100f, 100f), 0.5f, Random.Range(-100f, 100f)), this.transform.rotation);
        //o.GetComponent<Grade_Trigger>().enabled = false;
        //Destroy(o.GetComponent<Grade_Trigger>());
        NetworkServer.Spawn(o);
        //o.AddComponent<Grade_Trigger>();
        o.GetComponent<Grade_Trigger>().enabled = true;
        o.GetComponent<BoxCollider>().enabled = true;

    }

	public void Product_ExpBigPre(){
	
		//Debug.Log("111");
		GameObject o = Instantiate(this.expBigPre, this.transform.position + new Vector3(Random.Range(-100f, 100f), 0.5f, Random.Range(-100f, 100f)), this.transform.rotation);
		//o.GetComponent<Grade_Trigger>().enabled = false;
		//Destroy(o.GetComponent<Grade_Trigger>());
		NetworkServer.Spawn(o);
		//o.AddComponent<Grade_Trigger>();
		o.GetComponent<Grade_Trigger>().enabled = true;
		o.GetComponent<BoxCollider>().enabled = true;
	
	}

    public void Product_BigBulletPre()
    {

        //Debug.Log("111");
        GameObject o = Instantiate(this.bigBullerPre, this.transform.position + new Vector3(Random.Range(-100f, 100f), 2f, Random.Range(-100f, 100f)), this.transform.rotation);
        NetworkServer.Spawn(o);
        o.GetComponent<BoxCollider>().enabled = true;

    }

    public void Product_ATKPre() {

        GameObject o = Instantiate(this.atkPre, this.transform.position + new Vector3(Random.Range(-100f, 100f), 2f, Random.Range(-100f, 100f)), this.transform.rotation);
        NetworkServer.Spawn(o);
        o.GetComponent<BoxCollider>().enabled = true;

    }

    public void Product_DEFPre() {

        GameObject o = Instantiate(this.defPre, this.transform.position + new Vector3(Random.Range(-100f, 100f), 2f, Random.Range(-100f, 100f)), this.transform.rotation);
        NetworkServer.Spawn(o);
        o.GetComponent<BoxCollider>().enabled = true;

    }

    public void Product_RANGEPre() {

        GameObject o = Instantiate(this.rangePre, this.transform.position + new Vector3(Random.Range(-100f, 100f), 2f, Random.Range(-100f, 100f)), this.transform.rotation);
        NetworkServer.Spawn(o);
        o.GetComponent<BoxCollider>().enabled = true;

    }

    public void Product_SPEEDPre()
    {

        GameObject o = Instantiate(this.speedPre, this.transform.position + new Vector3(Random.Range(-100f, 100f), 2f, Random.Range(-100f, 100f)), this.transform.rotation);
        NetworkServer.Spawn(o);
        o.GetComponent<BoxCollider>().enabled = true;

    }

    public void Product_SHIELDPre()
    {

        GameObject o = Instantiate(this.shieldPre, this.transform.position + new Vector3(Random.Range(-100f, 100f), 2f, Random.Range(-100f, 100f)), this.transform.rotation);
        NetworkServer.Spawn(o);
        o.GetComponent<BoxCollider>().enabled = true;

    }

	public void Product_BLOODSMALLPre(){

		GameObject o = Instantiate(this.bloodSmallPre, this.transform.position + new Vector3(Random.Range(-100f, 100f), 0.5f, Random.Range(-100f, 100f)), this.transform.rotation);
		NetworkServer.Spawn(o);
		o.GetComponent<BoxCollider>().enabled = true;

	}

	public void Product_BLOODBIGPre(){
	
		GameObject o = Instantiate(this.bloodBigPre, this.transform.position + new Vector3(Random.Range(-100f, 100f), 0.5f, Random.Range(-100f, 100f)), this.transform.rotation);
		NetworkServer.Spawn(o);
		o.GetComponent<BoxCollider>().enabled = true;
	
	}

	public void Product_BOOMPre(){

		GameObject o = Instantiate(this.boomPre, this.transform.position + new Vector3(Random.Range(-100f, 100f), 0.5f, Random.Range(-100f, 100f)), this.transform.rotation);
		//Instantiate (,);
		o.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
		//Instantiate();
		NetworkServer.Spawn(o);
		o.GetComponent<BoxCollider>().enabled = true;

	}

    public void Product_VIEWPre() {

        GameObject o = Instantiate(this.viewPre, this.transform.position + new Vector3(Random.Range(-100f, 100f), 0.5f, Random.Range(-100f, 100f)), this.transform.rotation);
        NetworkServer.Spawn(o);
        o.GetComponent<BoxCollider>().enabled = true;

    }

    public void Product_ENEMYTankePre()
    {

        StartCoroutine(ProductEnemyTankPre());
        
        //o.GetComponent<BoxCollider>().enabled = true;

    }

    public void Product_WALLPre()
    {

        GameObject o = Instantiate(this.wallPre, this.transform.position, this.transform.rotation);
        //Instantiate (,);
        
        //Instantiate();
        NetworkServer.Spawn(o);
        

    }

    IEnumerator ProductEnemyTankPre() {

        yield return new WaitForSeconds(60f);
        GameObject o = Instantiate(this.enemyTankPre, this.transform.position, this.transform.rotation);
        //Instantiate (,);
        //o.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        //Instantiate();
        NetworkServer.Spawn(o);
        //  然后在将各个脚本激活。
        o.GetComponent<EnemyTankFiringBullet>().enabled = true;
        o.GetComponent<EnemyTankHealth>().enabled = true;
        o.GetComponent<EnemyTankTransforms>().enabled = true;
        o.transform.Find("TankRenderers").GetComponent<EnemyBoxcollider>().enabled = true;
        o.transform.Find("TankRenderers").GetComponent<BoxCollider>().enabled = true;

    }

}
