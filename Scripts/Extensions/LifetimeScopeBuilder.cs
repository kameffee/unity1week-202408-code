using UnityEngine;
using VContainer;

namespace Unity1week.Extensions
{
    public abstract class LifetimeScopeBuilder : MonoBehaviour
    {
        public abstract void Configure(IContainerBuilder builder);
    }
}