using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
     private TrajectoryVisualizer trajectoryVisualizer;

    private bool isGameRunning = false;

    public Transform cannonTransform;

    public GameObject trajectoryStart;
    public GameObject trajectoryTarget;
    private void Awake()
    {
        trajectoryVisualizer = GetComponent<TrajectoryVisualizer>();

        HideMarker();

        GameManager.GameStarted += OnGameStart;
        GameManager.GameIsWon += OnGameFinished;
        GameManager.GameIsLost += GameOver;
    }

    private void OnDisable()
    {
        GameManager.GameStarted -= OnGameStart;
        GameManager.GameIsWon -= OnGameFinished;
        GameManager.GameIsLost -= GameOver;
    }

    void OnGameStart()
    {
        isGameRunning = true;
        trajectoryVisualizer.CanShowTrajectory();
        Invoke(nameof(EnableThrowingBombs), 0.35f);
        ShowMarker();
    }

    const float moveSpeed = -0.1f * 70f;

    private bool canThrow = false;
    public float fireDistance = 1f;
    void EnableThrowingBombs()
    {
        canThrow = true;
    }

    private void Update()
    {
        if (isGameRunning && canThrow)
        {
            Vector3 input = new Vector3(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0f, Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime);

            Vector3 startPointPosition = trajectoryStart.transform.position + new Vector3(input.x * 0.5f, input.z,0f);
            startPointPosition.x = Mathf.Clamp(startPointPosition.x, -1.3f, 1.3f);
            startPointPosition.y = Mathf.Clamp(startPointPosition.y, 12.2f, 14f);

            float heightRatio = ((trajectoryStart.transform.position.y - 12.15f) / 1.6f);
            heightRatio = Mathf.Clamp(heightRatio, 0.5f, 100f);

            fireDistance = heightRatio;

            trajectoryVisualizer.ChangeHeight(trajectoryStart.transform.position.y + 1.5f);

            Vector3 targetPosition = cannonTransform.forward * (fireDistance * 50f);
            targetPosition.y = trajectoryTarget.transform.position.y;

            trajectoryTarget.transform.position = targetPosition;
            trajectoryStart.transform.position = startPointPosition;

            trajectoryVisualizer.DrawTrajectory(trajectoryStart.transform.position, trajectoryTarget.transform.position);

            if (Input.GetKey(KeyCode.Space))
            {
                canThrow = false;
                StartCoroutine(FireRocket());
            }
        }
    }

    void HideMarker()
    {
        trajectoryTarget.SetActive(false);
        trajectoryVisualizer.HideTrajectory();
    }

    void ShowMarker()
    {
        trajectoryTarget.SetActive(true);
        trajectoryVisualizer.ShowTrajectory();
    }

    IEnumerator FireRocket()
    {
        HideMarker();

        GameObject rocketProjectile = RocketsPool.Instance.GetRocket();
        Rocket rocket = rocketProjectile.gameObject.GetComponent<Rocket>();
        yield return null;
        rocketProjectile.transform.position = trajectoryStart.transform.position;
        rocketProjectile.transform.forward = transform.forward;
        rocketProjectile.SetActive(true);
        rocket.Launch(trajectoryTarget.transform.position);

        yield return new WaitForSeconds(2f);
        ShowMarker();
        canThrow = true;
    }

    void OnGameFinished()
    {
        HideMarker();
        isGameRunning = false;
    }

    void GameOver()
    {
        HideMarker();
        isGameRunning = false;
    }
}
