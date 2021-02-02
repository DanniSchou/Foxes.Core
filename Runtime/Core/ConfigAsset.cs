namespace Foxes.Core
{
    using Injection;
    using UnityEngine;

    public abstract class ConfigAsset : ScriptableObject, IConfig
    {
        public abstract void Configure();
    }
}