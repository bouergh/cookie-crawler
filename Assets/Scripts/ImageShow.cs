using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageShow : MonoBehaviour
{
    Animator animator;
    public bool isImgOn;
    public Image img;

    Spawner[] sp;
    SpikeTrap[] st;
    CookieController cc;

    void Start()
    {
        animator = img.GetComponent<Animator>();
        img.enabled = false;
        isImgOn = false;

        cc = CookieController.singleton;
        sp = FindObjectsOfType<Spawner>();
        st = FindObjectsOfType<SpikeTrap>();
    }
        
    public void ToggleOn()
    {
        Debug.Log("TOGGLE ON");
        Freeze();

        if (!isImgOn)
        {
            animator.SetBool("youLose", true);
            img.enabled = true;
            isImgOn = true;
        }
    }

    public void ToggleOff()
    {
        Debug.Log("TOGGLE OFF");
        if (isImgOn)
            animator.SetBool("youLose", false);

        Unfreeze();
    }

    void Freeze()
    {
        cc.canMove = false;
        foreach (SpikeTrap trap in st) { trap.enabled = false; }
        foreach (Spawner s in sp) { s.enabled = false; }

        FireBall[] fbs = FindObjectsOfType<FireBall>();
        foreach (FireBall fb in fbs)
        {
            fb.gameObject.GetComponent<Renderer>().enabled = false;
            Destroy(fb);
        }
    }

    void Unfreeze()
    {
        cc.canMove = true;
        foreach (SpikeTrap trap in st) { trap.enabled = true; }
        foreach (Spawner s in sp) { s.enabled = true; }
    }

    void Update()
    {
        if(isImgOn && !animator.GetBool("youLose"))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1) //is playing?
            {
                img.enabled = false;
                isImgOn = false;
            }
        }
    }
}
