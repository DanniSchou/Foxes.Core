namespace Foxes.Core
{
    using JetBrains.Annotations;
    using UnityEngine;

    [PublicAPI]
    public class InjectedBehaviour : MonoBehaviour
    {
        protected Root Root;

        protected virtual void Awake()
        {
            Root = Root.Instance;
        }

        protected virtual void Start()
        {
            Root.Inject(this);
        }
    }
}