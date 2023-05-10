using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketsPool : MonoBehaviour
{
    public GameObject rocket;
    private GameObject[] rockets;

    public static RocketsPool Instance;
    private void Awake()
    {
        if (Instance != this)
            Destroy(Instance);

        if (Instance == null)
            Instance = this;

        rocket.SetActive(false);

        rockets = new GameObject[100];

        for (int i = 0; i < rockets.Length; i++)
        {
            rockets[i] = Instantiate(rocket);
        }
    }

    int index = -1;

    public GameObject GetRocket()
    {
        index++;

        if (index >= rockets.Length)
            index = 0;

        GameObject selectedObject = rockets[index];
        selectedObject.SetActive(false);
        return selectedObject;
    }
}
