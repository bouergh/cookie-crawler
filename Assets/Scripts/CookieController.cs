using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CookieController : MonoBehaviour {

    public static CookieController singleton;

    //Inspector Variables
    public Tilemap traversableMap;
    public Vector3 initialPosition;

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
    }

    // Store input
    //public void storeInput(int input) {
    //    string inputsStr = "";
    //    this.inputs.Add(input);
    //    foreach(int i in inputs) {
    //        inputsStr += i.ToString() + ",";
    //    }
    //    Debug.Log("Current inputs : " + inputsStr);
    //}

    //public void removeActionIfExist(int input)
    //{
    //    if (inputs.Contains(input)) {
    //        inputs.Remove(input);
    //    }
    //}

    public void spaceDownFor(int player) {
        inputDown[player] = true;
    }

    public void spaceUpFor(int player) {
        inputDown[player] = false;
    }

    // Choice action to execute
    void choiceAction() {
        bool[] i = inputDown;

        Vector3Int vector = new Vector3Int();

        // 1(0) Left (1,0,0)
        // 2(1) Down (0,-1,0)
        // 3(2) Up   (0,1,0)
        // 4(3) Right(-1,0,0)

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

    void MoveForward(Vector3Int action)
    {
        Vector3Int newPos;

        newPos = Vector3Int.RoundToInt(transform.position) + action;

        if (traversableMap.HasTile(newPos))
        {
            transform.position = newPos;
        }

        transform.rotation = Quaternion.LookRotation(Vector3.forward, action);
        PlayerController[] players = GetComponents<PlayerController>();
        foreach(PlayerController player in players) {
            Debug.Log("PLAYER : " + player.ToString());
        }
        
    }

    // Update is called once per frame
    void Update () {

    }
}
