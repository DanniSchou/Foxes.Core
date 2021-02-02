namespace Foxes.Core
{
    using System.Collections.Generic;
    using System.IO;
    using UnityEditor;
    using UnityEngine;

    public sealed class CoreSettings : ScriptableObject
    {
        private const string ResourcesPath = "CoreSettings";
        private const string DefaultFolder = "/Resources/";
        private const string DefaultSavePath = "Assets" + DefaultFolder + ResourcesPath + ".asset";

        [SerializeField] 
        private ConfigAsset[] configs;

        public IEnumerable<ConfigAsset> Configs => configs;

        public static CoreSettings GetOrCreateSettings()
        {
            var settings = Resources.Load<CoreSettings>(ResourcesPath);
            if (settings != null) return settings;

            CreateDefaultFolderIfNeeded();
        
            settings = CreateInstance<CoreSettings>();
            AssetDatabase.CreateAsset(settings, DefaultSavePath);
            AssetDatabase.SaveAssets();
        
            return settings;
        }

        private static void CreateDefaultFolderIfNeeded()
        {
            var folderPath = $"{Application.dataPath}/{DefaultFolder}";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }

        public static SerializedObject GetSerializedSettings()
        {
            return new SerializedObject(GetOrCreateSettings());
        }
    }
}