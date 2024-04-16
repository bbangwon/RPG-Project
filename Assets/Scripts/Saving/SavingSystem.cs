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
            byte[] bytes = Encoding.UTF8.GetBytes("Çï·Î¿ì ¿ùµå!");
            stream.Write(bytes, 0, bytes.Length);
        }

        public void Load(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            Debug.Log("Would load from " + path);
            using FileStream stream = File.Open(path, FileMode.Open);
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);

            Debug.Log(Encoding.UTF8.GetString(buffer));
        }

        private string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
        }
    }
}
