using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  这个类是用来控制飞机的飞行的。

public class Plane_Move : MonoBehaviour {

    private bool flag = true;
    public Vector3 target;
    public float distance;  //  这个是用来定位飞机上的东西下落的位置的。

	// Use this for initialization
	void Start () {

        this.target = this.transform.position + new Vector3(-210f, 0f, 0f);  //  将坦克的目的地赋值。
        //distance = Random.Range(30f, 70f);

	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(this.transform.parent.name);
        //this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(-50f, 15.75f, 3.16f), 0.1f * Time.deltaTime);
        this.transform.Translate(this.transform.right * 8f * Time.deltaTime);

        //  这个是用来判断坦克是否到达了下落物体的位置。
        if (Vector3.Distance(this.transform.position, new Vector3(0f, this.transform.position.y, this.transform.position.z)) <= 1f && this.flag)
        {
            //  掉落飞机上的物品。
            Transform obj = this.transform.Find("Good");
            obj.parent = null;
            obj.GetComponent<Rigidbody>().useGravity = true;
            this.flag = false;
            StartCoroutine(DestoryCamera());  //  在扔掉物品后的规定时间后消灭相机。

        }


        if (Vector3.Distance(this.transform.position, this.target) <= 1f) {

            Destroy(this.gameObject);

        }

	}

    IEnumerator DestoryCamera() {

        yield return new WaitForSeconds(1.5f);
        Destroy(this.transform.Find("Camera").gameObject);

    }

}
