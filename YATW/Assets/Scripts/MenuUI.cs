using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuUI : MonoBehaviour

{
    public GameObject optionsPrompt;

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Quit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }

    public void OpenOptions()
    {
        optionsPrompt.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsPrompt.SetActive(false);
    }
}
