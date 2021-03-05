namespace FoxesEditor.Core
{
    using Foxes.Core;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.UIElements;

    public class CoreSettingsProvider : SettingsProvider
    {
        private SerializedObject _coreSettings;

        private CoreSettingsProvider()
            : base("Project/Foxes/Core", SettingsScope.Project, new[] { "configs" })
        {
        }
        
        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            _coreSettings = CoreSettings.GetSerializedSettings();
        }
        
        public override void OnGUI(string searchContext)
        {
            EditorGUILayout.PropertyField(_coreSettings.FindProperty("configs"), new GUIContent("Configs"));

            _coreSettings.ApplyModifiedProperties();
        }

        [SettingsProvider]
        public static SettingsProvider CreateMyCustomSettingsProvider()
        {
            return new CoreSettingsProvider();
        }
    }
}