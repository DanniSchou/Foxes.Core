namespace Foxes.Core
{
    using UnityEngine;

    public abstract class ConfigAsset : ScriptableObject, IConfig
    {
        public abstract void Configure();
    }
}