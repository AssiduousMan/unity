using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//  这个类是用来同步系统坦克炮筒的位置的。

public class EnemyTankTransforms : NetworkBehaviour {

    public Transform firPositon;  //  炮筒的位置。

    private void Start()
    {
        
    }

    //[]
    public void SetFirBulletPosition(Vector3 forwd) {

        RpcSetFirBulletPosition(forwd);

    }

    [ClientRpc]
    public void RpcSetFirBulletPosition(Vector3 forwd) {

        this.firPositon.forward = forwd;
       // Debug.Log("asdasdasdsa");

    }



}
