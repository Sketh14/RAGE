using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] ShotSounds;
    public AudioClip[] ReloadSounds;
    //public AudioSource[] sound1;
    public AudioClip ClipEmptySound;
    public AudioClip SniperCESound;
    public GameObject BackGroundMusic;

    PlayerMovement pGunInfo;
    private AudioSource GunshotSND;
    private AudioSource ReloadGunSND;
    private AudioSource[] Bckgrndmsic;
    private AudioSource CE;
    private int i;
    private bool BackGroundMusicStarted;

    private void Start()
    {
        pGunInfo = PlayerManager.Instance.MovePlayer;
        AudioSource[] asources = GetComponents<AudioSource>();
        Bckgrndmsic = BackGroundMusic.GetComponents<AudioSource>();
        GunshotSND = asources[0];
        ReloadGunSND = asources[1];
        CE = asources[2];               //no reference

        Bckgrndmsic[0].Play();
        i = 0;
        PlayBackGroundMusic();
    }
    /*
    private void Update()
    {
        PlayBackGroundMusic();
    }
    */
    public void PlayerShoots()
    {
        if (pGunInfo.indexOW != 10)
        {
            GunshotSND.clip = ShotSounds[pGunInfo.indexOW];
            GunshotSND.Play(); 
        }
        //ShotSounds[pGunInfo.indexOW].
    }

    public void PlayerReloads()
    {
        if (pGunInfo.indexOW != 10)
        {
            ReloadGunSND.clip = ReloadSounds[pGunInfo.indexOW];
            ReloadGunSND.Play(); 
        }
    }

    public void ClipEmptySoundPlay()
    {
        //Debug.Log("Empty Played Gun");
        //CE.clip = ClipEmptySound;
        ReloadGunSND.PlayOneShot(ClipEmptySound);
    }

    public void SniperEmpty()
    {
        //CE.clip = SniperCESound;
        ReloadGunSND.PlayOneShot(SniperCESound);
    }

    public void PlayBackGroundMusic()
    {
        if (i == 0)
        {
            //Debug.Log("IN First One");
            StartCoroutine(waitAudio());
        }
        //Debug.Log("Start Second One  :  "+ i);
        else
        {
            BackGroundMusicStarted = true;
            //Debug.Log("Normal plays");
            StartCoroutine(waitAudio()); 
        }
        if (i == 2)
        {
            i = 0;
            //Debug.Log("i has turned");
        }
    }

    private IEnumerator waitAudio()
    {
        //Debug.Log("In Waiting  :  "+ i);
        yield return new WaitForSeconds(Bckgrndmsic[i].clip.length);
        if (i == 0 && !BackGroundMusicStarted)
            ++i;
        else if (i != 0)
            ++i;
        PlaysBackground();
        PlayBackGroundMusic();
        //Debug.Log("end of sound : "+ i);
    }

    public void PlaysBackground()
    {
        //Debug.Log(i);
        Bckgrndmsic[i].Play();
        if (i == 0)
            BackGroundMusicStarted = false;
    }
    /*
        public void EnemyShoots()
        {

        }

        public void EnemyReloads()
        {

        }
        */
}
