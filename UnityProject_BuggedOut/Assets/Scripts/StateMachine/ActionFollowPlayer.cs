using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/FollowPlayer")]
public class ActionFollowPlayer : Action
{
    public float timePathDelay = 0.1f;

    public override void DoAction(Enemy enemy)
    {
        enemy.UpdatePath(timePathDelay,LevelManager.instance.transformPlayer.position);
        enemy.FollowPath();
    }
}

