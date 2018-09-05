using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager>
{

    [Header("Settings")]
    public Gradient gradientStability;
    public int healthMax = 2;
    public float rateStabilityDecrease = 0.1f;
    public float rateScoreIncrease = 1f;
    float nextScoreIncrease;
    [Space]
    public bool isPlaying = false;

    [Header("References")]
    public Transform transformPlayer;
    public Transform parentHealth;
    public Image imageFillStability;
    public Text textScore;
    public UI.Screen screenEnd;
    public ColorFlasher flasher;
    public Animator animPlayer;

    [Header("Prefabs")]
    public GameObject prefabHealthOn;
    public GameObject prefabHealthOff;
    public GameObject prefabPellet;

    public AudioClip deathAudio;
    public AudioClip hurtClip;
    public AudioClip respawnClip;

    int m_healthCurrent;
    public int healthCurrent
    {
        get
        {
            return m_healthCurrent;
        }
        set
        {
            if (m_healthCurrent > value)
            {
                flasher.FlashScreen(false);
                animPlayer.SetTrigger("Hit");
            }
            else
            {
                flasher.FlashScreen(true);
            }

            m_healthCurrent = Mathf.Clamp(value, 0, 3);

            if (parentHealth.childCount != healthCurrent)
            {
                parentHealth.DestroyChildren();
                for (int i = 0; i < healthCurrent; i++)
                {
                    GameObject spawnedHealthOnObject = Instantiate(prefabHealthOn,parentHealth);
                    Debug.Log("is" + healthCurrent);
                }
                for (int i = 0; i < (healthMax - healthCurrent); i++)
                {
                    AudioManager.instance.PlayClipLocalSpace(hurtClip);
                    GameObject spawnedHealthOffObject = Instantiate(prefabHealthOff, parentHealth);
                    Debug.Log("is" + healthCurrent);

                }
            }
            if (healthCurrent == 0)
            {
                EndLevel();
            }
        }
    }


    float m_stabilityCurrent;
    public float stabilityCurrent
    {
        get
        {
            return m_stabilityCurrent;
        }
        set
        {
            if (value <= 0)
            {
                EndLevel();
            }
            m_stabilityCurrent = Mathf.Clamp(value, 0f, 1f); ;
            imageFillStability.fillAmount = stabilityCurrent;
            imageFillStability.color = gradientStability.Evaluate(stabilityCurrent);
        }
    }

    int m_scoreCurrent = 0;
    public int scoreCurrent
    {
        get
        {
            return m_scoreCurrent;
        }
        set
        {
            m_scoreCurrent = value;
            textScore.text = scoreCurrent.ToString();
        }
    }

    void Awake()
    {

    }

    void Update()
    {
        if (isPlaying)
        {
            stabilityCurrent -= rateStabilityDecrease * Time.deltaTime;
            if (Time.time > nextScoreIncrease)
            {
                nextScoreIncrease = Time.time + rateScoreIncrease;
                scoreCurrent++;
            }
        }
    }

    public void StartLevel()
    {        
        stabilityCurrent = 0.3f;
        scoreCurrent = 0;
        healthCurrent = healthMax;
        isPlaying = true;
        AudioManager.instance.PlayClipLocalSpace(respawnClip);
    }

    public void EndLevel()
    {
        AudioManager.instance.PlayClipLocalSpace(deathAudio);
        animPlayer.SetBool("Dead", true);
        isPlaying = false;
        UI.ScreenManager.instance.ScreenAdd(screenEnd,false);        
    }

    public void CallbackReturnToMenu()
    {
        LoadSceneManager.instance.LoadScene("LargeMenuScreen");
    }

    public void CallbackReloadLevel()
    {
        LoadSceneManager.instance.LoadScene("Pathfinding");
    }
}
