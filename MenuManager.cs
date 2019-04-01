using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    private static Scene currentScene;
    private static bool created = false;
    //public GameObject target;


    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
    }

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    public void NextScene()
    {
        if (currentScene.name == "StartMenu"){
            SceneManager.LoadScene("Scene1", LoadSceneMode.Single);
        }

        else if (currentScene.name == "GameOver")
        {
            SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
        }

        else if (currentScene.name == "YouWin")
        {
            SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
        }
    }

    public static void NextSceneFromDeath(){
        //PlayerController.setHealthToMax();
        //target.SendMessage("setHealthToMax");
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }

    public static void NextSceneFromWin()
    {
        PlayerController.Reset();
        SceneManager.LoadScene("YouWin", LoadSceneMode.Single);
    }

    public void Exit(){
        Application.Quit();
    }
}
