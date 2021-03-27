using System.IO;
using UnityEngine;

namespace Moonlighter_Mod_Helper.Api
{
    public class JsonSerializer
    {
        public string SerializeJson<T>(T objectToSerialize, bool shouldIndent = false)
        {
            return JsonUtility.ToJson(objectToSerialize, shouldIndent);
        }

        public T DeserializeJson<T>(string text)
        {
            return JsonUtility.FromJson<T>(text);
        }




        /// <summary>
        /// Create an instance of a class from file
        /// </summary>
        /// <typeparam name="T">The type to load</typeparam>
        /// <param name="filePath">Location of the file</param>
        public T LoadFromFile<T>(string filePath) where T : class
        {
            string json = ReadTextFromFile(filePath);
            return (string.IsNullOrEmpty(json)) ? null : DeserializeJson<T>(json);
        }

        private string ReadTextFromFile(string filePath)
        {
            if (!IsPathValid(filePath))
                return null;

            string text = File.ReadAllText(filePath);
            if (string.IsNullOrEmpty(text))
                return null;

            return text;
        }

        private bool IsPathValid(string filePath)
        {
            Guard.ThrowIfStringIsNull(filePath, "Can't load file, path is null");
            return File.Exists(filePath);
        }




        /// <summary>
        /// Save an instance of a class to file
        /// </summary>
        /// <typeparam name="T">Type of class to save</typeparam>
        /// <param name="jsonObject">Object to save. Must be of Type T</param>
        /// <param name="savePath">Location to save file to</param>
        /// <param name="overwriteExisting">Overwrite the file if it already exists</param>
        public void SaveToFile<T>(T jsonObject, string savePath, bool shouldIndent = false, bool overwriteExisting = true)
        {
            Guard.ThrowIfStringIsNull(savePath, "Can't save file, save path is null");
            CreateDirIfNotFound(savePath);

            bool keepOriginal = !overwriteExisting;
            StreamWriter serialize = new StreamWriter(savePath, keepOriginal);

            string json = SerializeJson(jsonObject, shouldIndent);
            serialize.Write(json);
            serialize.Close();
        }

        private void CreateDirIfNotFound(string dir)
        {
            FileInfo f = new FileInfo(dir);
            Directory.CreateDirectory(f.Directory.FullName);
        }
    }
}
