using System;
using System.IO;
using System.Text;
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
            byte[] buffer = SerialzeVector(playerTransform.position);
            stream.Write(buffer, 0, buffer.Length);
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
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            Transform playerTransform = GetPlayerTransform();
            playerTransform.position = DeserializeVector(buffer);
        }

        private byte[] SerialzeVector(Vector3 vector)
        {
            byte[] bytes = new byte[3 * 4];
            BitConverter.GetBytes(vector.x).CopyTo(bytes, 0);
            BitConverter.GetBytes(vector.y).CopyTo(bytes, 4);
            BitConverter.GetBytes(vector.z).CopyTo(bytes, 8);
            return bytes;
        }

        private Vector3 DeserializeVector(byte[] buffer)
        {
            Vector3 result = new Vector3();
            result.x = BitConverter.ToSingle(buffer, 0);
            result.y = BitConverter.ToSingle(buffer, 4);
            result.z = BitConverter.ToSingle(buffer, 8);
            return result;
        }

        private string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
        }
    }
}
