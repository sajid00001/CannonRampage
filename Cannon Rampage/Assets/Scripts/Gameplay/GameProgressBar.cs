using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameProgressBar : MonoBehaviour
{
    public TargetsManager targetsManager;
    public Image fillBar;
    private void FixedUpdate()
    {
        fillBar.fillAmount = targetsManager.GetRatio();
    }
}
