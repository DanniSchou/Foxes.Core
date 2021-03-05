#if UNITY_EDITOR
namespace Foxes.Core
{
    using System.IO;
    using UnityEditor;
    using UnityEngine;

    public partial class CoreSettings
    {
        private const string DefaultFolder = "/Resources/";
        private const string DefaultSavePath = "Assets" + DefaultFolder + ResourcesPath + ".asset";
        
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
    }
}
#endif