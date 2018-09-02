using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "Action")]
public class Condition
{
    public virtual bool CheckCondition(Enemy enemy)
    {
        return true;
    }
}
