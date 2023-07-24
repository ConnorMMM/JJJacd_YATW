using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class MenuUI : MonoBehaviour

{
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

    public void PromptOpen(GameObject prompt)
    {
        prompt.SetActive(true);
    }

    public void PromptClose(GameObject prompt)
    {
        prompt.SetActive(false);
    }
}
