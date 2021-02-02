namespace Foxes.Core
{
    using JetBrains.Annotations;
    using UnityEngine;

    [PublicAPI]
    public class InjectedBehaviour : MonoBehaviour
    {
        protected virtual void Start()
        {
            Root.Instance.Inject(this);
        }
    }
}