using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CenasController : MonoBehaviour
{
    // Start is called before the first frame update
    public int TotalScenesInBuild;
    public string NameCurrentScene;
    public Scene[] AllSceneNames;

    public Scene IndexScene;

    Scene globalScene;
    void Start()
    {
        TotalScenesInBuild = SceneManager.sceneCountInBuildSettings;
        NameCurrentScene = SceneManager.GetActiveScene().name;
        //AllSceneNames = SceneManager.GetAllScenes();
        IndexScene = SceneManager.GetSceneAt(1);

        //SceneManager.LoadScene("CENA1");
        //SceneManager.LoadSceneAsync("Luz");
        

        globalScene = SceneManager.GetSceneByName("CENA1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
