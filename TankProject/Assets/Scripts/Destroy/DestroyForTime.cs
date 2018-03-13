using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  这个是用来控制游戏物体的销毁的。

public class DestroyForTime : MonoBehaviour {

    public float time;  //  销毁的时间。

	// Use this for initialization
	void Start () {

        Destroy(this.gameObject, time);

	}
	
}
