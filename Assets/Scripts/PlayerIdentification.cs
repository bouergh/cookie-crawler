using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerIdentification : NetworkBehaviour {

	[SyncVar]
	public int playerNumber = -1;
	public Color col = Color.grey;
	string color = "NONE";

	public Sprite[] sprites;

	// Use this for initialization
	void Start () {
		if(!isLocalPlayer){
			return;
		}
		Identify();
	}

	

	
	
	public int PlayerNumber(){
		//return (int)netId.Value;
		// //int num = LobbyManager.singleton.numPlayers;
		int num = GameObject.FindGameObjectsWithTag("Player").Length;
		CmdSyncPlayerNumber(num);
		Debug.Log("number of players on the server is "+num);
		return num;
	}
	[Command]
	public void CmdSyncPlayerNumber(int pn){
		playerNumber = pn;
		RpcSyncPlayerNumber(pn);
		if(pn == 4) LobbyManager.singleton.StartGame();
	}
	[ClientRpc]
	public void RpcSyncPlayerNumber(int pn){
		playerNumber = pn;
	}


	public void Identify(){
		//Debug.Log(netId);
		playerNumber = PlayerNumber();
		col =  PickColor(playerNumber);
		foreach(Text txt in Object.FindObjectsOfType<Text>()){
			if(txt.name == "PlayerText"){
				txt.color = col;
				txt.text = "Welcome to the Game !\n You are player "+playerNumber+",\n your color is "+color+".";
			}
		}
		//CmdAllChangeColor();//etape 1
	}

	

	[ClientRpc]
	public void RpcAssignValues(int num, GameObject go, Color colr){
		LobbyManager.singleton.inLobby = false;
		Debug.Log("player number is "+num);
		playerNumber = num;
		col =  PickColor(playerNumber);
		go.GetComponent<SpriteRenderer>().color = colr;
		go.GetComponent<SpriteRenderer>().sprite = sprites[num-1];
	}

	// //etape 2
	// [Command]
	// public void CmdAllChangeColor(){
	// 	foreach (PlayerIdentification pid in FindObjectsOfType<PlayerIdentification>())
	// 		{
	// 			pid.RpcAllChangeColor();
	// 		}
	// }

	// //etape 3
	// [ClientRpc]
	// public void RpcAllChangeColor(){
	// 	CmdChangeColor(gameObject, col, playerNumber);
	// }

	// //etape 4
	// [Command]
	// public void CmdChangeColor(GameObject go, Color colr, int pn){
	// 	go.GetComponent<SpriteRenderer>().color = colr;
	// 	Debug.Log("player number is "+pn);
	// 	go.GetComponent<SpriteRenderer>().sprite = sprites[pn-1];
	// 	RpcChangeColor(go, colr, pn);
	// }

	// //etape 5 //sans doute trop lourd on pourrait faire 2 ou 3 fonctions en une sur le serv au lieu de 2*Cmd/Rpc
	// [ClientRpc]
	// public void RpcChangeColor(GameObject go, Color colr, int pn){
	// 	go.GetComponent<SpriteRenderer>().color = colr;
	// 	go.GetComponent<SpriteRenderer>().sprite = sprites[pn-1];
	// }

	// [Command]
	// public void CmdPickPlayerNumber(GameObject player){
	// 	LobbyManager.singleton.SetPlayerNumber(player);
	// 	Debug.Log("player "+netId+" number is "+playerNumber);
	// }

	public Color PickColor(int playerNumber){
		int roundNum = ((playerNumber-1) % 4)+1;
		switch(roundNum){
			case 1:
			  color = "red";
              return Color.red;
            case 2:
			  color = "blue";
              return Color.blue;
			case 3:
			  color = "green";
              return Color.green;
            case 4:
			  color = "yellow";
              return Color.yellow;
          	default:
              Debug.Log("no good color pickable !!!!");
			  return Color.grey;
		}
	}
}
