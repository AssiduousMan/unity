using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  这个类是来控制UI的，更新他的UI显示。

public class UIUpdate : MonoBehaviour {

    public TankInfomation tankInformation;
    public Text level;  //  等级。
    public Text tankName;
    public Slider HP;
    public Text HP_Text;
    public Slider Exp;
    public Text exp_Text;
    public Text attack_Text;
    public Text defense_Text;
    public Text range_Text;

    //  这个类是用来更新坦克信息ui的。

    public void UpdateUI() {

        //  更新ui。
        this.level.text = "Lv:" + (int)this.tankInformation.level + "";
        this.tankName.text = this.tankInformation.tankName;

        //Debug.Log(this.tankInformation.current_Health / this.tankInformation.total_Health);
        this.HP.value = this.tankInformation.current_Health / this.tankInformation.total_Health;
        this.HP_Text.text = this.tankInformation.current_Health + "/" + this.tankInformation.total_Health;
        this.Exp.value = this.tankInformation.current_Exp / this.tankInformation.total_Exp;
        this.exp_Text.text = this.tankInformation.current_Exp + "/" + this.tankInformation.total_Exp;
        this.attack_Text.text = this.tankInformation.attack + "";
        this.defense_Text.text = this.tankInformation.defense + "";
        this.range_Text.text = (int)this.tankInformation.range + "";

    }
}
