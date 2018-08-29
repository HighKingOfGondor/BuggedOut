using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class Screen : MonoBehaviour
    {
        //[Header("Settings")]

        public virtual IEnumerator TransitionIn()
        {
            ScreenManager.instance.currentlyDisplayedScreens.Add(this);
            yield break;
        }

        public virtual IEnumerator TransitionOut()
        {
            ScreenManager.instance.currentlyDisplayedScreens.Remove(this);
            yield break;
        }

        public virtual void OnTransitionedIn()
        {

        }

        public virtual void OnTransitionedOut()
        {

        }
    }

}


