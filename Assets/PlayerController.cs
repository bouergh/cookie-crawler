using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("Player create");
	}



	// Update is called once per frame
	void Update () {

        if (!isLocalPlayer) {
            return;
          }

        if (Input.GetKeyDown(KeyCode.Space)) {
            print("YOLO");
          }
	}
}
