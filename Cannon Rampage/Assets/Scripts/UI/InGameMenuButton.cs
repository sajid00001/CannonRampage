using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class InGameMenuButton : MonoBehaviour, IPointerClickHandler
{
    public GameObject menuUI;

    public void OnPointerClick(PointerEventData eventData)
    {
        LeanTween.moveLocal(menuUI, Vector3.zero, 0.5f);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void MenuBackButton()
    {
        LeanTween.moveLocal(menuUI, Vector3.up * 1000f, 0.5f);
    }
}
