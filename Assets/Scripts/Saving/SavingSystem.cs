using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {

        public IEnumerator LoadLastScene(string saveFile)
        {
            Dictionary<string, object> state = LoadFile(saveFile);
            if(state.ContainsKey("lastSceneBuildIndex"))
            {
                int buildIndex = int.Parse(state["lastSceneBuildIndex"].ToString());

                if (buildIndex != SceneManager.GetActiveScene().buildIndex)
                {
                    yield return SceneManager.LoadSceneAsync(buildIndex);
                }
            }

            RestoreCapture(state);
        }

        public void Save(string saveFile)
        {
            Dictionary<string, object> state = LoadFile(saveFile);
            CaptureState(state);
            SaveFile(saveFile, state);
        }

        public void Load(string saveFile)
        {
            RestoreCapture(LoadFile(saveFile));
        }

        public void Delete(string defaultSaveFile)
        {
            File.Delete(GetPathFromSaveFile(defaultSaveFile));
        }

        private void SaveFile(string saveFile, Dictionary<string, object> dictionary)
        {
            string path = GetPathFromSaveFile(saveFile);
            using FileStream stream = File.Open(path, FileMode.Create);

            JsonSerializer.Serialize(stream, dictionary,
                               new JsonSerializerOptions
                               {
                                   WriteIndented = true
                               });
        }

        private Dictionary<string, object> LoadFile(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);

            if (!File.Exists(path)) return new Dictionary<string, object>();

            using FileStream stream = File.Open(path, FileMode.Open);
            return JsonSerializer.Deserialize<Dictionary<string, object>>(stream);
        }

        private void CaptureState(Dictionary<string, object> state)
        {
            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                state[saveable.GetUniqueIdentifier()] = saveable.CaptureState();
            }

            state["lastSceneBuildIndex"] = SceneManager.GetActiveScene().buildIndex;
        }

        private void RestoreCapture(Dictionary<string, object> state)
        {
            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                string id = saveable.GetUniqueIdentifier();
                if (state.ContainsKey(id))
                {
                    saveable.RestoreState(state[id]);
                }
            }
        }

        private string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
        }


    }
}
