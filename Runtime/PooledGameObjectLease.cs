using System;
using Jeomseon.Unity.GameObjectPooling.Handles;
using Jeomseon.Unity.Ownership;
using UnityEngine;

namespace Jeomseon.Unity.GameObjectPooling.Ownership
{
    public sealed class PooledGameObjectLease : IOwnershipHandle<GameObject>
    {
        private GameObjectPoolHandle _pool;

        internal PooledGameObjectLease(GameObjectPoolHandle pool, GameObject value)
        {
            _pool = pool ?? throw new ArgumentNullException(nameof(pool));
            Value = value != null ? value : throw new ArgumentNullException(nameof(value));
        }

        public GameObject Value { get; }
        public bool IsValid => _pool != null && _pool.IsValid && Value != null;

        public void Dispose()
        {
            var pool = _pool;
            _pool = null;
            if (pool is { IsValid: true } && Value != null) pool.Despawn(Value);
        }
    }
}
