using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public string name;
    public List<GameObject> enemies;
    public int count;
    public float rate;
}