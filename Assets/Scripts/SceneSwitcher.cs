using System;
using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{
    /// <summary>
    /// Load a scene by name
    /// </summary>
    public void LoadSceneByName(string name)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }

    /// <summary>
    /// Load a scene by index
    /// </summary>
    public void LoadSceneByIndex(int index)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    }

    /// <summary>
    /// Load the next scene by using the next index in the build settings
    /// </summary>
    public void LoadNextScene()
    {
        int nextSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneIndex);
    }

    /// <summary>
    /// Load the previous scene by using the next index in the build settings
    /// </summary>
    public void PreviousScene()
    {
        int previousSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex - 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(previousSceneIndex);

    }

    /// <summary>
    /// Reload the current scene
    /// </summary>
    public void ReloadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Quit the application
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
