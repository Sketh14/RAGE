using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class StartScreenScript : MonoBehaviour
{
    public Text highScoreText;
    public GameObject bullet;
    public Transform shootPointOpeningScene;
    public Image startButton;

    private bool bulletCreated;
   // private bool start;
    private Animator anim;
    private int i;
    private GameObject currentBullet;

    private void Start()
        {
        anim = GetComponent<Animator>();
        }

    public void LateUpdate()
    {
        if(Time.timeScale <= 0.2)
        {
            Time.timeScale = 1;
        }
        ShowHighScore(); 
        if(bulletCreated)
        {
            //Debug.Log(startButton.transform.position.x - currentBullet.transform.localPosition.x);
            if (currentBullet.transform.localPosition.x >= 7.1)
            { 
                SceneManager.LoadScene(1);
            } 
        }
    }

    public void startGame()
    {
        if(i <= 0)
        {
            anim.SetBool("IsShooting", true);
            StartCoroutine(wait()); 
        }
        i++;
    }

    public void ShowHighScore()
    {
        highScoreText.text = SaveGameManager.loadHighScore().ToString();
    }

    public void ExitTheGame()
    {
        Application.Quit();
    }   

    IEnumerator wait()
    {
        yield return new WaitForEndOfFrame();
        currentBullet = Instantiate(bullet, shootPointOpeningScene.position, shootPointOpeningScene.rotation);
        bulletCreated = true;
    }
}
