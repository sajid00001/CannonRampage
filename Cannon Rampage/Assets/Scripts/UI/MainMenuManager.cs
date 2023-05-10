using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuManager : MonoBehaviour
{
    public GameObject mainWindow;
    public GameObject optionsWindow;
    public GameObject sceneSelectionWindow;

    private void Awake()
    {
        OpenWindow(mainWindow);
    }

    void OpenWindow(GameObject window)
    {
        LeanTween.moveLocal(window, Vector3.zero, 0.5f);
    }

    void CloseWindow(GameObject window)
    {
        LeanTween.moveLocal(window, Vector3.up * 1000f, 0.5f);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void GameButtonClicked()
    {
        CloseWindow(mainWindow);
        OpenWindow(sceneSelectionWindow);
    }

    public void GameModesWindowBackButton()
    {
        OpenWindow(mainWindow);
        CloseWindow(sceneSelectionWindow);
    }

    public void SettingsButtonClicked()
    {
        CloseWindow(mainWindow);
        OpenWindow(optionsWindow);
    }

    public void SettingsBackButton()
    {
        OpenWindow(mainWindow);
        CloseWindow(optionsWindow);
    }
}
