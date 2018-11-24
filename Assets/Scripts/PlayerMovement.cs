using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

class PlayerMovement : MonoBehaviour
{
    const int TIME_OUT = 10;

    //Inspector Variables
    public Tilemap traversableMap;
    public Vector3 initialPosition;
    private int cpt = TIME_OUT;
    void Start()
    {
        transform.position = initialPosition;
    }

    void Update()
    {
        if (cpt >= TIME_OUT)
        {
            MoveForward();
            cpt = 0;
        }
        cpt++;
    }

    void MoveForward()
    {
        Vector3Int newPos;

        if (Input.GetKey(KeyCode.W))//Press up arrow key to move forward on the Y AXIS
        {
            newPos = Vector3Int.RoundToInt(transform.position) + Vector3Int.up;

            if (traversableMap.HasTile(newPos))
            {
                transform.position = newPos;
            }

            transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
        }
        if (Input.GetKey(KeyCode.S))//Press up arrow key to move forward on the Y AXIS
        {
            newPos = Vector3Int.RoundToInt(transform.position) + Vector3Int.down;

            if (traversableMap.HasTile(newPos))
            {
                transform.position = newPos;
            }
            transform.rotation = Quaternion.AngleAxis(180, Vector3.forward);
        }
        if (Input.GetKey(KeyCode.A))//Press up arrow key to move forward on the Y AXIS
        {
            newPos = Vector3Int.RoundToInt(transform.position) + Vector3Int.left;

            if (traversableMap.HasTile(newPos))
            {
                transform.position = newPos;
            }
            transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
        }
        if (Input.GetKey(KeyCode.D))//Press up arrow key to move forward on the Y AXIS
        {
            newPos = Vector3Int.RoundToInt(transform.position) + Vector3Int.right;

            if (traversableMap.HasTile(newPos))
            {
                transform.position = newPos;
            }
            transform.rotation = Quaternion.AngleAxis(-90, Vector3.forward);
        }
    }
}
