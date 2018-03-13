using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//  这个是tank信息的类，包括各个tank的属性，比如攻击、防御、射程。

public class TankInfomation : NetworkBehaviour {

    public float level = 1;  //  等级
    public float total_Health = 100;
    public float current_Health = 100;
    public float total_Exp = 100;
    //[SyncVar (hook = "OnChangeCurrent_Exp")]
    public float current_Exp = 0; 
    public float attack = 10;
    public float defense = 0;
    public float range = 5;
    public float speed = 1;
    public int count_ = 0;  //  这个是用来计数的，当count_一到四，就要坦克变大，速度变慢，来平衡游戏。
    public int tank_Number;  //  坦克的编号。
	public bool isWuDi = false;  //  用来判断当前坦克是不是处于无敌状态。
	public bool isBoom = false;  //  用来判断子弹是否可以穿墙。
    
    [SyncVar (hook = "OnChangeGrade")]
    public float grade = 0;  //  坦克的得分。
    [SyncVar(hook = "OnChangeName")]
    public string tankName = "消愁";
    public UIUpdate uiUpdate;
    public Update_Ranking update_Ranking;  //  排名的面板。
    public Tank_Health tank_Health;
    public GameObject shieldPre;  //  护盾的特效。
    public Subtitle subtitle;  //  字幕的ui。
    public Color c;  //  这个是用来记录坦克的颜色，因为要同步到各个客户端。


    public void Start()
    {
        this.tank_Number = ++PublicProperty.Tank_Count;
        
        

        if (this.isLocalPlayer == false) return;
        //  赋值uiupdate，并且将uiupdate的tankinformation的属性=这个类身上的对象的tankinformation组件。
        this.uiUpdate = GameObject.Find("Canvas").transform.Find("TankInfo").GetComponent<UIUpdate>();
        //this.uiUpdate.gameObject.SetActive(true);
        this.uiUpdate.tankInformation = this;
        //  获取名字。
        //  让坦克的显示名字的3dtext赋值。
        this.transform.Find("New Text").GetComponent<TextMesh>().text = GameObject.Find("ControlUI").GetComponent<ControlUI>().tankName;

        //GameObject.Find("ControlUI").GetComponent<ControlUI>().self = this;
        //GameObject.Find("ControlUI").GetComponent<ControlUI>().GengXinName();
        //  寻Update_Ranking组件，并将其tank_self赋值。
        this.update_Ranking = GameObject.Find("Canvas").transform.Find("Ranking").GetComponent<Update_Ranking>() ;
        this.update_Ranking.tank_Self = this;
        CmdSetTankName(GameObject.Find("ControlUI").GetComponent<ControlUI>().tankName);
        //this.grade = 1;
        this.subtitle = GameObject.Find("Canvas").transform.Find("Subtitle").GetComponent<Subtitle>() ;
        this.update_Ranking.Update_UI();
        this.uiUpdate.UpdateUI();

        //  下面是随机生成坦克的颜色。
        this.c = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        //Debug.Log(c);
        this.transform.Find("TankRenderers/TankChassis").GetComponent<MeshRenderer>().material.color = this.c;
        this.transform.Find("TankRenderers/TankTracksLeft").GetComponent<MeshRenderer>().material.color = this.c;
        this.transform.Find("TankRenderers/TankTracksRight").GetComponent<MeshRenderer>().material.color = this.c;
        this.transform.Find("TankRenderers/TankTurret").GetComponent<MeshRenderer>().material.color = this.c;
        CmdTankColor(this.c.r, this.c.g, this.c.b);  //  先向服务器发送自身坦克的颜色，然后在从服务器端调用相应的函数来改变客户端的颜色。
                                                     //  当没加入一辆坦克，就要从服务器端遍历所有的坦克，然后更新其颜色的显示，否则之前加入的坦克颜色该不一样了。

        //  下面是将AudioListener激活。
        this.GetComponent<AudioListener>().enabled = true;


    }

    //public void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.K))
    //    {

    //        this.tankName = "uio";

    //    }
    //}

