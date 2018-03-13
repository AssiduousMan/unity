using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text3D_lLookAtcamera : MonoBehaviour {

    public Camera cam;
    private Transform posi;

	// Use this for initialization
	void Start () {
        this.cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {

        //posi = new Transform();
        //  这个是为了防止摄像机也跟随移动。
        //float x = this.cam.transform.position.x;
        //float y = this.cam.transform.position.y;
        //float z = this.cam.transform.position.z;

        //Vector3 p = new Vector3(-x, -y, -z);
        ////posi.rotation = this.cam.transform.rotation;
        ////posi.position = p;

        //this.transform.forward = this.cam.transform.forward;
        //this.transform.LookAt(this.cam.transform);
        Vector3 p = this.gameObject.transform.position - this.cam.transform.position;
        this.transform.forward = p;
        //this.transform.Rotate(Vector3.up * 90, 10 * Time.deltaTime);


	}
}
