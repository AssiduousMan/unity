using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Data;
using UnityEngine.SceneManagement;

public class Button_Click : MonoBehaviour {

    public GameObject unetManage;
    

    public void OnClick_ExitButton() {  //  点击了退出游戏的按钮。


        Application.Quit();

    }

    public void OnClick_AgainButton() {
      //  Debug.Log("Again");
        NetworkManager networkManage= GameObject.FindWithTag("UnetManage").GetComponent<NetworkManager>();
        networkManage.GetComponent<NetworkManager>().StopClient();
        networkManage.GetComponent<NetworkManager>().StartClient();

        //  将这个面板隐藏
        GameObject.FindGameObjectWithTag("Canvas").transform.Find("GameOverPanel").gameObject.SetActive(false);
        
    }

    public void OnClick_RegisterButton() {  //  点击了注册的按钮。

        //  获取一下画布中Image组件，因为下面要用的组件都是其子组件。
        GameObject image = GameObject.FindGameObjectWithTag("Canvas").transform.Find("Image").gameObject;
        image.transform.Find("Login").gameObject.SetActive(false);  //  把登录界面隐藏。
        image.transform.Find("Register").gameObject.SetActive(true) ;  //  把注册账号的界面激活。   

    }

    public void OnClick_ConfirmRegisterButton() {  //  点击了确定注册的按钮。

        //  获取一下画布中Image组件，因为下面要用的组件都是其子组件。
        Transform register = GameObject.FindGameObjectWithTag("Canvas").transform.Find("Image/Register");
        string user = register.Find("Account/InputField").GetComponent<InputField>().text;
        string passwd1 = register.Find("Password/InputField").GetComponent<InputField>().text;
        string passwd2 = register.Find("Password2/InputField").GetComponent<InputField>().text;
        //this.ss = "asd";

        if (user == "" || passwd1 == "" || passwd2 == "") {

            register.Find("TextIsNull").gameObject.SetActive(true);  //  将提示面板激活。
            return ;

        }

        if (passwd1.Equals(passwd2))
        {

            bool isExist = false;  //  用来判断是否用户名重复的标志。

            //  连接数据库。
            //this.ss = "jkl";
            SqlAccess sql = new SqlAccess(); //this.ss = "qwe";
            
            DataSet ds = sql.SelectToAccount("select user from account");

            if (ds != null)
            {

                DataTable table = ds.Tables[0];

                foreach (DataRow row in table.Rows)
                {

                    foreach (DataColumn col in table.Columns)
                    {

                        if (user.Equals(row[col]))
                        {

                            isExist = true;  //  说明用户名重复。
                            //Debug.Log("用户名已经存在。");
                            break;

                        }

                    }

                }

            }
            //this.ss = "yui";
            if (isExist)
            {  //  如果用户名存在。

                register.Find("UserExist").gameObject.SetActive(true);  //  将用户名重复面板激活。
                register.Find("Account/InputField").GetComponent<InputField>().text = "";  //  并且将用户名置空。
                return;

            }
            //this.ss = "zxc";
            //  将注册的账号加入数据库中。
            sql.InsertToAccount("insert into account values('" + user + "', '" + passwd1 + "')");
            //this.ss = "fgh";
            //  获取一下画布中Image组件，因为下面要用的组件都是其子组件。
            GameObject image = GameObject.FindGameObjectWithTag("Canvas").transform.Find("Image").gameObject;
            image.transform.Find("Login").gameObject.SetActive(true);  //  把登录界面隐藏。
            image.transform.Find("Register").gameObject.SetActive(false);  //  把注册账号的界面激活。  


        }
        else {  //  说明两次输入的密码不一样。

            register.Find("PasswordDontSame").gameObject.SetActive(true);  //  将用户名重复面板激活。
            register.Find("Password/InputField").GetComponent<InputField>().text = "";  //  将密码置空。
            register.Find("Password2/InputField").GetComponent<InputField>().text = "";

        }
                            
    }