    public void Update_UI() {

        if (this.isLocalPlayer == false) return;  //  如果不是本地的玩家，那么不用调用更新UI的方法。
     //   Debug.Log("diaoyong");
		if (this.uiUpdate == null) {
		
		//	Debug.Log ("opop");
			return;

		}
        this.uiUpdate.UpdateUI();

    }
    [Command]
    public void CmdSetTankName(string name_) {

        this.tankName = name_;
        //  Debug.Log("a" + this.tankName);
        //  下面是为了将坦克的名字同步到各个客户端上。
        GameObject[] tanks = GameObject.FindGameObjectsWithTag("Tank");
        for(int i = 0; i < tanks.Length; i++)
            tanks[i].GetComponent<TankInfomation>().RpcSetTankNameText(tanks[i].GetComponent<TankInfomation>().tankName);

    }

    [ClientRpc]
    public void RpcSetTankNameText(string name_) {  //  这个是用来同步坦克身上显示名字的组件的。

        if (this.isLocalPlayer == true) return;

        this.transform.Find("New Text").GetComponent<TextMesh>().text = name_;

    }

    [Command]
    public void CmdTankColor(float r, float g, float b) {  //  向客户端发送坦克的颜色。

        this.c = new Color(r, g, b);

        this.transform.Find("TankRenderers/TankChassis").GetComponent<MeshRenderer>().material.color = this.c;
        this.transform.Find("TankRenderers/TankTracksLeft").GetComponent<MeshRenderer>().material.color = this.c;
        this.transform.Find("TankRenderers/TankTracksRight").GetComponent<MeshRenderer>().material.color = this.c;
        this.transform.Find("TankRenderers/TankTurret").GetComponent<MeshRenderer>().material.color = this.c;
        StartOnServer.isUpdateColor = true;  //  告诉该脚本可以更新坦克的颜色了。因为服务器端已经赋好了值。

    }

    [ClientRpc]
    public void RpcTankColor(float r, float g, float b) {

        if (this.isLocalPlayer == true) return;  //  只需要同步不是本地的就行。
        this.c = new Color(r, g, b);
        //Debug.Log(c);
        this.transform.Find("TankRenderers/TankChassis").GetComponent<MeshRenderer>().material.color = this.c;
        this.transform.Find("TankRenderers/TankTracksLeft").GetComponent<MeshRenderer>().material.color = this.c;
        this.transform.Find("TankRenderers/TankTracksRight").GetComponent<MeshRenderer>().material.color = this.c;
        this.transform.Find("TankRenderers/TankTurret").GetComponent<MeshRenderer>().material.color = this.c;



    }

    public void OnChangeGrade(float gra) {
        //  当分数发生变化的时候应该把这个方法发送到各个客户端，让各个客户端来调用这个方法。
        //  这个方法主要是直接通过寻找到排名的ui组件，之后通过ui组件来更新显示排名信息。
        //Debug.Log("rank");
        this.grade = gra;
        GameObject.Find("Canvas").transform.Find("Ranking").GetComponent<Update_Ranking>().Update_UI();

    }

    public void OnChangeName(string s) {
        //  当名字改变时，将该方法发送到各个客户端。
        //Debug.Log("text");
        //Debug.Log("b" + s);
        this.tankName = s;
        //  当一改变姓名，排名面板也要变。
        GameObject.Find("Canvas").transform.Find("Ranking").GetComponent<Update_Ranking>().Update_UI();
        this.Update_UI();


    }

    public void GetExperiencee(float exp)
    {   //  获得经验的方法。

        this.current_Exp += exp;  //  Exp++
        //this.Update_UI();
        while (this.current_Exp >= this.total_Exp)
        {  //  循环判断当前经验是不是大于升级经验，是的话就升级。
            
            this.count_++;
            if (this.count_ == 4)
            {


                //  这个里面不能用cmd函数，因为当前的经验是来自服务器的，所以从服务器上在调用cmd方法发送到服务器是不对的。只能从客户端发送到服务器。
                //BecomeBig();  //  先发送给服务器，然后在发向各个客户端，因为如果服务器的坦克不变大，那么发出来的子弹的位置就不一样了。
                //Cmdc();
                BecomeBig();
                RpcBecomeBig();


            }
            this.current_Exp -= this.total_Exp;
            this.total_Exp *= 1.5f;
            UpGrade();
        }

    }

