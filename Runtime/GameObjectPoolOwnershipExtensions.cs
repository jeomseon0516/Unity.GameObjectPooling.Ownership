using System;
using Jeomseon.Unity.GameObjectPooling.Contracts;
using Jeomseon.Unity.GameObjectPooling.Handles;
using UnityEngine;

namespace Jeomseon.Unity.GameObjectPooling.Ownership
{
    public static class GameObjectPoolOwnershipExtensions
    {
        public static PooledGameObjectLease SpawnOwned(this GameObjectPoolHandle pool)
        {
            if (pool == null) throw new ArgumentNullException(nameof(pool));
            return new PooledGameObjectLease(pool, pool.Spawn());
        }

        public static PooledGameObjectLease SpawnOwned(
            this GameObjectPoolHandle pool,
            in PoolSpawnOptions options)
        {
            if (pool == null) throw new ArgumentNullException(nameof(pool));
            return new PooledGameObjectLease(pool, pool.Spawn(options));
        }

        public static PooledInstanceLease<T> SpawnOwned<T>(this GameObjectPoolHandle pool)
            where T : Component
        {
            if (pool == null) throw new ArgumentNullException(nameof(pool));
            return new PooledInstanceLease<T>(pool, pool.Spawn<T>());
        }

        public static PooledInstanceLease<T> SpawnOwned<T>(
            this GameObjectPoolHandle pool,
            in PoolSpawnOptions options)
            where T : Component
        {
            if (pool == null) throw new ArgumentNullException(nameof(pool));
            return new PooledInstanceLease<T>(pool, pool.Spawn<T>(options));
        }
    }
}
