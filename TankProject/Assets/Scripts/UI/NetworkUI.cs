using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkUI : NetworkBehaviour {

    public NetworkManager networkManager;

    public void OnClick_HostButton() {

        this.networkManager.StartHost();

    }

    public void OnClick_ServerButton() {

        this.networkManager.StartServer();

    }

    public void OnClick_LinkButton() {

        this.networkManager.StartClient();

    }

}