    [ClientRpc]
    public void RpcGetExperiencee(float exp) {   //  获得经验的方法。
        //Debug.Log("111222");
        if (this.isServer) {
            //  如果这个客户端同时是服务器端，那么就不用执行了，因为在服务器端已经执行一次了。
            this.Update_UI();
            //Debug.Log("server");
            return;

        }
        if (this.isLocalPlayer == false) return;
        this.current_Exp += exp;  //  Exp++
        //if (this.isLocalPlayer == true)
        this.Update_UI();
        //this.Update_UI();
        while (this.current_Exp >= this.total_Exp)
        {  //  循环判断当前经验是不是大于升级经验，是的话就升级。
           // Debug.Log("jinru");
            this.count_++;
            if (this.count_ == 4)
            {


                //  这个里面不能用cmd函数，因为当前的经验是来自服务器的，所以从服务器上在调用cmd方法发送到服务器是不对的。只能从客户端发送到服务器。
                //BecomeBig();  //  先发送给服务器，然后在发向各个客户端，因为如果服务器的坦克不变大，那么发出来的子弹的位置就不一样了。
                //Cmdc();
                BecomeBig();


            }
            this.current_Exp -= this.total_Exp;
            this.total_Exp *= 1.5f;
            UpGrade();
        }

    }

    public void OnChangeCurrent_Exp(float f) {

        //Debug.Log()
        this.current_Exp = f;
        if (this.isLocalPlayer == true) this.Update_UI();
        while (this.current_Exp >= this.total_Exp)
        {  //  循环判断当前经验是不是大于升级经验，是的话就升级。

            this.count_++;
            if (this.count_ == 3)
            {


                //  这个里面不能用cmd函数，因为当前的经验是来自服务器的，所以从服务器上在调用cmd方法发送到服务器是不对的。只能从客户端发送到服务器。
                //BecomeBig();  //  先发送给服务器，然后在发向各个客户端，因为如果服务器的坦克不变大，那么发出来的子弹的位置就不一样了。
                //Cmdc();
                BecomeBig();


            }
            this.current_Exp -= this.total_Exp;
            this.total_Exp *= 1.5f;
            UpGrade();
        }


    }

    //[ClientRpc]
    public void BecomeBig() {  //  让身体变大，速度变慢。

        //if (this.isServer == true) return;  //  如果当前的客户端同时也是服务器，那么就不用改变了。因为前面已经改变了。
      //  Debug.Log("4");
        if (this.transform.localScale.x <= 1.7f) {

            this.transform.localScale = new Vector3(this.transform.localScale.x * 1.2f, this.transform.localScale.y * 1.2f, this.transform.localScale.z * 1.2f);
            this.GetComponent<Tank_Move>().move_Speed -= this.GetComponent<Tank_Move>().move_Speed * 0.2f;

        }
        
        //this.attack *= 1.2f;
        
        this.count_ = 0;
        //Cmdc();

    }

    [ClientRpc]
    public void RpcBecomeBig() {

        if (this.isLocalPlayer == true) return;  //  如果是本地的玩家，就不变大了，因为在获得经验的方法中有变大的，并且是在本地运行的。
      //  Debug.Log("4");
        if (this.transform.localScale.x <= 1.7f)
        {

            this.transform.localScale = new Vector3(this.transform.localScale.x * 1.2f, this.transform.localScale.y * 1.2f, this.transform.localScale.z * 1.2f);
            this.GetComponent<Tank_Move>().move_Speed -= this.GetComponent<Tank_Move>().move_Speed * 0.2f;

        }

        //this.count_ = 0;

    }

