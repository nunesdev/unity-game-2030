using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingController : MonoBehaviour
{   
    public string SceneLoad;
    public float TimeToLoad = 5;
    public enum TypeLoad {Loading, FixedTime};
    public TypeLoad TypeLoading;
    public Image ProgressBar;
    public Text ProgressText;
    private int Progress = 0;
    private string ProgressTextOriginal;
    void Start()
    {
        switch (TypeLoading)
        {
            case  TypeLoad.Loading:
                StartCoroutine(LoadOfScene (SceneLoad));
                break;
            case  TypeLoad.FixedTime:
                StartCoroutine (FixedTime (SceneLoad));
                break;
        }
        
        if(ProgressText != null)
        {
            ProgressTextOriginal = ProgressText.text;
        }
        
        if(ProgressBar != null) {
            ProgressBar.type = Image.Type.Filled;
            ProgressBar.fillMethod = Image.FillMethod.Horizontal;
            ProgressBar.fillOrigin = (int)Image.OriginHorizontal.Left;
        }

        IEnumerator LoadOfScene(string scene) {
            AsyncOperation loading = SceneManager.LoadSceneAsync(scene);
            while (!loading.isDone)
            {
                Progress = (int)(loading.progress * 100.0f);
                yield return null;
            }
        } 

        IEnumerator FixedTime(string scene) {
            yield return new WaitForSeconds(TimeToLoad);
            SceneManager.LoadScene(scene);
        }  
       
    }

    void Update()
    {
         switch (TypeLoading)
        {
            case  TypeLoad.Loading:
                break;
            case  TypeLoad.FixedTime:
                Progress = (int) (Mathf.Clamp((Time.time/TimeToLoad), 0.0f, 1.0f) * 100.0f);
                break;
        }

        if(ProgressText != null)
            ProgressText.text = ProgressTextOriginal + " " + Progress + "%";
        
        if(ProgressBar != null) {
            ProgressBar.fillAmount = (Progress / 100.0f);
        }

    }
}
 