using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  这个是更新排名ui的类。

public class Update_Ranking : MonoBehaviour {

    public TankInfomation tank_Self;  //  本地的坦克。
    public GameObject[] tanks;  //  当前的所有的坦克
    public TankInfomation[] tankInformations;  //  当前所有坦克身上的组件。

    //  各个排名组件上的子物体的text组件。
    public Text[] grades;
    
    //  ui上属于自己的排名的分数的组件。
    public Text self_Ranking;
    public Text self_Information;

	// Use this for initialization
	void Start () {
		
	}

    public void Update_UI() {

        this.tanks = GameObject.FindGameObjectsWithTag("Tank");

        int count_ = 0;

        this.tankInformations = new TankInfomation[this.tanks.Length];

        //  挨个获取每个坦克身上的tankInformation组件。
        foreach (GameObject obj in tanks) {

            this.tankInformations[count_++] = obj.GetComponent<TankInfomation>();

            //if (this.tankInformations[count_++].isLocalPlayer == true) {
            //    //  如果该坦克是本地的坦克。
            //    this.tank_Self = this.tankInformations[count_ - 1];

            //}

        }

        

        //  冒泡排序进行排序。
        int length = this.tankInformations.Length;
        for (int i = 0; i < length - 1; i++) {

            for (int j = 0; j < length - i - 1; j++) {

                if (this.tankInformations[j].grade < this.tankInformations[j + 1].grade) {

                    TankInfomation temp = this.tankInformations[j];
                    this.tankInformations[j] = this.tankInformations[j + 1];
                    this.tankInformations[j + 1] = temp;

                }

            }

        }

        int k = 0;
        for (k = 0; k < length; k++) {

            //  如果找到了本地的排名，那么跳出，下标+1即是排名。
            if (this.tank_Self == this.tankInformations[k])
                break;

        }

        //  先把本地的坦克的得分信息显示上去。
        this.self_Ranking.transform.parent.gameObject.SetActive(true);
        this.self_Ranking.text = (k + 1) + ".";  //  显示排名。
        //  显示分数。
        if(this.tank_Self != null)
            this.self_Information.text = this.tank_Self.tankName + "   " + this.tank_Self.grade + "";

        //  下面开始更新排名ui。

        if (length < 5)
        {  //  如果当前的在线人数不够五个。

            for (int i = 0; i < length; i++)
            {

                this.grades[i].transform.parent.gameObject.SetActive(true);
                this.grades[i].text = this.tankInformations[i].tankName + "   " + this.tankInformations[i].grade + "";

            }
            for (int i = length; i < 5; i++)
            {  //  将剩下的没有用到的ui隐藏。

                this.grades[i].transform.parent.gameObject.SetActive(false);

            }

            //switch (length) {

            //    case 1:  //  如果是一个人在线。
            //        //  将排名组件激活。
            //        this.first.transform.parent.gameObject.SetActive(true);
            //        this.first.text = this.tankInformations[0].tankName + this.tankInformations[0].grade + "";
            //        break;
            //    case 2:  //  如果是两个人在线。
            //        //  将排名组件激活。
            //        this.first.transform.parent.gameObject.SetActive(true);
            //        this.first.text = this.tankInformations[0].tankName + this.tankInformations[0].grade + "";
            //        this.second.transform.parent.gameObject.SetActive(true);
            //        this.second.text = this.tankInformations[1].tankName + this.tankInformations[1].grade + "";
            //        break;
            //    case 3:  //  如果是三个人在线。
            //        //  将排名组件激活。
            //        this.first.transform.parent.gameObject.SetActive(true);
            //        this.first.text = this.tankInformations[0].tankName + this.tankInformations[0].grade + "";
            //        this.second.transform.parent.gameObject.SetActive(true);
            //        this.second.text = this.tankInformations[1].tankName + this.tankInformations[1].grade + "";
            //        this.third.transform.parent.gameObject.SetActive(true);
            //        this.third.text = this.tankInformations[2].tankName + this.tankInformations[2].grade + "";
            //        break;
            //    case 4:  //  如果是四个人在线。
            //        //  将排名组件激活。
            //        this.first.transform.parent.gameObject.SetActive(true);
            //        this.first.text = this.tankInformations[0].tankName + this.tankInformations[0].grade + "";
            //        this.second.transform.parent.gameObject.SetActive(true);
            //        this.second.text = this.tankInformations[1].tankName + this.tankInformations[1].grade + "";
            //        this.third.transform.parent.gameObject.SetActive(true);
            //        this.third.text = this.tankInformations[2].tankName + this.tankInformations[2].grade + "";
            //        this.tra
            //        break;
            //    case 1:  //  如果是一个人在线。
            //        //  将排名组件激活。
            //        this.first.transform.parent.gameObject.SetActive(true);
            //        this.first.text = this.tankInformations[0].tankName + this.tankInformations[0].grade + "";
            //        break;


            //}

        }
        else {  //  如果当前的人数大于5个人。

            for (int i = 0; i < 5; i++) {

                this.grades[i].transform.parent.gameObject.SetActive(true);
                this.grades[i].text = this.tankInformations[i].tankName + "   " + this.tankInformations[i].grade + "";

            }

        }


    }
}