    [Command]
    public void Cmdc() {

        this.transform.localScale = new Vector3(this.transform.localScale.x * 2f, this.transform.localScale.y * 2f, this.transform.localScale.z * 2f);
        this.GetComponent<Tank_Move>().move_Speed -= this.GetComponent<Tank_Move>().move_Speed * 0.2f;
        //this.attack *= 1.2f;

        this.count_ = 0;
      //  Debug.Log("11111111111111");


    }

    public void AddAtk(float f) {

        
      //  Debug.Log("server ATK" + f);
        this.attack += f;
        RpcAddATK(f);

    }

    [ClientRpc]
    public void RpcAddATK(float f) {

        Debug.Log(f);
        if (this.isServer == true) return;
        if (this.isLocalPlayer == false) return;
        this.attack += f;
        if (this.subtitle == null) return;
        this.subtitle.gameObject.SetActive(true);
        this.subtitle.SetSubtitleContent("攻击+" + f);
        //if (this.isLocalPlayer == true)
        this.Update_UI();
        

    }

    public void AddDef(float f)
    {

     //   Debug.Log("server DEF" + f);
        this.defense += f;
        RpcAddDEF(f);

    }

    [ClientRpc]
    public void RpcAddDEF(float f)
    {

        Debug.Log(f);
        if (this.isServer == true) {

            this.subtitle.gameObject.SetActive(true);
            this.subtitle.SetSubtitleContent("防御+" + f);
            return;

        } 
        if (this.isLocalPlayer == false) return;
        this.defense += f;
        if (this.subtitle == null) return;
        this.subtitle.gameObject.SetActive(true);
        this.subtitle.SetSubtitleContent("防御+" + f);
        //if (this.isLocalPlayer == true)
        this.Update_UI();

    }

    public void AddRange(float f) {

     //   Debug.Log("server RANGE" + f);
        this.range += f;
        this.GetComponent<FiringBullets>().firePower = this.range;
        RpcAddRANGE(f);

    }

    [ClientRpc]
    public void RpcAddRANGE(float f)
    {

      //  Debug.Log("Range" + f);
        if (this.isServer == true) return;
        if (this.isLocalPlayer == false) return;
        this.range += f;
        if (this.subtitle == null) return;
        this.subtitle.gameObject.SetActive(true);
        this.subtitle.SetSubtitleContent("射程+" + f);
        //if (this.isLocalPlayer == true)
        this.Update_UI();

    }

    public void AddSpeed(float f)
    {

      //  Debug.Log("server SPEED" + f);
        if (this.speed >= 2.0f) {

            return;

        }
        this.speed += f;
        this.GetComponent<Tank_Move>().move_Speed = this.speed;
        RpcAddSPEED(f);

    }

    [ClientRpc]
    public void RpcAddSPEED(float f)
    {

      //  Debug.Log("Speed" + f);
        if (this.isServer == true) return;
        if (this.isLocalPlayer == false) return;
        this.speed += f;
        this.GetComponent<Tank_Move>().move_Speed = this.speed;
        if (this.subtitle == null) return;
        this.subtitle.gameObject.SetActive(true);
        this.subtitle.SetSubtitleContent("速度+" + f);
        //if (this.isLocalPlayer == true)
        //this.Update_UI();

    }

    public void AddShield()
    {

        //Debug.Log("server SPEED" + f);
        //this.speed += f;
        //this.GetComponent<Tank_Move>().move_Speed = this.speed;
        GameObject obj = Instantiate(this.shieldPre, this.transform);  //  生成护盾特效，是该坦克的子物体。
        obj.GetComponent<BoxCollider>().enabled = true;  //  把碰撞器打开。
        obj.transform.localScale = Vector3.one;
        //this.gameObject.GetComponent<BoxCollider>().enabled = false;
		this.isWuDi = true;  //  坦克无敌状态标志。
		StartCoroutine(CloseWuDi());
        RpcAddSHIELD();

    }

	IEnumerator CloseWuDi() {  //  打开坦克的碰撞器在无敌消失之后。

		yield return new WaitForSeconds(5f);
		this.isWuDi = false;  //  让坦克的无敌状态消失。

	}

