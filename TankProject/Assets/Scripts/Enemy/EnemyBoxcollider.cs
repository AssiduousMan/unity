using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//  这个系统坦克的碰撞器检测的类，为了检测进入攻击范围的坦克以及坐标，然后方便攻击。

public class EnemyBoxcollider : NetworkBehaviour {

    public Dictionary<int, Transform> dic;
    public Transform firPosition;
    public Transform target;
    public float cutDownTime = 1f;  //  倒计时
    public EnemyTankFiringBullet firingBullet;
    public EnemyTankTransforms tankTransforms;
	private bool isTargetDead = true;  //  用来判断目标坦克是否死亡。
	private int targetTankNumber = -1;  //  目标坦克的编号。

    //List<Transform> list;


    public void Start()
    {

        this.dic = new Dictionary<int, Transform>();
        //list = new List<Transform>();
        //list.Add("qwe");
        //list.Add("asd");
        //list.Add("zxc");
        //Debug.Log(list.Count);
        //Debug.Log(list[0]);
        //list.RemoveAt(0);
        //Debug.Log(list[0]);
        //Debug.Log(list[list.Count - 1]);
        //this.dic[]


    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Tank") {

            //this.list.Add(other.transform);
			//Debug.Log("一辆坦克进入攻击范围。");
            this.dic.Add(other.gameObject.GetComponent<TankInfomation>().tank_Number, other.transform);
            setTarget();

        }

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Tank")
        {

            this.dic.Remove(other.gameObject.GetComponent<TankInfomation>().tank_Number);
            setTarget();
        }

    }

    private void setTarget() {

        if (dic.Count != 0)  //  如果这个字典有目标。
        {

            foreach (KeyValuePair<int, Transform> kvp in this.dic)
            {  //  遍历字典。

                this.target = kvp.Value;
				this.targetTankNumber = this.target.GetComponent<TankInfomation> ().tank_Number;
				//Debug.Log ("该坦克编号为" + this.targetTankNumber);
                break;

            }

        }
        else
        {  //   如果没有目标。

            this.target = null;

        }


    }

    private void Update()
    {

        if (this.target == null) 
		{
			//Debug.Log ("xiaomei2222");
			if(this.isTargetDead == false){
				//  这个说明上个坦克已经死亡了，那么需要更换目标。

				this.isTargetDead = true;
				this.dic.Remove(this.targetTankNumber);  //  然后从该字典中移除死去的坦克。
				setTarget ();  //  这个是为了当坦克死了之后，target就回变为null，如果此时在坦克的攻击范围内还有坦克，那么就可以继续攻击下个坦克。
				//Debug.Log("更换攻击目标。");

			}

			return;
		}
		this.isTargetDead = false;
		//this.targetTankNumber = 
        //if(this.)
        this.cutDownTime -= Time.deltaTime;
        if (this.cutDownTime <= 0f) {  //   当倒计时结束，要发射子弹。

			//int temp = this.target
            //this.firPosition.LookAt(this.target + Vector3.up);
            Vector3 forwd = this.target.position - this.firPosition.position;
            this.firPosition.forward = forwd + Vector3.up * 2;

            //Debug.Log(this.target.position);
            //Debug.Log(this.firPosition.position);

            //kkRpcEnemyFirPosition(/*this.target.position - this.firPosition.position*/); 
            this.tankTransforms.SetFirBulletPosition(forwd + Vector3.up * 2);
            this.firingBullet.Fire();
            this.cutDownTime = 1f;
			//Debug.Log ("fashe");
			if (this.target == null) {
			
				//Debug.Log ("xiaomei");
			
			}
            

        }
               

    }

    //[ClientRpc]
    //public void RpcEnemyFirPosition(/*Vector3 tran*/) {

    //    Debug.Log("asdasdasd");
    //    //this.firPosition.forward = tran.normalized;
    //    //this.firPosition.LookAt(trans);

    //}


}
