using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorFlasher : MonoBehaviour
{

    [Header("Settings")]
    public float timeFlash = 0.5f;
    public Color colorFlashGood;
    public Color colorFlashBad;

    [Header("References")]
    public List<Image> toFlash = new List<Image>();

    Coroutine coroutineFlash;

    void Awake()
    {
        toFlash = GetComponentsInChildren<Image>().ToList();
    }
    
    public void FlashScreen(bool goodFlash)
    {
        if (coroutineFlash != null)
        {
            StopCoroutine(coroutineFlash);
        }
        coroutineFlash = StartCoroutine(Flash(goodFlash));
    }

    IEnumerator Flash(bool goodFlash)
    {
        Color alphaFull = colorFlashGood;
        if (goodFlash)
        {
            alphaFull = colorFlashGood;
        }
        else
        {
            alphaFull = colorFlashBad;
        }
        
        Color alphaNone = new Color (1, 1, 1, 0);

        float currentTime = 0f;
        while (currentTime < (timeFlash / 2f))
        {
            currentTime += Time.deltaTime;
            foreach (var i in toFlash)
            {
                i.color = Color.Lerp(alphaNone, alphaFull, currentTime / (timeFlash / 2f));
            }
            yield return null;
        }

        currentTime = 0f;
        while (currentTime < (timeFlash / 2f))
        {
            currentTime += Time.deltaTime;
            foreach (var i in toFlash)
            {
                i.color = Color.Lerp(alphaFull, alphaNone, currentTime / (timeFlash / 2f));                
            }
            yield return null;
        }

        foreach (var i in toFlash)
        {
            i.color = alphaNone;
        }
        yield break;
    }

}
