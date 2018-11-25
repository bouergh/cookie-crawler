using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject toSpawn;
    const int COOLDOWN = 100;
    const int RANGE = 10;
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
        if(PlayerInRange())
            Instantiate(toSpawn, gameObject.transform.position, Quaternion.identity);
    }

    bool PlayerInRange()
    {
        return Vector3.Distance(CookieController.singleton.transform.position, transform.position) < RANGE;
    }
}
