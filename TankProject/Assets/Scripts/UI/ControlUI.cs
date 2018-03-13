using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

//  这个类是来控制当服务器启动时来显示ui组件的

public class ControlUI : NetworkBehaviour {

    public GameObject TankInfo;
    public Slider HP;
    public Slider Exe;
    public GameObject zuoPanel;
    public GameObject youPanel;
    public GameObject ranking;  //  排名的面板。
    public GameObject inputField;  //  一开始输入名字的文本框。
    public string tankName;
    public TankInfomation self;

	// Use this for initialization
	void Start () {

	}

    public override void OnStartClient()
    {
        this.TankInfo.SetActive(true);
        //Slider s[] = this.TankInfo.transform.Find()
        this.HP.enabled = false;
        this.Exe.enabled = false;
        this.zuoPanel.SetActive(true);
        this.youPanel.SetActive(true);
        this.ranking.SetActive(true);
        this.tankName = this.inputField.GetComponent<InputField>().text;
        this.inputField.SetActive(false);

    }

    public void GengXinName() {

        this.self.tankName = this.tankName;

    }

}
