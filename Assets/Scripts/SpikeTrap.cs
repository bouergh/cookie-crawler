using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour {

    const int TIME_DANGER = 200;
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
            animator.SetBool("isDanger", isDangerous);
            cpt = 0;
        }

        cpt++;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isDangerous)
            CookieController.singleton.TriggerDeath();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (isDangerous)
            CookieController.singleton.TriggerDeath();
    }


}
