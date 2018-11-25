using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FireBall : MonoBehaviour {
    public Tilemap traversableMap;
    public GameObject player;
	int speed = 10;
    Vector3 dir;

    void Start()
    {
        dir = Vector3.Normalize(player.transform.position - gameObject.transform.position);
        transform.rotation = Quaternion.LookRotation(dir, Vector3.forward);
    }

	// Update is called once per frame
	void Update () {
        transform.Translate(dir.x * Time.deltaTime * speed, dir.y * Time.deltaTime, 0);

        if (traversableMap.HasTile(Vector3Int.CeilToInt(gameObject.transform.position)))
        {
            Destroy(gameObject);
        }
    }
}
