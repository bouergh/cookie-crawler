﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Tilemaps;

public class CookieController : NetworkBehaviour {

    const int TIME_FREEZE = 50;
    int cpt_freeze = 0;
    bool frozen = false;
    public bool canMove = true;

    public static CookieController singleton;

    //Inspector Variables
    public Tilemap traversableMap;
    public Vector3 initialPosition;
    ImageShow imgShow;

    public Vector3 cookieOffsetPos;

    public PlayerController[] players;

    // List inputs for action and from network
    //    private List<int> inputs = new List<int>();

    // List input down
    private bool[] inputDown = new bool[4];

    // Time in second between check for action
    public float _checkTime = 1;


    // Initialization
    void Awake()
    {
        // Check existing Cookie
        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            Destroy(gameObject);
        }

        inputDown[0] = false;
        inputDown[1] = false;
        inputDown[2] = false;
        inputDown[3] = false;
    }

    // Use this for initialization
    void Start () {
        InvokeRepeating("choiceAction", 0, _checkTime);
        initialPosition = gameObject.transform.position;
        imgShow = FindObjectOfType<ImageShow>();
    }

    public void spaceDownFor(int player) {
        inputDown[player] = true;
    }

    public void spaceUpFor(int player) {
        inputDown[player] = false;
    }

    public void setPlayers() {
        players = FindObjectsOfType<PlayerController>();
    }

    // Choice action to execute
    void choiceAction() {
        bool[] i = inputDown;

        Vector3Int vector = new Vector3Int();

        if(i[0]) {
            vector.x += -1;
        }

        if (i[1]) {
            vector.y += -1;
        }

        if (i[2]) {
            vector.y += 1;
        }

        if (i[3]) {
            vector.x += 1;
        }

        MoveForward(vector);
    }

    [Server]
    void MoveForward(Vector3Int action)
    {
        if (!canMove) { return; }
        Vector3Int newPos;

        newPos = Vector3Int.RoundToInt(transform.position) + action;


        if (traversableMap.HasTile(newPos))
        {
            transform.position = newPos;
            foreach (PlayerController player in players)
            {
                RpcMovePlayer(player.gameObject, action);
            }

        }

        //transform.rotation = Quaternion.LookRotation(Vector3.forward, action);

    }

    [ClientRpc]
    public void RpcMovePlayer(GameObject player, Vector3 action) {
        player.transform.position += action;
    }
    
    void Update()
    {
        if(frozen) {
            Debug.Log("FROZEN");
            if (TIME_FREEZE < cpt_freeze)
            {
                Debug.Log("UNFREEZE");
                frozen = false;
                imgShow.ToggleOff();
                cpt_freeze = 0;
            }

            cpt_freeze++;
        }
    }

    public void TriggerDeath()
    {
        if (!frozen)
        { 
            Debug.Log("TRIGGERED DEATH");
            singleton.transform.position = singleton.initialPosition;
            imgShow.ToggleOn();
            frozen = true;
        }
    }

}
