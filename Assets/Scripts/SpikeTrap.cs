using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour {

    const int TIME_DANGER = 100;
    float cpt = 0;
    Animator animator;
    bool isDangerous;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        Debug.Log(animator);
        animator.SetBool("isDanger", isDangerous);
    }
    void Update()
    {
        SetDanger();
    }

    void SetDanger()
    {
        if (cpt >= TIME_DANGER)
        {
            isDangerous = !isDangerous;
            //Debug.Log("SET DANGER " + isDangerous);
            animator.SetBool("isDanger", isDangerous);
            cpt = 0;
        }

        cpt++;
        //Debug.Log("UPDATING CPT");

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("ON TRIGGER ENTER 2D");
        if (isDangerous)
        {
            //Debug.Log("IS DANGEROUS");
            CookieController.singleton.transform.position = CookieController.singleton.initialPosition;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        
        Debug.Log("collision spike and cookie or something else");
        Debug.Log("ON TRIGGER STAY 2D");
        Debug.Log(isDangerous);
        if (isDangerous)
        {
            Debug.Log("IS DANGEROUS!!");
            CookieController.singleton.transform.position = CookieController.singleton.initialPosition;
            //CookieController.singleton.Death();
            Debug.Log("POSITION SET!!");
        }
    }



}
