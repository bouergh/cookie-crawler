using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.NetworkSystem;

public class LobbyManager : NetworkManager {

	public static new LobbyManager singleton;
	public bool inLobby = true;
	public string cookieMatchName = "coookie";
	public Transform[] spawnPositions;
	public static bool gameStarted = false;
	public float spaceCooldown = 3f;
	private float spaceTimer = 0f;

	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
		spawnPositions = new Transform[4];
		for(int i = 1; i<=4;i++){
			spawnPositions[i-1] = GameObject.Find("Spawn"+i).transform;
			Debug.Log(spawnPositions[i-1].name);
		}
		int np = GameObject.FindGameObjectsWithTag("Player").Length;
		Vector2 spawnPosition = spawnPositions[np].position;
        GameObject player = (GameObject)Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

	// Use this for initialization

	void Awake()
	{
		if(singleton == null){
			singleton = this;
		}else{
			Destroy(gameObject);
		}
	}
	void Start () {
		inLobby = true;
	}
	
	// Update is called once per frame
	void Update () {
#if UNITY_STANDALONE || UNITY_STANDALONE_OSX
        if (Input.GetKeyDown(KeyCode.Space) && inLobby && !IsClientConnected() && spaceTimer >= spaceCooldown){ //hardcode this button FOREVER :o
            spaceTimer = 0f;
			Debug.Log("play the dam game");
            StartMatchMaker();
            matchMaker.ListMatches(0, 10, "", true, 0, 0, OnMatchList);
        }

#elif UNITY_IOS || UNITY_ANDROID
        Touch touch = Input.GetTouch(0);
        if ((touch.phase == TouchPhase.Began) && inLobby && !IsClientConnected() && spaceTimer >= spaceCooldown){
			spaceTimer = 0f;
            Debug.Log("play the dam game");
            StartMatchMaker();
            matchMaker.ListMatches(0, 10, "", true, 0, 0, OnMatchList);
        }
#endif

		spaceTimer += Time.deltaTime;

		// if(numPlayers == matchSize){
		// 	inLobby = false;
		// 	StartGame();
		// }
	}



	public IEnumerator StartGame(){
		yield return new WaitForSeconds(2f);
		inLobby = false;
		Debug.Log("starting game !!!");
		foreach(PlayerIdentification pid in FindObjectsOfType<PlayerIdentification>()){
			int num = pid.playerNumber;//a tester
			GameObject go = pid.gameObject;
			Color col = PickColor(num);
			pid.playerNumber = num; 
			pid.RpcAssignValues(num, go, col);
		}
        CookieController.singleton.setPlayers();
	}

	public Color PickColor(int playerNumber){
		int roundNum = ((playerNumber-1) % 4)+1;
		switch(roundNum){
			case 1:
              return Color.red;
            case 2:
              return Color.blue;
			case 3:
              return Color.green;
            case 4:
              return Color.yellow;
          	default:
              Debug.Log("no good color pickable !!!!");
			  return Color.grey;
		}
	}

	public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
    {
        if(success){
			Debug.Log("match listed with success");
			foreach(MatchInfoSnapshot match in matches){
				if(match.name == cookieMatchName){
					Debug.Log("cookie match found");
					matchMaker.JoinMatch(match.networkId, "" , "", "", 0, 0, OnMatchJoined);
					Debug.Log("cookie match joined");
					return;
				}
			}
		}
		Debug.Log("no cookie match found");
		matchMaker.CreateMatch(cookieMatchName, 4, true, "", "", "", 0, 0, OnMatchCreate);
    }


	
	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        if (extraMessageReader != null)
        {
            var s = extraMessageReader.ReadMessage<StringMessage>();
            Debug.Log("my name is " + s.value);
        }
        OnServerAddPlayer(conn, playerControllerId, extraMessageReader);
    }

	
	
	public void Quit(){
        StopHost();
		inLobby = true;
	}
}
