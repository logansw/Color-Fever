using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMan : MonoBehaviour
{
    public static SceneType s_currentScene;

    public static SceneType GetCurrentScene() {
        return s_currentScene;
    }

    public void Start() {
        s_currentScene = SceneType.Title;
    }

    public void LoadScene(string sceneName) {
        switch (sceneName) {
            case "Title":
                SceneManager.LoadScene("Title");
                s_currentScene = SceneType.Title;
                break;
            case "Single":
                SceneManager.LoadScene("Single");
                s_currentScene = SceneType.Single;
                break;
            case "Versus":
                SceneManager.LoadScene("Versus");
                s_currentScene = SceneType.Versus;
                break;
            case "Double":
                SceneManager.LoadScene("Double");
                s_currentScene = SceneType.Double;
                break;
        }
    }

    public void LoadScene(SceneType scene) {
        switch (scene) {
            case SceneType.Title:
                SceneManager.LoadScene("Title");
                s_currentScene = SceneType.Title;
                break;
            case SceneType.Single:
                SceneManager.LoadScene("Single Board");
                s_currentScene = SceneType.Single;
                break;
            case SceneType.Versus:
                SceneManager.LoadScene("Versus");
                s_currentScene = SceneType.Versus;
                break;
            case SceneType.Double:
                SceneManager.LoadScene("Double");
                s_currentScene = SceneType.Double;
                break;
        }
    }
}