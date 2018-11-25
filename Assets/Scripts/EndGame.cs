using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EndGame : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D other)
    {
        NetworkManager.singleton.ServerChangeScene("EndScene");
    }
}
