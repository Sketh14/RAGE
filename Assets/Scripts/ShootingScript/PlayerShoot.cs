using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    //public GameObject[] bullet = new GameObject[4];
    //public GameObject[] weapons = new GameObject[10];
    //public GameObject showAmmo;
    //public GameObject ammoShowing;
    public Text InClip;
    public Text TotalAmmo;

    private Transform firePoint;
    //private Bullet blt;
    private int fireAmmo; 
    private int currentIndex =10;
    private Animator anim;
    private float fireRate;
    private bool shoot;
    PlayerMovement gunOnPlayer;
    SoundManager GunSoundsTBP;
    private GameObject currentBullet;
    private float fireSpeed; 
    private int inClip;
    private int inClipCopy;
    public int totalAmmo;
    private bool sniperReloaded;
    //private int totalAmmoCopy;

    private void Start()
    {
        GunSoundsTBP = PlayerManager.Instance.GunSoundsPNE;
        gunOnPlayer = PlayerManager.Instance.MovePlayer;
        //Debug.Log("At Start, Index : " + currentIndex);
        selectedAmmo();
        anim = GetComponent<Animator>();

        /*if(blt != null)
        {
            fireSpeed = 0f;
        }*/
    }

    void Update()
    { 
        if (gunOnPlayer.indexOW != currentIndex)
        { 
            //Debug.Log("Done player shoot");
            selectedAmmo();
            currentIndex = gunOnPlayer.indexOW;
            inClip = gunOnPlayer.SelectedWeapon.weapons[currentIndex].clipsize;
            inClipCopy = inClip;
            InClip.text = inClip.ToString();

            if (currentIndex == 0 || currentIndex == 1)
            {
                totalAmmo = 50;
                TotalAmmo.text = totalAmmo.ToString();
            }
            else if (currentIndex == 5 || currentIndex == 6)
            {
                totalAmmo = 30;
                TotalAmmo.text = totalAmmo.ToString();
            }
            else
            {
                totalAmmo = 100;
                TotalAmmo.text = totalAmmo.ToString();
            }

            fireSpeed = gunOnPlayer.SelectedWeapon.weapons[currentIndex].GunFireSpeed; 
            firePoint = gunOnPlayer.weaponPlacement.transform.GetChild(currentIndex).transform.GetChild(0).transform; 
            //weapons[currentIndex].transform.GetChild(0).transform;
        }

        if(shoot)
        {
            anim.SetBool("IsShooting", true);
        } 
    }

    private void LateUpdate()
    {
        if (shoot)
        {
            if (currentIndex == 0 || currentIndex == 1 || currentIndex == 5 || currentIndex == 6)
            {
                ++fireAmmo;
            }
            Shoot();
        }
    }

    void Shoot()
    {   
        if(Time.time > fireRate && fireAmmo<=1 && inClipCopy>0 && totalAmmo >= 0)
        {
            PlayerManager.Instance.GunSoundsPNE.PlayerShoots();
            --inClipCopy;
            if (inClipCopy == 0 && currentIndex == 6 && !sniperReloaded)
            {
                PlayerManager.Instance.GunSoundsPNE.SniperEmpty();
                sniperReloaded = true;
                //Debug.Log("Empty Played Sniper");
            }
            InClip.text = inClipCopy.ToString(); 
            fireRate = Time.time + fireSpeed;
            var clone = Instantiate(currentBullet, firePoint.position, firePoint.rotation);
            clone.name = currentBullet.name;
            //Debug.Log("Time : " + Time.time + " Rate : "+ fireRate);
        }
        /*else if (fireAmmo <= 0)
        { 
            Instantiate(currentBullet, firePoint.position, firePoint.rotation);
            ++fireAmmo;
        }*/
    }

    public void GunShoot()
    {
        shoot = true;
        if(inClipCopy == 0 && PlayerManager.Instance.MovePlayer.hasAGun)
        {
            PlayerManager.Instance.GunSoundsPNE.ClipEmptySoundPlay();
            //Debug.Log("Empty Played"); 
        }
        if (gunOnPlayer.hasAGun)
            anim.SetBool("HasAGun", true);
        if(!gunOnPlayer.hasAGun)
        {
            shoot = false;
        }
    }

    public void LiftFinger()
    {
        shoot = false; 
        anim.SetBool("IsShooting", false);
        fireAmmo = 0;
    }

    void selectedAmmo()
    {
        if (gunOnPlayer.indexOW == 0 || gunOnPlayer.indexOW == 1)
        {
            PlayerManager.Instance.ammoShowing.SetActive(false);
            currentBullet = PlayerManager.Instance.bullet[0];
            PlayerManager.Instance.ammoShowing = PlayerManager.Instance.showAmmo.transform.GetChild(0).gameObject;
            PlayerManager.Instance.ammoShowing.SetActive(true);
        }
        else if (gunOnPlayer.indexOW == 2 || gunOnPlayer.indexOW == 3 || gunOnPlayer.indexOW == 4)
        {
            PlayerManager.Instance.ammoShowing.SetActive(false);
            currentBullet = PlayerManager.Instance.bullet[1];
            PlayerManager.Instance.ammoShowing = PlayerManager.Instance.showAmmo.transform.GetChild(1).gameObject;
            PlayerManager.Instance.ammoShowing.SetActive(true);
        }
        else if (gunOnPlayer.indexOW == 5 || gunOnPlayer.indexOW == 6)
        {
            PlayerManager.Instance.ammoShowing.SetActive(false);
            currentBullet = PlayerManager.Instance.bullet[2];
            PlayerManager.Instance.ammoShowing = PlayerManager.Instance.showAmmo.transform.GetChild(2).gameObject;
            PlayerManager.Instance.ammoShowing.SetActive(true);
        } 
    }

    public void reloadGun()
    {
        if (inClipCopy < inClip)
        {
            sniperReloaded = false;
            if (inClipCopy == 0 && totalAmmo > 0)
            {
                PlayerManager.Instance.GunSoundsPNE.PlayerReloads();
                Debug.Log("passed first if");
                totalAmmo -= inClip;
                inClipCopy = inClip;
            }
            else
            {
                PlayerManager.Instance.GunSoundsPNE.PlayerReloads();
                if (inClipCopy < totalAmmo)
                {
                    Debug.Log("passed if");
                    totalAmmo -= inClip - inClipCopy;
                    inClipCopy = inClip;
                }
                else
                {
                    Debug.Log("passed else");
                    inClipCopy += totalAmmo;
                    if(inClipCopy > inClip)
                    {
                        totalAmmo = inClipCopy - inClip;
                        inClipCopy -= totalAmmo;
                    }
                    else
                    {
                        totalAmmo = 0;
                    }
                }

                if (totalAmmo < 0)
                {
                    Debug.Log("passed second if");
                    totalAmmo = 0;
                    if (inClipCopy == 0)
                    {
                        inClipCopy = 0;
                    }
                } 
            }
        }
        InClip.text = inClipCopy.ToString();
        TotalAmmo.text = totalAmmo.ToString();
    }
}
