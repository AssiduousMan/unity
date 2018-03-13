using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;

//  这个是控制坦克血量的类，来控制坦克的各项属性。

public class Tank_Health : NetworkBehaviour {

    public float healthTotal = 100;
    //[SyncVar (hook = "OnChangeHealth")]
    public float currentHealth;  //  坦克的血量。
    public GameObject tankExplosionPre;//  坦克爆炸的预设体。
    public AudioClip tankExplosionAudio;  //  坦克爆炸的音效。
    public Slider slider;  //  血条。
    public TankInfomation tankInformation;  //  坦克信息的类。
    public Image screenShotImage;  //  这个是用来显示截屏的图片。
    public GameObject gameOverPanel;  //  当坦克死亡时出现的ui。
    public GameObject ima;  //  测试。

    private void Start()
    {
        this.tankInformation = this.GetComponent<TankInfomation>();
        this.healthTotal = this.tankInformation.total_Health;
        this.currentHealth = this.healthTotal;
        this.slider.enabled = false;
        if (this.isLocalPlayer == true) {
            //  如果是本地的用户。

            //  当tank连接上客户端的时候，需要先同步一下系统坦克的血量。

            //Debug.Log("asdasd");  //  将自身的属性赋值。
            this.screenShotImage = GameObject.Find("Canvas").transform.Find("GameOverPanel/Image").GetComponent<Image>();
            this.gameOverPanel = GameObject.Find("Canvas").transform.Find("GameOverPanel").gameObject;
            this.ima = GameObject.Find("Canvas").transform.Find("Image").gameObject;
        }

    }

    //private void Update()
    //{

    //    if (this.isLocalPlayer) {

    //        if (Input.GetKeyDown(KeyCode.Space)) {

    //            Debug.Log("退出服务器");
    //            GameObject.Find("UnetManage").GetComponent<NetworkManager>().StopClient();

    //        }
            

    //    }

    //}

    public void TakeDamage(float dama, int number) {
       // Debug.Log("123");
        //Debug.Log("tank:" + number);
        dama = dama - this.tankInformation.defense;   //  最后的伤害等于传过来的伤害减去当前坦克的防御。
        if (dama < 0) dama = 0;
        if (this.currentHealth <= 0) return;
        this.currentHealth -= dama;
        if (this.currentHealth <= 0)
            this.tankInformation.current_Health = 0;
        else
            this.tankInformation.current_Health = this.currentHealth;
        if (this.currentHealth <= 0) {
            //  说明已经死亡。

            if(number != 100) {  //  说明不是系统坦克打死的，那么给坦克加经验。 
                GameObject[] tanks = GameObject.FindGameObjectsWithTag("Tank");
                for (int i = 0; i < tanks.Length; i++) {

                    TankInfomation trans = tanks[i].GetComponent<TankInfomation>();

                    if (trans.tank_Number == number) {  //  找到了该坦克

                       // Debug.Log("gaitank:" + number);

                        trans.grade += 50;
                        trans.GetExperiencee(100);
                        trans.RpcGetExperiencee(100);
                        break;

                    }
                

                }
            }
            

            this.currentHealth = 0;
            this.tankInformation.current_Health = 0;
            //  实例化爆照效果。
            GameObject.Instantiate(this.tankExplosionPre, this.transform.position, this.transform.rotation);
            //  播放坦克爆炸音效。
            AudioSource.PlayClipAtPoint(this.tankExplosionAudio, this.transform.position);
            GameObject.Destroy(this.gameObject);
            RpcTankBoom();
            //TankDead();

        }
        this.slider.value = this.currentHealth / this.healthTotal;
        
        //if(this.isLocalPlayer == true)
            this.tankInformation.Update_UI();  //  更新ui；
        RpcOnChangeHealth(this.slider.value, this.currentHealth);

    }

    [ClientRpc]
    public void RpcOnChangeHealth(float value, float current)
    {  //  当health的值改变的时候会发送大各个客户端来调用这个方法。
       // Debug.Log(value);
        if (this.tankInformation == null) return;
       // Debug.Log("asd" + value);
        this.slider.value = value;
        if (this.isLocalPlayer == true && this.tankInformation.uiUpdate != null)
        {

           // Debug.Log("ceshi");
            this.currentHealth = current;
            this.tankInformation.current_Health = current;
           // Debug.Log(current);
            this.tankInformation.Update_UI();  //  更新ui；

        }

    }

    [ClientRpc]
    public void RpcSyncHealth(float value, Vector3 scale)  //  这个方法是为了当有客户端链接时，将当前坦克的信息发送一下，用来同步。
    {  //  当health的值改变的时候会发送大各个客户端来调用这个方法。
        //Debug.Log(value);
        //if (this.tankInformation == null) return;

        if (this.isLocalPlayer == true) return;
        this.slider.value = value;
        this.transform.localScale = scale;


    }

