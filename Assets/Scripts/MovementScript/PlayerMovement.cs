using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{ 
    Animator m_Animator;
    private Vector3 lastPosition;
    public float movementSpeed;
    public float JumpSpeed;
    public int health = 100;
    public GameObject weaponPlacement;
    public int indexOW;
    public int cntrl;
    public bool isDead;
    public GameObject barrierDontPass;
    public GameObject GameOver;
    public GameObject[] MainCanvasOff;
    //public GameObject currentWeapon;

    //private int dir;
    private Rigidbody2D rb;
    private int jumpCounter;
    //PlayerShoot playerInfo;

    //public Text ClickJump;
    public GameObject[] ClickDuck;

    public GameObject weaponDescription;
    public WeaponSelect SelectedWeapon;

    private bool move;
    private bool moveRight;
    private bool jump;
    private bool OnObject;
    private bool duck;
    private bool CanShootWJ;
    private int currentIndex;
    private int jControl;
    private int moveRightCounter;
    //private bool Shoot;
     
    public bool IsFacingRight;
    public bool hasAGun;

    void Start ()
    {
        SelectedWeapon = weaponDescription.GetComponent<WeaponSelect>();
        //playerInfo = this.gameObject.GetComponent<PlayerShoot>();
        m_Animator = GetComponent<Animator>(); 
        IsFacingRight = true;
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        jumpCounter = 0; 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Barrier")
        {
            OnObject = true;
            m_Animator.SetBool("IsJumping", false);
        }
        else
        {
            OnObject = false;
        }
      /*
        if(collision.gameObject.tag == "Ground")
        {
            ClickJump.gameObject.SetActive(false); 
        }  */
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.gameObject.tag == "Weapon" && collision.gameObject.transform.root.tag != "Enemy")
        {
            hasAGun = true;
            m_Animator.SetBool("HasAGun", true);
            pickupWeapon(collision.transform);
            //Debug.Log("At Trigger, index" + currentIndex);
            if(currentIndex != indexOW && jControl >= 2)
            {
                weaponPlacement.transform.GetChild(currentIndex).gameObject.SetActive(false); 
            }
            //for mobile test build, line deactivated
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "LevelControl")
        {
            ++cntrl;
            if(cntrl >=7)
            {
                cntrl = 0;
            }
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.tag == "HealthRefill")
        {
            if(health<=100)
            {
                health += 50; 
                if(health>100)
                {
                    health -= health - 100;
                }
            }
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "AmmoRefill")
        {
            if(PlayerManager.Instance.playerShoots.totalAmmo >0 && PlayerManager.Instance.playerShoots.totalAmmo <= 350)
            {
                Debug.Log("Done");
                PlayerManager.Instance.playerShoots.totalAmmo += 100;
                PlayerManager.Instance.playerShoots.TotalAmmo.text = PlayerManager.Instance.playerShoots.totalAmmo.ToString();
            }
            Destroy(collision.gameObject);
        }
    }

    void Update ()
    {
        //Debug.Log("YO u");
        currentIndex = indexOW;
        //Moving Code
        if (move)
        {
            Vector3 sp = new Vector3(1.0f, 0.0f);
            transform.Translate(sp * movementSpeed);
            m_Animator.SetBool("IsRunning", true);
        } 

        /*else if (moveLeft)
        {
            Vector3 sp = new Vector3(-1.0f, 0.0f);
            transform.Translate(sp * movementSpeed);
            m_Animator.SetBool("IsRunning", true);
        }*/

        else
        {
            m_Animator.SetBool("IsRunning", false);
        }

        //Jumping Code 
        if (this.transform.position.y >= 0.7f && !OnObject)
        { 
            m_Animator.SetBool("IsJumping", true);
            if (CanShootWJ)
            {
                if(hasAGun)
                    m_Animator.SetBool("HasAGun", true);
                m_Animator.SetBool("IsShooting", true);
            }
        }
        if (jump && jumpCounter == 1)
        {
            Vector3 sp1 = new Vector3(0.0f, JumpSpeed);
            rb.AddForce(sp1, ForceMode2D.Impulse);
            jump = false;
        }
        else
        {
            if (this.transform.position.y <= 0.7f)
            {
                m_Animator.SetBool("IsJumping", false); 
                if (jumpCounter > 1)
                {
                    jumpCounter--;
                }
            }
        }

        //Ducking Code
        if (duck)
        { 
            m_Animator.SetBool("IsDucking", true); 
        }
        else
        { 
            m_Animator.SetBool("IsDucking", false);
        }

        //Barrier Code
        if(moveRight)
        {
            if(!(this.transform.position.x - barrierDontPass.transform.position.x < 40))
            {
                Vector3 pl = new Vector3(this.transform.position.x - 40, 6.0f, 0.0f);
                barrierDontPass.transform.position = pl; 
            }
        }

        //developmentphase();
    } 

    public void movementRight()
    {
        if(!IsFacingRight)
        {
            Flip();
        }
        moveRight = true;
        move = true;
        ++moveRightCounter;
    }

    public void stationary()
    {
        move = false;
        moveRight = false;
        duck = false;
        //Shoot = false;
        for (int a = 0; a < ClickDuck.Length; a++)
        {
            if (ClickDuck[a].activeSelf)
                ClickDuck[a].SetActive(false);
        }
       // ClickDuck.gameObject.SetActive(false);
    }

    public void movementLeft()
    {
        if(IsFacingRight)
        {
            Flip(); 
        }
        //moveLeft = true; 
        move = true;
    }

    public void jumpOnClick()
    {
        jump = true;
        jumpCounter++;
        //ClickJump.gameObject.SetActive(true);
    } 

    public void DuckOnClick()
    {
        duck = true;
        for (int a = 0; a < ClickDuck.Length; a++)
        {
            if (!ClickDuck[a].activeSelf)
                ClickDuck[a].SetActive(true);
        }
        //ClickDuck.gameObject.SetActive(true);
    }

    /*public void ShootOnClick()
    {
        Shoot = true;
    }*/
    
    void Flip()
    {
        IsFacingRight = !IsFacingRight;
        transform.Rotate(0f, 180f, 0f);  
    } 

    void pickupWeapon(Transform weaponName)
    {
        for(int i = 0; i<7; i++)
        {
            if(weaponName.name == SelectedWeapon.weapons[i].weaponName.name)
            { 
                indexOW = i;
                weaponPlacement.transform.GetChild(i).gameObject.SetActive(true); 
                //Debug.Log("Picked UP");
                ++jControl;
            }
        } 
    }

    public void takeDamageByEnemy(int dmgByEnemy)
    {
        health -= dmgByEnemy;

        if(health<=0)
        {
            gameObject.SetActive(false); 
            //isDead = true;
            for (int a=0; a < MainCanvasOff.Length; a++)
            {
                if(MainCanvasOff[a].activeSelf)
                     MainCanvasOff[a].SetActive(false);
            }
            PlayerManager.Instance.saveHighScore();
           //if(!isDead)
               // StartCoroutine(wait());
            Time.timeScale = 0;
            GameOver.SetActive(true); 
        }
    }

  /*  IEnumerator wait()
    {
        //Debug.Log("Yo1");
        yield return new WaitForEndOfFrame();
    }
    */

    public void CanSWJ()
    {
        CanShootWJ = true;
    }

    public void CantSWJ()
    {
        m_Animator.SetBool("HasAGun", false);
        CanShootWJ = false;
    }

    /*void developmentphase()
    {//Shooting Code
     if(Shoot)
     {
         m_Animator.SetBool("IsShooting", true);
     }
     else
     {
         m_Animator.SetBool("IsShooting",false);
     } 

     if (Input.GetKeyDown(KeyCode.A) )
    {
        Vector3 ChangeScale = transform.localScale;

        ChangeScale.x *= -1;
        transform.localScale = ChangeScale;

        //Debug.Log("Now Facing Left");
    }
    if (Input.GetKey(KeyCode.D))
    {
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 sp = new Vector3(horizontal, 0.0f); 
        //Debug.Log("Key Pressed");
    }
    }*/
}
