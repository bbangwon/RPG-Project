using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Control;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {        
        PlayableDirector playableDirector;
        PlayerController playerController;
        ActionScheduler playerActionScheduler;


        private void Awake()
        {
            playableDirector = GetComponent<PlayableDirector>();
            
            GameObject player = GameObject.FindWithTag("Player");
            playerController = player.GetComponent<PlayerController>();
            playerActionScheduler = player.GetComponent<ActionScheduler>();

        }

        private void Start()
        {
            playableDirector.played += DisableControl;
            playableDirector.stopped += EnableControl;
        }

        void DisableControl(PlayableDirector playableDirector)
        {
            playerActionScheduler.CancelCurrentAction();
            playerController.enabled = false;
        }

        void EnableControl(PlayableDirector playableDirector)
        {
            playerController.enabled = true;
        }
    }
}
