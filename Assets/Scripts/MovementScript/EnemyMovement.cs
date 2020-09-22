using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Animator m_Animator;
    private bool IsFacingRight; 
    //private bool moveRight;
   // private bool moveLeft;
    private bool visible;
    private bool LineOfSight;

    public int enemyMovement;
    private GameObject player;
    public int health = 100;
    private GameObject weaponOnEnemy;
    private GameObject weaponHolding;
    private bool shootPlayer;
    public int randomizerOfEnemyWeapon;
    private GameObject currentBullet_Enemy;
    private Transform firepointE;
    public BoxCollider2D enemyCollider;
    public Transform fp;

    private float fireRateEnemy;
    private float fireSpeedEnemy;
    public AudioSource GunShotByEnemy;

    void Start()
    {
        enemyCollider = this.GetComponent<BoxCollider2D>();
        player = PlayerManager.Instance.currentPlayer;
        //Debug.Log("name :" + player.name);
        m_Animator = GetComponent<Animator>();
        weaponOnEnemy = this.gameObject.transform.GetChild(0).gameObject;
        weaponInHand();
        selectedAmmoByEnemy();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Box")
        {
            enemyMovement = 2;
        }
    }

    private void Update()
    {
        if(health <= 0)
        {
            Die();
        }
        RaycastHit2D hit = Physics2D.Raycast(fp.position, fp.right, 15);
        if (hit.transform)
        {
            if (hit.transform.tag == "Player")
            {
                Debug.Log("hit" + hit.transform.name);
                LineOfSight = true;
            }
            //lr.SetPosition(1, hit.point);
        }
        else
            LineOfSight = false;
    }

    private void FixedUpdate()
    { 
        playerVisible();

        if (visible && LineOfSight)
        {
            shootPlayer = true;
            enemyMovement = 2;
        }

        else
        {
            shootPlayer = false;
            enemyWalk();
        }

        if (enemyMovement == 0)
        {
            if(!IsFacingRight)
            {
                Flip();
            }
            Move();
        }

        else if (enemyMovement == 1)
        {
            if(IsFacingRight)
            {
                Flip();
            }
            Move();
        }

        else
        {
            if(player != null)
            {
                m_Animator.SetBool("IsRunning", false);
                if (shootPlayer)
                {
                    //this.transform.LookAt(player.transform.position); 
                    m_Animator.SetBool("IsShooting", true);
                    shoot();
                }
                else
                {
                    m_Animator.SetBool("IsShooting", false);
                } 
            }
        }
    }

    void enemyWalk()
    {
        if(Time.time % 5 == 0)
        {
            enemyMovement = (int)Random.Range(0.0f, 2.0f);
            //Debug.Log("value  = " + enemyMovement );
        } 
    }

    void Flip()
    {
        IsFacingRight = !IsFacingRight; 
        transform.Rotate(0f, 180f, 0f); 
    }

    void playerVisible()
    {
        if(player != null)
        {
            if (Mathf.Abs(transform.position.x - player.transform.position.x) <= 15)
            {
                visible = true;
                if (transform.position.x < player.transform.position.x && IsFacingRight)
                {
                    Flip();
                }
                else if (transform.position.x > player.transform.position.x && !IsFacingRight)
                {
                    Flip();
                }
            }
            else
            {
                visible = false;
            }

        }
    }

    void Move()
    {
        Vector3 sp1 = new Vector3(0.05f, 0.0f);
        transform.Translate(sp1);
        m_Animator.SetBool("IsRunning", true);
    }

    private void weaponInHand()
    { 
        if(!PlayerManager.Instance.MovePlayer.isDead)
        {
            randomizerOfEnemyWeapon = Random.Range(0, 6);
            //Debug.Log("inside enemy movement" + randomizerOfEnemyWeapon);

            weaponHolding = PlayerManager.Instance.weaponsList[randomizerOfEnemyWeapon];
            var clone = Instantiate(weaponHolding, weaponOnEnemy.transform.position, weaponOnEnemy.transform.rotation, weaponOnEnemy.transform);

            clone.transform.localScale -= new Vector3(0.4f, 0.4f, 0f);
            //clone.transform.localRotation = Quaternion.Euler(0f, 0f, 90f); 
            clone.name = weaponHolding.name;
            GunShotByEnemy = clone.GetComponent<AudioSource>();

            firepointE = clone.transform.GetChild(0).transform;
            fireSpeedEnemy = PlayerManager.Instance.weaponSelected.weapons[randomizerOfEnemyWeapon].GunFireSpeedE;
            //Debug.Log(fireSpeedEnemy); 
        }
    }

    public void takeDamage(int damage)
    {
        health -= damage;
    }

    public void Die()
    {
            Destroy(gameObject); 
    }

    void shoot()
    {
        if (Time.time > fireRateEnemy)
        {
            GunShotByEnemy.Play();
            //PlayerManager.instance.GunSoundsPNE.EnemyShoots();
            fireRateEnemy = Time.time + fireSpeedEnemy;
            var clone = Instantiate(currentBullet_Enemy, firepointE.position, firepointE.rotation,this.transform.GetChild(0).GetChild(0).GetChild(0).transform);
            clone.name = currentBullet_Enemy.name;
            //Debug.Log(clone.transform.localPosition);
        }
    }

    void selectedAmmoByEnemy()
    {
        if (randomizerOfEnemyWeapon == 0 || randomizerOfEnemyWeapon == 1)
        { 
            currentBullet_Enemy = PlayerManager.Instance.bullet[0];
            //PlayerManager.instance.ammoShowing = PlayerManager.instance.showAmmo.transform.GetChild(0).gameObject; 
        }
        else if (randomizerOfEnemyWeapon == 2 || randomizerOfEnemyWeapon == 3 || randomizerOfEnemyWeapon == 4)
        { 
            currentBullet_Enemy = PlayerManager.Instance.bullet[1];
            //PlayerManager.instance.ammoShowing = PlayerManager.instance.showAmmo.transform.GetChild(1).gameObject; 
        }
        else if (randomizerOfEnemyWeapon == 5)
        { 
            currentBullet_Enemy = PlayerManager.Instance.bullet[2];
            //PlayerManager.instance.ammoShowing = PlayerManager.instance.showAmmo.transform.GetChild(2).gameObject; 
        }
    }
}