    public void OnClick_LoginButton() {
               
        // 获取一下画布中Image组件，因为下面要用的组件都是其子组件。
        Transform login = GameObject.FindGameObjectWithTag("Canvas").transform.Find("Image/Login");
        string user = login.Find("Account/InputField").GetComponent<InputField>().text;
        string passwd = login.Find("Password/InputField").GetComponent<InputField>().text;

        bool isUserExist = false;  //  判断用户是否存在的标志。
        bool isPasswdConfirm = false;  //  判断密码是否正确的标志。

        SqlAccess sql = new SqlAccess(); //this.ss = "qwe";
        DataSet ds = sql.SelectToAccount("select user from account");

        if (ds != null)
        {

            DataTable table = ds.Tables[0];

            foreach (DataRow row in table.Rows)
            {

                foreach (DataColumn col in table.Columns)
                {

                    if (user.Equals(row[col]))  //  
                    {

                        isUserExist = true;  //  说明用户名存在。
                        //if(passwd.Equals(row[col + 1]))

                        break;

                    }

                }

            }

            if (isUserExist)
            {
                //  如果用户存在，那么进行密码判断。
                ds = sql.SelectToAccount("select passwd from account where user = '" + user + "'");

                if (ds != null)
                {

                    DataTable table2 = ds.Tables[0];

                    foreach (DataRow row in table2.Rows)
                    {

                        foreach (DataColumn col in table2.Columns)
                        {

                            if (passwd.Equals(row[col]))  //  
                            {

                                isPasswdConfirm = true;  //  说明密码也正确。
                                                         //if(passwd.Equals(row[col + 1]))

                                break;

                            }

                        }

                    }

                }



            }
            else
            {
                //  如果用户不存在。
                //Debug.Log("用户不存在");
                login.Find("UserDontExist").gameObject.SetActive(true);  //  将用户名不存在的提示面板激活。
                login.Find("Account/InputField").GetComponent<InputField>().text = "";  //  同时将用户输入置空。
                return;

            }

            if (isPasswdConfirm == false)  //  说明密码不正确。
            {

                login.Find("PasswordDontConfirm").gameObject.SetActive(true);  //  将用户名不存在的提示面板激活。
                login.Find("Password/InputField").GetComponent<InputField>().text = "";  //  同时将用户输入置空。
                //Debug.Log("密码不正确");
                return;

            }

            if (isUserExist && isPasswdConfirm) {  //  说明账号密码都对。

                //  获取一下画布中Image组件，因为下面要用的组件都是其子组件。
                GameObject image = GameObject.FindGameObjectWithTag("Canvas").transform.Find("Image").gameObject;
                image.transform.Find("Login").gameObject.SetActive(false);  //  把登录界面隐藏。
                //image.transform.Find("Register").gameObject.SetActive(false);  //  把注册账号的界面激活。

                if (user.Equals("admin")) {

                    PublicProperty.isAdmin = true;  //  告诉下一个场景，当前客户端为管理员。

                }
                
                GameObject.Find("LoadScence").GetComponent<LoadScence>().Load();

            }

        }  
        

    }

    public void OnClick_UserExistButton() {  //  点击了用户重复的确定按钮。

        //  将用户重复的提示面板隐藏。
        GameObject.FindGameObjectWithTag("Canvas").transform.Find("Image/Register/UserExist").gameObject.SetActive(false);

    }

    public void OnClick_PasswordDontSameButton()  //  点击了密码不一致的确定按钮。
    {  //  点击了用户重复的确定按钮。

        //  将用户重复的提示面板隐藏。
        GameObject.FindGameObjectWithTag("Canvas").transform.Find("Image/Register/PasswordDontSame").gameObject.SetActive(false);

    }

    public void OnClick_UserDontExistButton() {  //  点击了用户不存在的提示面板的确定按钮。

        GameObject.FindGameObjectWithTag("Canvas").transform.Find("Image/Login/UserDontExist").gameObject.SetActive(false);

    }

    public void OnClick_PasswordDontConfirmButton()
    {  //  点击了用户不存在的提示面板的确定按钮。

        GameObject.FindGameObjectWithTag("Canvas").transform.Find("Image/Login/PasswordDontConfirm").gameObject.SetActive(false);

    }

    public void OnClick_TextIsNull() {
        //  点击了提示文本框不能为空的按钮。

        GameObject.FindGameObjectWithTag("Canvas").transform.Find("Image/Register/TextIsNull").gameObject.SetActive(false);

    }

    public void OnClick_StartGameButton() {
        //  点击了主界面的开始游戏的按钮。
        //  根据tag来寻找UnetMange游戏物体然后在获得其身上的组件。
        this.unetManage.GetComponent<NetworkManager>().StartClient();
        GameObject.FindGameObjectWithTag("Canvas").transform.Find("StartPanel").gameObject.SetActive(false);  //  然后再把开始面板关闭。


    }

    //IEnumerator LoadScene() {

    //    AsyncOperation async;
    //    async = SceneManager.LoadSceneAsync("sceneName");
    //    async.allowSceneActivation = false;
    //    yield return async;


    //}
    //Slider processBar;
    //int nowProcess;
    //private void OnGUI()
    //{

    //    //GUILayout.Label(this.ss);

    //}

}
