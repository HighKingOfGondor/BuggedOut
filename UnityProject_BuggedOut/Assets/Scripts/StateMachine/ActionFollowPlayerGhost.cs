using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/FollowPlayerGhost")]
public class ActionFollowPlayerGhost : Action
{    

    public override void DoAction(Enemy enemy)
    {        
        enemy.FollowPosition(LevelManager.instance.transformPlayer.position);
    }
}
