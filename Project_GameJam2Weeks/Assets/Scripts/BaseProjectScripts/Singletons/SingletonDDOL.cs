using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonDDOL<T> : MonoBehaviour where T : MonoBehaviour
{

    static T _instance;
    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    GameObject newInstance = new GameObject();
                    newInstance.name = "SingletonDDOL[" + typeof(T).ToString() + "]";
                    _instance = newInstance.AddComponent<T>();
                }
                DontDestroyOnLoad(_instance);
            }
            return _instance;
        }
    }
}
