using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerIdentification : NetworkBehaviour {

	[SyncVar]
	public int playerNumber = -1;
	[SyncVar]
	public Color col = Color.grey;
	string color = "NONE";

	// Use this for initialization
	void Start () {
		if(!isLocalPlayer){
			return;
		}
		
		
		Identify(netId);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public int PlayerNumber(NetworkInstanceId netId){
		return (int)netId.Value;
	}

	public void Identify(NetworkInstanceId netId){
		foreach(Text txt in Object.FindObjectsOfType<Text>()){
			if(txt.name == "PlayerText"){
				playerNumber = PlayerNumber(netId);
				col =  PickColor(playerNumber);
				txt.color = col;
				txt.text = "Welcome to the Game !\n You are player "+playerNumber+",\n your color is "+color+".";
				CmdAllChangeColor();//etape 1
			}
		}
	}


	//etape 2
	[Command]
	public void CmdAllChangeColor(){
		foreach (PlayerIdentification pid in FindObjectsOfType<PlayerIdentification>())
			{
				pid.RpcAllChangeColor();
			}
	}

	//etape 3
	[ClientRpc]
	public void RpcAllChangeColor(){
		CmdChangeColor(gameObject, col);
	}

	//etape 4
	[Command]
	public void CmdChangeColor(GameObject go, Color colr){
		//go.GetComponent<SpriteRenderer>().color = colr;
		RpcChangeColor(go, colr);
	}

	//etape 5 //sans doute trop lourd on pourrait faire 2 ou 3 fonctions en une sur le serv au lieu de 2*Cmd/Rpc
	[ClientRpc]
	public void RpcChangeColor(GameObject go, Color colr){
		go.GetComponent<SpriteRenderer>().color = colr;
	}

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
