using RPG.Saving;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defualtSaveFile = "save";
        SavingSystem savingSystem;

        private void Awake()
        {
            savingSystem = GetComponent<SavingSystem>();
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }

            if(Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
        }

        public void Save()
        {
            Debug.Log("Saving game...");
            savingSystem.Save(defualtSaveFile);
        }

        public void Load()
        {
            Debug.Log("Loading game...");
            savingSystem.Load(defualtSaveFile);
        }
    } 
}
