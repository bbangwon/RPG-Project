using UnityEngine;

namespace RPG.Saving
{
    [ExecuteAlways]
    public class SaveableEntity : MonoBehaviour
    {
        [SerializeField] private string uniqueIdentifier = "";
        public string GetUniqueIdentifier()
        {
            return "";
        }

        public object CaptureState()
        {
            Debug.Log("Capturing state for " + GetUniqueIdentifier());
            return null;
        }

        public void RestoreState(object state)
        {
            Debug.Log("Restoring state for " + GetUniqueIdentifier());
        }

        private void Update()
        {
            if (Application.IsPlaying(gameObject)) return;

            Debug.Log("Editing");
        }
    }
}