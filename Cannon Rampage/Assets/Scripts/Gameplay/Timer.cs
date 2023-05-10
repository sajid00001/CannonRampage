using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    [SerializeField]private float totalTimeInSecs = 120f;
    public TextMeshProUGUI timerText;
    public GameManager gameManager;


    private bool canCheck = true;
    private void Update()
    {
        if (canCheck == false)
            return;

        totalTimeInSecs -= Time.deltaTime;

        if (totalTimeInSecs <= 0)
        {
            totalTimeInSecs = 0;
            gameManager.TimerUp();
        }

        int min = Mathf.FloorToInt(totalTimeInSecs / 60);
        int sec = Mathf.FloorToInt(totalTimeInSecs % 60);
        timerText.text = min.ToString("00") + ":" + sec.ToString("00");
    }
}
