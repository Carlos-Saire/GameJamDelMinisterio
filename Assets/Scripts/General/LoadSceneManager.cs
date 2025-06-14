using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    public static event Action OnLoadScene;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadScene("Menu");
        }
    }
    public static void LoadScene(string scene)
    {
        Time.timeScale = 1;
        OnLoadScene?.Invoke();
        SceneManager.LoadScene(scene);
    }
}
