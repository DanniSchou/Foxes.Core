namespace Foxes.Core
{
    using System.Collections.Generic;
    using UnityEngine;
#if UNITY_EDITOR
    using System.IO;
    using UnityEditor;
#endif

    public class CoreSettings : ScriptableObject
    {
        private const string ResourcesPath = "CoreSettings";

#if UNITY_EDITOR
        private const string DefaultFolder = "/Resources/";
        private const string DefaultSavePath = "Assets" + DefaultFolder + ResourcesPath + ".asset";
#endif
        
        [SerializeField] 
        private ConfigAsset[] configs;

        public IEnumerable<ConfigAsset> Configs => configs;

        public static CoreSettings GetOrCreateSettings()
        {
            var settings = Resources.Load<CoreSettings>(ResourcesPath);
            
#if UNITY_EDITOR
            if (settings == null)
            {
                settings = CreateSettings();
            }
#endif
            
            return settings;
        }
        
#if UNITY_EDITOR
        private static CoreSettings CreateSettings()
        {
            CreateDefaultFolderIfNeeded();
            
            var settings = CreateInstance<CoreSettings>();
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
#endif
    }
}