using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject toSpawn;
    const int COOLDOWN = 10;
    private int cpt = COOLDOWN;

    void Update()
    {
        if (cpt >= COOLDOWN)
        {
            Spawn();
            cpt = 0;
        }
        cpt++;
    }

    void Spawn()
    {
        Instantiate(toSpawn, gameObject.transform.position, Quaternion.identity);
    }

}
