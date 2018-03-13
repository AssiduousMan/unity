using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using UnityEngine.UI;

//  这个是公共的类，用来显示一些共有的属性，比如坦克的数量。
//  包括用户登录成功后会判断当前用户是admin还是普通用户。

public class PublicProperty : MonoBehaviour {

    public static int Tank_Count = 0;  //  坦克的数量。
    public static bool isAdmin = false;  //  这个是用来判断是不是管理员身份的，当场景切换的时候，如果是管理员登录，会将其置为true。 
    public NetworkManager ma;
    public Image image;
    public GameObject startPanel;  //  这个是游戏的开始游戏的界面。

    private void Start()
    {

        if (isAdmin == false)
        {

            //  如果不是管理员用户，那么激活客户的界面。
            this.startPanel.SetActive(true);
            //  并且把UNET界面关掉。
            GameObject.FindGameObjectWithTag("UnetManage").GetComponent<NetworkManagerHUD>().showGUI = false;

        }
        else {  //  表示管理员用户

            Debug.Log("admin");
            //  把主相机的AudioListener激活。
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioListener>().enabled = true;

        }
       // Debug.Log(isAdmin);

    }

    //private void Update()
    //{

    //    //if()
    //    //if()
    //    //this.ma.OnClientConnect();
    //    //Debug.Log("链接数量：" + this.ma.numPlayers);
    //    //if (Input.GetKeyDown(KeyCode.Space)) {

    //    //    this.ma.StopServer();

    //    //}

    //    if (Input.GetKeyDown(KeyCode.F1)) {
    //        //   截屏。

    //        StartCoroutine(GetCapture());

    //    }

    //}

    IEnumerator GetCapture()  //  截屏
    {
        yield return new WaitForEndOfFrame();
        int width = Screen.width;
        int height = Screen.height;
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0, true);
        byte[] imagebytes = tex.EncodeToPNG();//转化为png图
        tex.Compress(false);//对屏幕缓存进行压缩
        //this.image.mainTexture = tex;//对屏幕缓存进行显示（缩略图）
        Sprite tempSp = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0));//将图转化为ui的贴图  
        //m_tSprite.GetComponent<Image>().sprite = tempSp;//贴在ui上  
        this.image.sprite = tempSp;
        File.WriteAllBytes(Application.dataPath + "/screencapture.png", imagebytes);//存储png图
        
    }

}
