using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EndManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	void Update () {
#if UNITY_STANDALONE || UNITY_STANDALONE_OSX
        if (Input.GetKeyDown(KeyCode.Space)){ //hardcode this button FOREVER :o
            
			LobbyManager.singleton.Quit();
        }

#elif UNITY_IOS || UNITY_ANDROID
        Touch touch = Input.GetTouch(0);
        if ((touch.phase == TouchPhase.Began)){ 
			
			LobbyManager.singleton.Quit();
        }
#endif
	}	

	
}
