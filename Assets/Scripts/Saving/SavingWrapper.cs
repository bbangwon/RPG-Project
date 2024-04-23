using System.Collections;
using UnityEngine;

namespace RPG.Saving
{
    public class SavingWrapper : MonoBehaviour
    {
        SavingSystem savingSystem;
        const string defaultSaveFile = "save";

        private void Awake()
        {
            savingSystem = GetComponent<SavingSystem>();
        }

        private IEnumerator Start()
        {
            yield return savingSystem.LoadLastScene(defaultSaveFile);
        }

        private void Update()
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
            savingSystem.Save(defaultSaveFile);
        }

        public void Load()
        {
            savingSystem.Load(defaultSaveFile);
        }
    }

}