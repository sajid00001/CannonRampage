using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISpriteAnimation : MonoBehaviour
{

    Image mImage;

    public Sprite[] spritesArray;
    public float animSpeed = 0.02f;
    private int indexSprite;
    private Coroutine corotineAnim;
    private bool isDone = false;

    private void OnEnable()
    {
        mImage = GetComponent<Image>();
        StartCoroutine(PlayAnim());
    }

    IEnumerator PlayAnim()
    {
        yield return new WaitForSeconds(animSpeed);
        if (indexSprite >= spritesArray.Length)
        {
            indexSprite = 0;
        }
        mImage.sprite = spritesArray[indexSprite];
        indexSprite += 1;
        if (isDone == false)
            corotineAnim = StartCoroutine(PlayAnim());
    }
}