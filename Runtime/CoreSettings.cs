namespace Foxes.Core
{
    using System.Collections.Generic;
    using UnityEngine;

    public partial class CoreSettings : ScriptableObject
    {
        private const string ResourcesPath = "CoreSettings";

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
    }
}