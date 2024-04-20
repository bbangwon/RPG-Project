using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using UnityEditorInternal;
using UnityEngine;

namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        public void Save(string saveFile)
        {
            SaveFile(saveFile, CaptureState());
        }

        public void Load(string saveFile)
        {
            RestoreCapture(LoadFile(saveFile));
        }

        private void SaveFile(string saveFile, Dictionary<string, object> dictionary)
        {
            string path = GetPathFromSaveFile(saveFile);
            Debug.Log("Would save to " + path);
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
            Debug.Log("Would load from " + path);
            using FileStream stream = File.Open(path, FileMode.Open);

            return JsonSerializer.Deserialize<Dictionary<string, object>>(stream);
        }

        private Dictionary<string, object> CaptureState()
        {
            Dictionary<string, object> state = new Dictionary<string, object>();
            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                state[saveable.GetUniqueIdentifier()] = saveable.CaptureState();
            }
            return state;
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
