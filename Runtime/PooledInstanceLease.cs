using System;
using Jeomseon.Unity.GameObjectPooling.Handles;
using Jeomseon.Unity.Ownership;
using UnityEngine;

namespace Jeomseon.Unity.GameObjectPooling.Ownership
{
    public sealed class PooledInstanceLease<T> : IOwnershipHandle<T>
        where T : Component
    {
        private GameObjectPoolHandle _pool;

        internal PooledInstanceLease(GameObjectPoolHandle pool, T value)
        {
            _pool = pool ?? throw new ArgumentNullException(nameof(pool));
            Value = value != null ? value : throw new ArgumentNullException(nameof(value));
        }

        public T Value { get; }
        public bool IsValid => _pool != null && _pool.IsValid && Value != null;

        public void Dispose()
        {
            var pool = _pool;
            _pool = null;
            if (pool is { IsValid: true } && Value != null) pool.Despawn(Value);
        }
    }
}
