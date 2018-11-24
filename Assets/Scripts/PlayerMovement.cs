using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PlayerMovement : MonoBehaviour {

    //Inspector Variables
    const float playerSpeed= 10; //speed player moves
   
   void Update()
    {
        MoveForward(); // Player Movement 
    }

    void MoveForward()
    {
        if (Input.GetKey(KeyCode.W))//Press up arrow key to move forward on the Y AXIS
        {
            transform.Translate(0, playerSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.S))//Press up arrow key to move forward on the Y AXIS
        {
            transform.Translate(0, -playerSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.A))//Press up arrow key to move forward on the Y AXIS
        {
            transform.Translate(-playerSpeed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))//Press up arrow key to move forward on the Y AXIS
        {
            transform.Translate(playerSpeed * Time.deltaTime, 0, 0);
        }
    }
}
