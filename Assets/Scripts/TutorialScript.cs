using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public Animator anim;
     
    void Start()
    {
       // anim = GetComponent<Animator>();   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            if(this.name == "hasMoved")
            {
                anim.SetBool("HasWalked", true);
                Destroy(gameObject);
            }

            if (this.name == "hasDucked")
            {
                anim.SetBool("HasDucked", true);
                Destroy(gameObject);
            }
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        { 
            if (this.name == "hasAWeapon")
            {
                anim.SetTrigger("HasAWeapon");
                Destroy(gameObject);
            }

            if (this.name == "sawHealth")
            {
                anim.SetTrigger("SawHealth");
                Destroy(gameObject);
            }

            if (this.name == "sawAmmo")
            {
                anim.SetTrigger("SawAmmo");
                Destroy(gameObject);
            }

            if (this.name == "sawAnEnemy")
            {
                anim.SetTrigger("SawAnEnemy");
                Destroy(gameObject);
            }

            if (this.name == "sawAnEnemySpawner")
            {
                anim.SetTrigger("SawASpawner");
                Destroy(gameObject);
            }

            if (this.name == "hasJumped")
            {
                anim.SetBool("HasJumped", true);
                Destroy(gameObject);
            }
             
            if (this.name == "tutFinished")
            {
                anim.SetBool("HasFinishedTut", true);
                Destroy(gameObject);
            }
        }

        if(collision.name == "AmmoMedium")
        {
            anim.SetBool("HasShot", true);
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("OpeningFinished"))
        {
            anim.SetBool("IntroFinished", true);
        }
        
    }
}
