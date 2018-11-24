using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieController : MonoBehaviour {

    public static CookieController singleton;

    // List inputs for action and from network
    private List<int> inputs = new List<int>();

    // Time in second between check for action
    private int _checkTime = 1;


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
    }

    // Use this for initialization
    void Start () {
        InvokeRepeating("choiceAction", 0, _checkTime);
	}

    // Store input
    public void storeInput(int input) {
        string inputsStr = "";
        this.inputs.Add(input);
        foreach(int i in inputs) {
            inputsStr += i.ToString() + ",";
        }
        Debug.Log("Current inputs : " + inputsStr);
    }

    public void removeActionIfExist(int input)
    {
        if (inputs.Contains(input)) {
            inputs.Remove(input);
        }
    }

    // Choice action to execute
    void choiceAction() {
        if (inputs.Count != 0) {
            int choice = Random.Range(0, inputs.Count);
            Debug.Log("ACTION : " + inputs[choice]);
            inputs = new List<int>();
        } else {
            Debug.Log("NO ACTION");
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
