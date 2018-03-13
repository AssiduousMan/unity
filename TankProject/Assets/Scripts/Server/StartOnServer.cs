using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//  这个是跑在服务器的类。

public class StartOnServer : NetworkBehaviour {

    public GameObject Product_Goods;
    public NetworkManager manage;
    public int tankNumber = 0;
    public static bool isUpdateColor = false;

    private void Update()
    {

        if (this.isServer == false)
            return;
        //Debug.Log("当前在线的坦克的数量为：" + this.manage.numPlayers);
        if (this.tankNumber != this.manage.numPlayers)
        {
            //  说明有人连接上或有人退出了。
            if (this.tankNumber < this.manage.numPlayers)
            {
                //  说明有人连接上了。
                //  然后同步一下系统坦克的血量。
              //  Debug.Log("加入了一辆坦克");
                EnemyTankHealth enemyTankHealth = GameObject.FindGameObjectWithTag("EnemyTank").GetComponent<EnemyTankHealth>();
                enemyTankHealth.RpcOnChangeHealth(enemyTankHealth.currentHealth / enemyTankHealth.healthTotal);
                this.tankNumber++;

                //  接下来让让新加入的坦克同步一下当前坦克的信息。比如血量
                GameObject[] objs = GameObject.FindGameObjectsWithTag("Tank");  //  在服务器端获得当前所有的坦克
                for (int i = 0; i < objs.Length - 1; i++) {  //  最后一辆坦克就不用讲他的信息发送到各个客户端了，因为他一进来的时候已经同步了。

                  //  Debug.Log("同步坦克信息");
                    //  然后在单个获取其身上的生命组件。然后在发送给各个客户端。
                    Tank_Health tank_Health = objs[i].GetComponent<Tank_Health>();  

                    tank_Health.RpcSyncHealth(tank_Health.currentHealth / tank_Health.healthTotal, tank_Health.transform.localScale);
                 //   Debug.Log("坦克信息:" + tank_Health.currentHealth / tank_Health.healthTotal);

                }

                



            }
            else
            {
                //  说明有人退出了。

                this.tankNumber--;

            }


        }

        if (isUpdateColor) {  //  说明有新的坦克加入，并且已经在服务器端同步了该颜色，那么现在应该同步到各个客户端了。
          //  Debug.Log("更新坦克的颜色。");
            GameObject[] objs = GameObject.FindGameObjectsWithTag("Tank");  //  在服务器端获得当前所有的坦克

            for (int i = 0; i < objs.Length; i++)
            {  //  同步坦克的颜色，因为是随机的颜色，所以没加入一个坦克，都要同步一次。

                TankInfomation trans = objs[i].GetComponent<TankInfomation>();
                trans.RpcTankColor(trans.c.r, trans.c.g, trans.c.b);

            }

            isUpdateColor = false;  //  同步完颜色之后将标志设为false。

        }

    }

    //[ClientRpc]
    //public void Update_TankInformation() {



    //}
    //public override void OnStartServer()
    //{

    //    Debug.Log(111);
    //    Instantiate(this.Product_Goods, this.transform.position, this.transform.rotation);

    //}


}
