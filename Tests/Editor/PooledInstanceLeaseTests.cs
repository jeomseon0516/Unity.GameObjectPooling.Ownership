using Jeomseon.Unity.GameObjectPooling.Configurations;
using Jeomseon.Unity.GameObjectPooling.Scopes;
using Jeomseon.Unity.Ownership;
using NUnit.Framework;
using UnityEngine;

namespace Jeomseon.Unity.GameObjectPooling.Ownership.Tests
{
    internal partial class ManagedPoolOwner : MonoBehaviour
    {
        [ManagedPooledObject] private GameObject _instance;
    }

    public sealed class PooledInstanceLeaseTests
    {
        [Test]
        public void Dispose_ReturnsGameObjectToOriginalPoolExactlyOnce()
        {
            var prefab = new GameObject("Prefab");
            var scopeObject = new GameObject("Scope");
            var scope = scopeObject.AddComponent<GameObjectPoolScope>();
            var handle = scope.Register(
                new UnityGameObjectPoolConfiguration(prefab),
                PoolLifetimeConfiguration.Scope);
            var lease = handle.SpawnOwned();
            var instance = lease.Value;

            lease.Dispose();
            lease.Dispose();

            Assert.That(instance.activeSelf, Is.False);
            Assert.That(lease.IsValid, Is.False);
            Assert.That(handle.TryGetStatistics(out var statistics), Is.True);
            Assert.That(statistics.CountActive, Is.Zero);
            Assert.That(statistics.CountInactive, Is.EqualTo(1));

            Object.DestroyImmediate(scopeObject);
            Object.DestroyImmediate(prefab);
        }

        [Test]
        public void OwnershipSlot_ReplacementReturnsPreviousInstance()
        {
            var prefab = new GameObject("Prefab");
            var scopeObject = new GameObject("Scope");
            var scope = scopeObject.AddComponent<GameObjectPoolScope>();
            var handle = scope.Register(
                new UnityGameObjectPoolConfiguration(prefab),
                PoolLifetimeConfiguration.Scope);
            using var slot = new OwnershipSlot<GameObject>();
            var first = handle.SpawnOwned();
            var firstInstance = first.Value;

            slot.Set(first);
            slot.Set(handle.SpawnOwned());

            Assert.That(firstInstance.activeSelf, Is.False);
            Assert.That(first.IsValid, Is.False);

            slot.Dispose();
            Object.DestroyImmediate(scopeObject);
            Object.DestroyImmediate(prefab);
        }

        [Test]
        public void GeneratedSetter_ReplacesAndReturnsInstanceWhenOwnerIsDestroyed()
        {
            var prefab = new GameObject("Prefab");
            var scopeObject = new GameObject("Scope");
            var ownerObject = new GameObject("Owner");
            var owner = ownerObject.AddComponent<ManagedPoolOwner>();
            var handle = scopeObject.AddComponent<GameObjectPoolScope>().Register(
                new UnityGameObjectPoolConfiguration(prefab),
                PoolLifetimeConfiguration.Scope);
            var first = handle.SpawnOwned();
            var firstInstance = first.Value;

            owner.SetInstance(first);
            owner.SetInstance(handle.SpawnOwned());

            Assert.That(owner.Instance, Is.Not.Null);
            Assert.That(firstInstance.activeSelf, Is.False);
            Assert.That(first.IsValid, Is.False);

            Object.DestroyImmediate(ownerObject);
            Assert.That(handle.TryGetStatistics(out var statistics), Is.True);
            Assert.That(statistics.CountActive, Is.Zero);

            Object.DestroyImmediate(scopeObject);
            Object.DestroyImmediate(prefab);
        }

        [Test]
        public void GeneratedTake_TransfersLeaseWithoutReturningInstance()
        {
            var prefab = new GameObject("Prefab");
            var scopeObject = new GameObject("Scope");
            var ownerObject = new GameObject("Owner");
            var owner = ownerObject.AddComponent<ManagedPoolOwner>();
            var handle = scopeObject.AddComponent<GameObjectPoolScope>().Register(
                new UnityGameObjectPoolConfiguration(prefab),
                PoolLifetimeConfiguration.Scope);
            owner.SetInstance(handle.SpawnOwned());

            var transferred = owner.TakeInstance();
            Object.DestroyImmediate(ownerObject);

            Assert.That(transferred.IsValid, Is.True);
            Assert.That(transferred.Value.activeSelf, Is.True);
            transferred.Dispose();
            Assert.That(handle.TryGetStatistics(out var statistics), Is.True);
            Assert.That(statistics.CountActive, Is.Zero);

            Object.DestroyImmediate(scopeObject);
            Object.DestroyImmediate(prefab);
        }
    }
}
