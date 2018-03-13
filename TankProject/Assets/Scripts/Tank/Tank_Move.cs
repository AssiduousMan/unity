using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

using UnityEngine.EventSystems;

public class Tank_Move : NetworkBehaviour {

    public float move_Speed = 1;
    public int angualr_Speed = 30;  //  这个是坦克旋转的速度
    private Rigidbody rigi;
    public RectTransform joy;  //  行走的摇杆。
    public RectTransform joy_FaShe;  //  发射的摇杆
    public GameObject paoTong;  //  坦克身上的炮筒。
    public Camera camera_;
    public float camera_Distance_Y = 20f;  //  这个是控制摄像机的位置的。
    public float camera_Distance_Z = -10f;

    public AudioSource audio_;
    public AudioClip engineIdle;  //  坦克静止的声音。
    public AudioClip engineDriving;  //  坦克开动的声音。
    public NetworkTransform networkTransform;
    public UIUpdate uiUpdate;  //  这个是控制UI更新的类。

    public bool isUpdateStaticMove = false;  //  用来判断是否可以在坦克静止的时候向其他客户端发送位置信息。
    public float cutTime = 2f;  //  这个是用来倒计时的。因为当坦克不动时，为了解决延迟的问题，会继续同步2s的坦克位置。

    //[SyncVar (hook = "OnChangePosi")]
    public Vector3 po;


	// Use this for initialization
	void Start () {

        this.rigi = this.GetComponent<Rigidbody>();
        this.joy = GameObject.Find("Canvas").transform.Find("YaoGan_ControllMove/Joy").GetComponent<RectTransform>();
        this.joy_FaShe = GameObject.Find("Canvas").transform.Find("YaoGan_ControllFaShe/Joy").GetComponent<RectTransform>();
        this.camera_ = Camera.main;
        
        if (isLocalPlayer == true)
            this.networkTransform = this.GetComponent<NetworkTransform>();
        Network.sendRate = 60f;

        this.joy.transform.parent.gameObject.SetActive(true);
        this.joy_FaShe.transform.parent.gameObject.SetActive(true);

        po = this.transform.position;  //  坦克的位置。

    }
	
	// Update is called once per frame
	void Update () {

        if (this.isLocalPlayer == false) return;  //  如果不是本地的玩家，那么将不操作。

       // float v = Input.GetAxis("Vertical");
        //Debug.Log(h);
        //  给坦克上的刚体组件加上一个水平的力。
      //  this.rigi.velocity = this.transform.forward * v * this.move_Speed;

       // float h = Input.GetAxis("Horizontal");

        //  给坦克上的刚体组件加上一个旋转的力。
      //  this.rigi.angularVelocity = this.transform.up * h * this.angualr_Speed;
        if (this.joy.anchoredPosition.magnitude >= 5f)
        {
           // Debug.Log("x:" + this.joy.anchoredPosition.x);
           // Debug.Log("y:" + this.joy.anchoredPosition.y);
            Vector2 xy = new Vector2(this.joy.anchoredPosition.x, this.joy.anchoredPosition.y).normalized * 8f;


            //Vector3 posi = new Vector3(this.transform.position.x + (this.joy.anchoredPosition.x / 10), 0, this.transform.position.z + (this.joy.anchoredPosition.y / 10));
            Vector3 posi = new Vector3(this.transform.position.x + xy.x, 0, this.transform.position.z + xy.y);
            this.transform.position = Vector3.Lerp(this.transform.position, posi, this.move_Speed * Time.deltaTime);
            //this.po = posi;
            //CmdPosi();
            this.transform.LookAt(posi);
            this.isUpdateStaticMove = true;  //  告诉坦克在静止的时候现在可以向其他客户端发送坦克的信息。
            this.cutTime = 2f;  //  这个是坦克静止的时候发送的时间，到达两秒之后就不用了，因为延迟不会那么长。

            CmdTank(posi, this.transform.position);

            //if (Vector3.Distance(this.transform.position, posi) >= 0.1f)
            //{
            //    Quaternion rotate = Quaternion.LookRotation(posi - this.transform.forward);
            //    Debug.Log(rotate);
            //    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotate, 360 * Time.deltaTime);
            //}
            if (this.audio_.clip != this.engineDriving)
            {  //  如果当前在播放的不是开动的声音。

                //this.audio_.Stop();  //  停止之前播放的音乐。
                this.audio_.clip = this.engineDriving;
                this.audio_.Play();

            }

        }
        else
        {
            if (this.isUpdateStaticMove) {  //  说明可以发送坦克的位置信息。

                if (this.cutTime >= 0f)
                {  //  开始倒计时。

                    CmdTank1(this.transform.position);
                    this.cutTime -= Time.deltaTime;
                  //  Debug.Log("123548asdasd");

                }
                else {  //  说明时间到了

                    this.cutTime = 2f;
                    this.isUpdateStaticMove = false;  //  然后设为不可以发送。

                }

            }
            
            //Debug.Log("ceshi1");
            //  坦克没动
            if (this.audio_.clip != this.engineIdle)
            {
                //  如果当前播放的不是坦克静止的音乐。
                //Debug.Log("ceshi");
                //this.audio_.Stop();
                this.audio_.clip = this.engineIdle;
                this.audio_.Play();


            }

        }

        //Debug.Log(this.paoTong.transform.position.y);

        if (this.joy_FaShe.anchoredPosition.magnitude >= 10f)
        {
            //Debug.Log(this.joy_FaShe.anchoredPosition.magnitude);

            Vector3 posi = new Vector3(this.paoTong.transform.position.x + (this.joy_FaShe.anchoredPosition.x), 1f * this.transform.localScale.x, this.paoTong.transform.position.z + (this.joy_FaShe.anchoredPosition.y));
            this.paoTong.transform.LookAt(posi);
            CmdPaoTong(posi);

            // Debug.Log("lll");


        }
        Camera_Follow();
    }


