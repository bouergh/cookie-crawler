using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    // Use this for initialization
    void Start()
    {
        Debug.Log("Player create");
    }

    [Command]
    void CmdSendSpaceDown() {
        Debug.Log("SPACE DOWN RPC from : " + this.netId);
        CookieController.singleton.storeInput(int.Parse(this.netId.ToString()));
    }

    [Command]
    void CmdSendSpaceUp() {
        Debug.Log("SPACE UP RP from : " + this.netId);
        CookieController.singleton.removeActionIfExist(int.Parse(this.netId.ToString()));
    }



	// Update is called once per frame
    void Update () {
        if (!isLocalPlayer) {
            return;
        }
        checkInput();
	}

    // Get if space is down
    void checkInput() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdSendSpaceDown();
        }

        if (Input.GetKeyUp(KeyCode.Space)) {
            CmdSendSpaceUp();
        }

    }
}
