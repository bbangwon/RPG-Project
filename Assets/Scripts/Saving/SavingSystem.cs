using System.IO;
using System.Text.Json;
using UnityEngine;

namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        public void Save(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            Debug.Log("Would save to " + path);
            using FileStream stream = File.Open(path, FileMode.Create);

            Transform playerTransform = GetPlayerTransform();
            JsonSerializer.Serialize(stream, new SerializableVector3(playerTransform.position));
        }

        private Transform GetPlayerTransform()
        {
            return GameObject.FindWithTag("Player").transform;
        }

        public void Load(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            Debug.Log("Would load from " + path);
            using FileStream stream = File.Open(path, FileMode.Open);

            SerializableVector3 position = JsonSerializer.Deserialize<SerializableVector3>(stream);

            Transform playerTransform = GetPlayerTransform();
            playerTransform.position = position.ToVector();
        }

        private string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
        }
    }
}