    //private void FixedUpdate()
    //{

    //    if (this.isLocalPlayer == false) return;  //  实时发送自己的位置。

    //    CmdTank(this.transform.position);



    //}

    public void Camera_Follow() {  //  这个方法是让摄像机跟随坦克移动的。

        this.camera_.transform.position = this.transform.position + Vector3.up * this.camera_Distance_Y + new Vector3(0f, 0f, this.camera_Distance_Z) ;
        this.camera_.transform.LookAt(this.transform);

    }

    [Command]
    public void CmdTank(Vector3 posi, Vector3 posi2) {
        //Debug.Log(posi);
        //Debug.Log(posi2);
        //Debug.Log("距离" + Vector3.Distance(this.transform.position, posi2));
        this.transform.position = Vector3.Lerp(this.transform.position, posi, this.move_Speed * Time.deltaTime);

        this.transform.LookAt(posi);
        RpcTankPosition(posi, this.transform.position);

    }
    [ClientRpc]
    public void RpcTankPosition(Vector3 posi, Vector3 posi2) {

        if (this.isLocalPlayer == true || this.isServer == true) return;  //  因为本地的用户在上面已经同步了。

        //Debug.Log("距离" + Vector3.Distance(this.transform.position, posi2));
        float f = Vector3.Distance(this.transform.position, posi2);

        if (f < 1.5f)
        {

            this.transform.position = Vector3.Lerp(this.transform.position, posi, this.move_Speed * Time.deltaTime);

        }
        //else if (f < 3.0f)
        //{

        //    this.transform.position = Vector3.Lerp(this.transform.position, posi, this.move_Speed * Time.deltaTime * 2f);

        //}
        //else if (f < 7.0f)
        //{

        //    this.transform.position = Vector3.Lerp(this.transform.position, posi, this.move_Speed * Time.deltaTime * 2.5f);

        //}
        else {

            this.transform.position = Vector3.Lerp(this.transform.position, posi, this.move_Speed * Time.deltaTime * 1.1f);

        }

        //this.transform.position = Vector3.Lerp(this.transform.position, posi, this.move_Speed * Time.deltaTime);

        this.transform.LookAt(posi);

    }

    [Command]
    public void CmdTank1(Vector3 posi)
    {
        //if (Vector3.Distance(posi, this.transform.position) < 1.0f) {

        //    this.transform.position = posi;
        //    RpcTankPosition1(posi);
        //    return;

        //}
        //Vector3 dis = posi - this.transform.position;
        
        //this.transform.Translate(dis.normalized * Time.deltaTime);
        this.transform.position = Vector3.Lerp(this.transform.position, posi, this.move_Speed * Time.deltaTime * 2f);
        //this.transform.position = posi;
        RpcTankPosition1(posi);

    }
    [ClientRpc]
    public void RpcTankPosition1(Vector3 posi)
    {

        if (this.isLocalPlayer == true || this.isServer == true) return;  //  因为本地的用户在上面已经同步了。
        //if (Vector3.Distance(posi, this.transform.position) < 1.0f)
        //{

        //    this.transform.position = posi;
        //    RpcTankPosition1(posi);
        //    return;

        //}
        //Vector3 dis = posi - this.transform.position;

        //this.transform.Translate(dis.normalized * Time.deltaTime);
        this.transform.position = Vector3.Lerp(this.transform.position, posi, this.move_Speed * Time.deltaTime * 2f);
        //this.transform.position = posi;

    }
    [Command]
    public void CmdPaoTong(Vector3 posi) {

        RpcPaoTongPosition(posi);  //  发送炮筒的位置。
        this.paoTong.transform.LookAt(posi);
    }

    [ClientRpc]
    public void RpcPaoTongPosition(Vector3 posi) {
       // Debug.Log("fasong");
        if (this.isLocalPlayer == true) return;
        //Debug.Log("dasoing");
        this.paoTong.transform.LookAt(posi);

    }

    [Command]
    public void CmdPosi() {

        this.po = this.transform.position;
        NetworkTransform t;
        

    }

    public void OnChangePosi(Vector3 posi) {

        if (this.isLocalPlayer == true) return;
        //Debug.Log("123123123123");
        Vector3 distance = posi - this.transform.position;
        this.transform.position = Vector3.Lerp(this.transform.position, posi, this.move_Speed * Time.fixedDeltaTime);
        this.transform.LookAt(posi);

    }

}
