using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : Singleton<DebugManager> {

    [Header("Main")]
    public bool enableDebug;    // Should disable all other debug functions
    // have different booleans for different aspects of the game
    // they should be able to be changed here

    // For example
    // [Header("Enemy Controller")]
    // public bool debugRaycasts;
    // public bool debugPathfindingPath;

    [Header("LoadSceneManager")]
    public bool debugFinishedLoadingSceneName;
    public bool debugSceneNameWhenCalled;
}
