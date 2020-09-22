using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelect : MonoBehaviour
{
    public struct weaponsSelect
    {
        public GameObject weaponName; 
        public int clipsize;
        public int damageSize;
        public float GunFireSpeed;
        public float GunFireSpeedE;
    }
    public weaponsSelect[] weapons = new weaponsSelect[7];

    private void Start()
    {
        //Debug.Log("weapon select first");
        for (int i=0; i<7; i++)
        {
            weapons[i].weaponName = PlayerManager.Instance.weaponsList[i];
        }

        for(int i= 0; i<7; i++)
        {
            if(weapons[i].weaponName.transform.name == "Gun_SilencedRifle")
            {
                //Debug.Log(weapons[i].weaponName.transform.name);
                weapons[i].clipsize = 40;
                weapons[i].damageSize = 45;
                weapons[i].GunFireSpeed = 0.15f;
                weapons[i].GunFireSpeedE = 0.15f;
            }

            else if (weapons[i].weaponName.transform.name == "Gun_WierdPistol")
            {
                //Debug.Log(weapons[i].weaponName.transform.name);
                weapons[i].clipsize = 5;
                weapons[i].damageSize = 70;
                weapons[i].GunFireSpeed = 0f;
                weapons[i].GunFireSpeedE = 0.7f;
            }

            else if (weapons[i].weaponName.transform.name == "Gun_ModifiedRifle")
            {
                //Debug.Log(weapons[i].weaponName.transform.name);
                weapons[i].clipsize = 50;
                weapons[i].damageSize = 40;
                weapons[i].GunFireSpeed = 0.2f;
                weapons[i].GunFireSpeedE = 0.2f;
            }

            else if (weapons[i].weaponName.transform.name == "Gun_Pistol")
            {
                //Debug.Log(weapons[i].weaponName.transform.name);
                weapons[i].clipsize = 15;
                weapons[i].damageSize = 20;
                weapons[i].GunFireSpeed = 0f;
                weapons[i].GunFireSpeedE = 0.7f;
            }

            else if (weapons[i].weaponName.transform.name == "Gun_50cal")
            {
                //Debug.Log(weapons[i].weaponName.transform.name);
                weapons[i].clipsize = 5;
                weapons[i].damageSize = 100;
                weapons[i].GunFireSpeed = 0f;
                weapons[i].GunFireSpeedE = 1.3f;
            }

            else if (weapons[i].weaponName.transform.name == "Gun_Ak47")
            {
                //Debug.Log(weapons[i].weaponName.transform.name);
                weapons[i].clipsize = 30;
                weapons[i].damageSize = 60;
                weapons[i].GunFireSpeed = 0.15f;
                weapons[i].GunFireSpeedE = 0.15f;
            }

            else if (weapons[i].weaponName.transform.name == "Gun_m1")
            {
                Debug.Log(weapons[i].weaponName.transform.name);
                weapons[i].clipsize = 8;
                weapons[i].damageSize = 100;
                weapons[i].GunFireSpeed = 0f;
                weapons[i].GunFireSpeedE = 1.3f;
            }
        }
    }

}
