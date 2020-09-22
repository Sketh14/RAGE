using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject tutorialOption;
    public Toggle tutorialToggle;
    public Animator anim1;
    public AudioMixer masterMixer;

    public GameObject childOfPause;

    private void Start()
    {
        childOfPause = tutorialOption.transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        dontShowWhenPause();
    }

    void dontShowWhenPause()
    {
        if(pausePanel.activeSelf)
        {
            anim1.enabled = false;
            foreach(Transform child in tutorialOption.transform)
            {
                 if(child.gameObject.activeSelf)
                 {
                    childOfPause = child.transform.gameObject;
                    childOfPause.SetActive(false);
                    if(childOfPause.activeSelf==false)
                    {
                        Debug.Log(childOfPause.transform.name); 
                    }
                }
            }
        }
        else if(!pausePanel.activeSelf && tutorialToggle.isOn)
        {
            anim1.enabled = true;
            childOfPause.SetActive(true);
        }
    }

    public void pauseOnPressed()
    {
        Time.timeScale = 0.0f;
    }

    public void quitTheGame()
    {
        Application.Quit();
    }

    public void continueOnPressed()
    {
        Time.timeScale = 1;
    }

    public void loadMainMenu()
    {
        SceneManager.LoadScene(0);
    } 

    public void SetSound(Slider slider)
    {
        masterMixer.SetFloat("musicVol", slider.value);
    }
}
