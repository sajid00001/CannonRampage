using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetsManager : MonoBehaviour
{
    private GameObject[] targets;
    private bool isGameRunning = false;
    private void Awake()
    {
        targets = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            targets[i] = transform.GetChild(i).transform.gameObject;
        }

        GameManager.GameStarted += OnGameStart;
        GameManager.GameIsWon += OnGameFinished;
        GameManager.GameIsLost += OnGameFinished;
    }

    private void OnDisable()
    {
        GameManager.GameStarted -= OnGameStart;
        GameManager.GameIsWon -= OnGameFinished;
        GameManager.GameIsLost -= OnGameFinished;
    }

    void OnGameStart()
    {
        isGameRunning = true;
    }

    void OnGameFinished()
    {
        isGameRunning = false;
    }

    float timer = 0.0f;
    const float checkTime = 0.5f;

    private void Update()
    {
        if (isGameRunning)
        {
            timer += Time.deltaTime;

            if (timer > checkTime)
            {
                if (EnemyLeft() == false)
                    GameManager.GameIsWon();

                timer = 0.0f;
            }
        }
    }

    public float GetRatio()
    {
        return  (float) GetTargetDestroyedCount() / (float)targets.Length;
    }

    int GetTargetDestroyedCount()
    {
        int count = 0;

        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i].CompareTag("dead") || targets[i].CompareTag("wrecked"))
            {
                count++;
            }
        }

        return count;
    }

    public bool EnemyLeft()
    {
        bool isLeft = false;

        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i].CompareTag("dead") == false && targets[i].CompareTag("wrecked") == false)
            {
                isLeft = true;
                break;
            }
        }

        return isLeft;
    }
}
