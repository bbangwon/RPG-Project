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
            FileStream stream = File.Open(path, FileMode.Create);
            byte[] bytes = Encoding.UTF8.GetBytes("Çï·Î¿ì ¿ùµå!");
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();
        }

        public void Load(string saveFile)
        {
            Debug.Log("Would load from " + GetPathFromSaveFile(saveFile));
        }

        private string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
        }
    }
}
