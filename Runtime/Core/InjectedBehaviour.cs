namespace Foxes.Core
{
    using JetBrains.Annotations;
    using UnityEngine;

    [PublicAPI]
    public class InjectedBehaviour : MonoBehaviour
    {
        protected IInjector Injector;

        protected virtual void Awake()
        {
            Injector = Root.Injector;
        }

        protected virtual void Start()
        {
            Injector.Inject(this);
        }
    }
}