using UnityEngine;

namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        public void Save(string saveFile)
        {
            Debug.Log("Would save to " + saveFile);
        }

        public void Load(string saveFile)
        {
            Debug.Log("Would load from " + saveFile);
        }
    }
}
