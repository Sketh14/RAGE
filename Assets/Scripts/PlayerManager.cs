using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public GameObject[] bullet = new GameObject[3];
    public GameObject[] weaponsList = new GameObject[7];
    public GameObject[] levels = new GameObject[11];
    public Transform spawnLevelPoint;
    public GameObject healthSpawn;
    public GameObject ammoSpawn;
    public GameObject showAmmo;
    public GameObject ammoShowing;
    public WeaponSelect weaponSelected;
    public Text HighScore;
    public GameObject tmpplayerInfo;

    public static int scoreKeeperInFile;
    public List<GameObject> enemyDetail;
    public PlayerMovement MovePlayer;
    public PlayerShoot playerShoots;
    public GameObject currentPlayer;
    public SoundManager GunSoundsPNE;

    public int highScoreCounter;

    private static PlayerManager _instance; 
    public static PlayerManager Instance
    {
        get
        {
            if(_instance == null)
            {
                GameObject obj = new GameObject("PlayerManager");
                obj.AddComponent<PlayerManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        enemyDetail = new List<GameObject>();
        playerShoots = this.GetComponent<PlayerShoot>();
        MovePlayer = this.GetComponent<PlayerMovement>();
        currentPlayer = this.gameObject;
        _instance = this;
        //tmpplayerInfo = this.GetComponent<GameObject>();
    }

    public int killCounter()
    {
        if(MovePlayer.health > 0)
        {
            highScoreCounter += 10;
            HighScore.text = highScoreCounter.ToString();
            //SaveGameManager.SaveHighScore(highScoreCounter);
            return 0;
        }

        else if(MovePlayer.health <= 0)
        {
            return highScoreCounter;
        }

        else
        {
            return 0;
        }
    }

    public void saveHighScore()
    {
        if (!tmpplayerInfo.activeSelf)
        {
            Debug.Log("Score Recorded");

            SaveGameManager.SaveHighScore(highScoreCounter);
        }
    }
}
