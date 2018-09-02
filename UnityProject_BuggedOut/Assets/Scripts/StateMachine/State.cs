using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State")]
public class State : ScriptableObject
{
    public List<Transition> transitions = new List<Transition>();
    public List<Action> actions = new List<Action>();    
}
