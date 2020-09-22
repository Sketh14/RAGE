using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject Player;
    private Vector3 movement;
    //PlayerMovement p1;

    void Start()
    {
        //p1 = PlayerManager.MovePlayer;
        movement.y = 2;
        movement.z = -1;
    }

    void Update()
    {
        if(Player !=null)
        {
            movement.x = Player.transform.position.x + 7; 
        } 
        //gameEnds();
    }

    void LateUpdate()
    {
        transform.position = movement;
    }
    /*
     void gameEnds()
     { 
         if (p1.isDead)
         {
             StartCoroutine(wait());
             Time.timeScale = 0.1f;
             p1.GameOver.SetActive(true); 
         }
     }

     IEnumerator wait()
     {
         //Debug.Log("Yo1");
         yield return new WaitForFixedUpdate();
     }
     */
}
