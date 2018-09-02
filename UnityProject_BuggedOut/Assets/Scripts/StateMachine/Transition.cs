using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Transition
{
    public List<Condition> conditions = new List<Condition>();
    public State state;
}
