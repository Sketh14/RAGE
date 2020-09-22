using System.Collections; 
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionScript : MonoBehaviour
{
    private Animator anim;
    public GameObject LoadingScreen;
    public Slider ProgressSlider;
    public Text ProgressText;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("ChangeSceneTransition", true);
    }

    private void Update()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("ChangeToNextScene"))
        {
            //int y = SceneManager.GetActiveScene().buildIndex;
            //SceneManager.LoadScene(++y);
            StartCoroutine(LoadNextScene()); 
        } 
    }
   // /*
        IEnumerator LoadNextScene()
        {
            int y = SceneManager.GetActiveScene().buildIndex;
            AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(++y);

            LoadingScreen.SetActive(true);
            
            loadingOperation.allowSceneActivation = false;
            while(!loadingOperation.allowSceneActivation)
            {
                float progressIL = Mathf.Clamp01(loadingOperation.progress / 0.9f);
                ProgressSlider.value = progressIL;
                ProgressText.text = (progressIL * 100).ToString("F0") + "%";

                yield return null;

                if(progressIL >= 1)
                   loadingOperation.allowSceneActivation = true;
            }
        } 
      //  */
   /* function LoadingScreen(lvl : String)
    {
        loadScreenObject.SetActive(true);
        async = SceneManager.LoadSceneAsync(lvl);
        GameControl.control.newPosition = entryPoint;

        async.allowSceneActivation = false;

        while (!async.allowSceneActivation)
        {
            loadingBar.value = async.progress;
            yield;

            if (async.progress >= 0.9f)
            {
                loadingBar.value = 1f;

                async.allowSceneActivation = true;

            }
        }
        */
    }