    IEnumerator OpenBoxCollider() {  //  打开坦克的碰撞器在无敌消失之后。

        yield return new WaitForSeconds(30f);
        this.gameObject.GetComponent<BoxCollider>().enabled = true;

    }

    [ClientRpc]
    public void RpcAddSHIELD()
    {

        //Debug.Log("Speed" + f);
        if (this.isServer == true) return;
        //if (this.isLocalPlayer == false) return;
        //this.speed += f;
        //this.GetComponent<Tank_Move>().move_Speed = this.speed;
        GameObject obj = Instantiate(this.shieldPre, this.transform);
        obj.transform.localScale = Vector3.one;
        if (this.subtitle == null) return;
		if (this.isLocalPlayer == false)
			return;  //  如果不是本地的用户，那么不用显示。
        this.subtitle.gameObject.SetActive(true);
        this.subtitle.SetSubtitleContent("无敌5秒钟");
        //if (this.isLocalPlayer == true)
        //this.Update_UI();

    }

	public void AddBlood(float f)  //  调用加血的方法。
	{

	//	Debug.Log("server BLOOD" + f);
		if (this.current_Health == this.total_Health)
			return;  //  如果满血，那么就不用加血。
		this.current_Health += f;
		if (this.current_Health > this.total_Health) {
		//  如果加血之后大于总的血量，那么让它跟总的血量相等。

			this.current_Health = this.total_Health;
		
		}
		this.tank_Health.currentHealth = this.current_Health;
		this.tank_Health.slider.value = this.current_Health / this.total_Health;
	//	Debug.Log ("yu" + this.current_Health);
		//this.Update_UI ();
		RpcAddBLOOD(this.tank_Health.slider.value, this.current_Health);

	}

	[ClientRpc]
	public void RpcAddBLOOD(float f, float ff)
	{

	//	Debug.Log("Blood" + ff);
		if (this.isServer == true)
			return;
		if (this.isLocalPlayer == true) {
			this.current_Health = ff;//  然后将本地坦克的当前生命值更新，为了更新ui。
			//Debug.Log("rpc" + this.current_Health);
			this.Update_UI ();  //  如果是本地的用户，那么更新一下ui。

		
		}
			
		//if (this.isLocalPlayer == false) return;

		this.tank_Health.slider.value = f;  //  将该坦克的血条改了。

		//if (this.isLocalPlayer == true)
		//this.Update_UI();

	}

	public void AddBoom()  //  子弹可以穿透墙壁的方法。
	{

	//	Debug.Log("server BOOM");
		this.isBoom = true;  //  然子弹可以穿透墙壁。


		RpcAddBOOM();

	}

	[ClientRpc]
	public void RpcAddBOOM()
	{

		//Debug.Log("Blood" + ff);
		if (this.isServer == true)
			return;
		if (this.isLocalPlayer == true) {
			
			if (this.subtitle == null) return;
			this.subtitle.gameObject.SetActive(true);
			this.subtitle.SetSubtitleContent("子弹可以破坏墙体。");

		}



	}

    public void AddView()  //  视野变大
    {

        //Debug.Log("server BOOM");
        //this.isBoom = true;  //  然子弹可以穿透墙壁。


        RpcAddVIEW();

    }

    [ClientRpc]
    public void RpcAddVIEW()
    {

        //Debug.Log("Blood" + ff);
        //if (this.isServer == true)
           // return;
        if (this.isLocalPlayer == true)
        {

            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().fieldOfView += 10f;

        }



    }

    private void UpGrade() {  //  升级的方法。

        
        this.level++;
        this.current_Health += 50f;
        this.total_Health += 50f;
		//if (this.isServer || this.isLocalPlayer) {
		
		this.tank_Health.slider.value = this.current_Health / this.total_Health;

		//}
        this.attack += 5f;
        this.defense += 2;
        this.range += 5;
        this.GetComponent<FiringBullets>().firePower = this.range;
        this.tank_Health.currentHealth = this.current_Health;
        this.tank_Health.healthTotal = this.total_Health;
        if (this.isLocalPlayer == false) return;  //  是本地的用户才更新。
        Update_UI();  //  更新坦克ui显示。
    }


}

