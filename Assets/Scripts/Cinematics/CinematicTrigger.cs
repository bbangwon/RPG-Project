using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        PlayableDirector playableDirector;

        bool hasBeenTriggered = false;

        private void Awake()
        {
            playableDirector = GetComponent<PlayableDirector>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!hasBeenTriggered && other.gameObject.tag == "Player")
            {
                hasBeenTriggered = true;
                playableDirector.Play();
            }
        }
    }
}