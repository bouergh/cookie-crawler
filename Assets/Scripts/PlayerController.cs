using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    public Vector3 cookieOffsetPos;

    // Use this for initialization
    void Start()
    {
        cookieOffsetPos = CookieController.singleton.transform.position - transform.position;
        Debug.Log("Player create");
    }

    [Command]
    void CmdSendSpaceDown() {
        Debug.Log("SPACE DOWN RPC from : " + this.netId);
        //CookieController.singleton.spaceDownFor(int.Parse(this.netId.ToString()));
        CookieController.singleton.spaceDownFor(GetComponent<PlayerIdentification>().playerNumber-1);
    }

    [Command]
    void CmdSendSpaceUp() {
        Debug.Log("SPACE UP RP from : " + this.netId);
        CookieController.singleton.spaceUpFor(GetComponent<PlayerIdentification>().playerNumber-1);
    }



	// Update is called once per frame
    void Update () {
        if (!isLocalPlayer) {
            return;
        }
        checkInput();
        transform.position = CookieController.singleton.transform.position - cookieOffsetPos;
	}

    // Get if space is down
    void checkInput() {
#if UNITY_STANDALONE || UNITY_STANDALONE_OSX
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdSendSpaceDown();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            CmdSendSpaceUp();
        }

#elif UNITY_IOS || UNITY_ANDROID
        Touch touch = Input.GetTouch(0);
        if(touch.phase == TouchPhase.Began) {
            CmdSendSpaceDown();
        }

        if(touch.phase == TouchPhase.Ended) {
            CmdSendSpaceUp();
        }
#endif


    }
}
