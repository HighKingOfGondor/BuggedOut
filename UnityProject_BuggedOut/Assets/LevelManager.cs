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

    [Header("Prefabs")]
    public GameObject prefabHealthOn;
    public GameObject prefabHealthOff;
    public GameObject prefabPellet;



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
            }
            else
            {
                flasher.FlashScreen(true);
            }

            m_healthCurrent = Mathf.Clamp(value, 0, int.MaxValue);

            if (parentHealth.childCount != healthCurrent)
            {
                parentHealth.DestroyChildren();
                for (int i = 0; i < healthCurrent; i++)
                {
                    GameObject spawnedHealthOnObject = Instantiate(prefabHealthOn,parentHealth);

                }
                for (int i = 0; i < (healthMax - healthCurrent); i++)
                {
                    GameObject spawnedHealthOffObject = Instantiate(prefabHealthOff, parentHealth);
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
            m_stabilityCurrent = value;
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
        stabilityCurrent = 1f;
        scoreCurrent = 0;
        healthCurrent = healthMax;
        isPlaying = true;
    }

    public void EndLevel()
    {
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