    //private void OnChangeHealth(float heal)
    //{  //  当health的值改变的时候会发送大各个客户端来调用这个方法。
    //    if (this.tankInformation == null) return;
    //    this.tankInformation.current_Health = heal;  //  先让坦克的信息类改变当前的血量。
    //    this.slider.value = heal / this.healthTotal;
    //    if (this.isLocalPlayer == true && this.tankInformation.uiUpdate != null) {

    //        Debug.Log("ceshi");
    //        this.tankInformation.Update_UI();  //  更新ui；

    //    }

    //}

    public void TankDead() {
        //  坦克死亡时调用的方法。
        if (this.isLocalPlayer == true) {


            //  然后需要截屏显示。
            //this.screenShotImage.gameObject.SetActive(true);  //  将截屏的图片显示激活.
            //StartCoroutine(GetCapture());


            //GetCapture();


            CapTure();

            this.gameOverPanel.SetActive(true);
            //  如果是本地的用户，首先死亡之后要退出客户端。
            //Debug.Log("退出服务器");
            //GameObject.Find("UnetManage").GetComponent<NetworkManager>().StopClient();

        }   


    }

    public void /*IEnumerator*/ GetCapture()  //  这是压缩截屏的方法。
    {
      //  Debug.Log("jingping1");
        //yield return new WaitForEndOfFrame();
       // Debug.Log("jingping2");
        int width = Screen.width;
        int height = Screen.height;
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0, true);
        //byte[] imagebytes = tex.EncodeToPNG();//转化为png图
        //tex.Compress(false);//对屏幕缓存进行压缩
        tex.Apply();
        //this.image.mainTexture = tex;//对屏幕缓存进行显示（缩略图）
        Sprite tempSp = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0));//将图转化为ui的贴图  
        //m_tSprite.GetComponent<Image>().sprite = tempSp;//贴在ui上  
        this.screenShotImage.sprite = tempSp;
        //File.WriteAllBytes(Application.dataPath + "/screencapture.png", imagebytes);//存储png图
    //    Debug.Log("jieping3");
    }

    public void CapTure() {


        //获取系统时间并命名相片名  
        System.DateTime now = System.DateTime.Now;
        string times = now.ToString();
        times = times.Trim();
        times = times.Replace("/", "-");
        string filename = "Screenshot" + times + ".png";
        //判断是否为Android平台  
        if (Application.platform == RuntimePlatform.Android)
        {

            //截取屏幕  
            int width = Screen.width;
            int height = Screen.height;
            Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
            tex.ReadPixels(new Rect(0, 0, width, height), 0, 0, true);
            //byte[] imagebytes = tex.EncodeToPNG();//转化为png图
            //tex.Compress(false);//对屏幕缓存进行压缩
            tex.Apply();
            //this.image.mainTexture = tex;//对屏幕缓存进行显示（缩略图）
            Sprite tempSp = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0));//将图转化为ui的贴图  
                                                                                                         //m_tSprite.GetComponent<Image>().sprite = tempSp;//贴在ui上  
            this.screenShotImage.sprite = tempSp;
            //转为字节数组  
            byte[] bytes = tex.EncodeToPNG();

            string destination = "/sdcard/DCIM/ARphoto";
            DirectoryInfo mydir = new DirectoryInfo(destination);

            //判断目录是否存在，不存在则会创建目录
            if (!Directory.Exists(destination))
            {
                this.ima.SetActive(true);
                //Debug.Log("yyyyy");
                Directory.CreateDirectory(destination);

            }

            //Debug.Log("yyyyy");
            //if (mydir.Exists) {

            //    print("文件夹不存在,创建");
            //    Directory.CreateDirectory(destination);

            //}
            //if (!System.IO.Exists(destination))
            //{
            //    System.IO.CreateDirectory(destination);
            //    print("文件夹不存在,创建");
            //}
            string Path_save = destination + "/" + filename;
            //存图片  

            System.IO.File.WriteAllBytes(Path_save, bytes);
        }
        else {

            GetCapture();  //  调用客户端的截屏的方法。

        }

    }

    [ClientRpc]
    public void RpcTankBoom() {  //  坦克爆炸的特效。

        if (this.isServer == true) return;

        //  实例化爆照效果。
        GameObject.Instantiate(this.tankExplosionPre, this.transform.position, this.transform.rotation);
        //  播放坦克爆炸音效。
        AudioSource.PlayClipAtPoint(this.tankExplosionAudio, this.transform.position);
        GameObject.Destroy(this.gameObject);
        TankDead();
    }



}
