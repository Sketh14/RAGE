using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{ 
    private Rigidbody2D rb;
    private Vector3 lastPosition;
    public EnemyMovement aboutEnemyClones; 
    private GameObject g;
    private EnemyMovement enemyWeaponDetail;
    private CapsuleCollider2D bulletCollider;

    void Start()
    {
        bulletCollider = GetComponent<CapsuleCollider2D>();
        g = this.gameObject.transform.root.gameObject;
        enemyWeaponDetail = g.GetComponent<EnemyMovement>(); 
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * 800.0f);
        lastPosition = transform.position;
       //Ammo();
    }

    private void Update()
    {
        if (Mathf.Abs(lastPosition.x - transform.position.x) >= 30)
        {
            //Debug.Log("Destroy Bullet At Update");
            Destroy(gameObject);
        }
    }

    /*public void Ammo()
    {
        if (name == "AmmoMedium")
        {
            dmg = 40;
            //Debug.Log("assignedM");
            return 0.1f;
        }

        else if (name == "AmmoHeavy")
        {
            dmg = 70;
            //Debug.Log("assignedH");
            return 0f;
        }

        else
        {
            dmg = 20;
            //Debug.Log("assignedL");
            return 0f;
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyMovement enemy = collision.GetComponent<EnemyMovement>();
        if(enemy != null && PlayerManager.Instance.MovePlayer.indexOW != 10)
        {
            //Physics2D.IgnoreCollision(enemy.enemyCollider,bulletCollider);
            enemy.takeDamage(PlayerManager.Instance.MovePlayer.SelectedWeapon.weapons[PlayerManager.Instance.MovePlayer.indexOW].damageSize); 
            if(enemy.health <= 0)
            {
                PlayerManager.Instance.killCounter();
            }
            //Debug.Log("damage : "+ weaponDamage.SelectedWeapon.weapons[weaponDamage.indexOW].damageSize);
        }

        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if(player != null)
        {
                //Debug.Log(enemyWeaponDetail.randomizerOfEnemyWeapon);
                player.takeDamageByEnemy(PlayerManager.Instance.MovePlayer.SelectedWeapon.weapons[enemyWeaponDetail.randomizerOfEnemyWeapon].damageSize);
        }

        if(collision.tag != "Weapon" && collision.tag != "HealthRefill" && collision.tag != "AmmoRefill")
        {
            if(collision.name != "LvlControl" && collision.name != "LvlControl (1)")
                Destroy(gameObject);
        }
    } 
}
