using RPG.SavingV0;
using System.Collections;
using UnityEngine;

namespace RPG.SceneManagement.SavingV0
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defualtSaveFile = "save";
        SavingSystem savingSystem;
        [SerializeField] float fadeInTime = 0.2f;

        private void Awake()
        {
            savingSystem = GetComponent<SavingSystem>();
        }

        IEnumerator Start()
        {
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();
            yield return savingSystem.LoadLastScene(defualtSaveFile);
            yield return fader.FadeIn(fadeInTime);
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
