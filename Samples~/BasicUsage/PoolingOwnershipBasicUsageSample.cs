using Jeomseon.Unity.GameObjectPooling.Configurations;
using Jeomseon.Unity.GameObjectPooling.Scopes;
using UnityEngine;

namespace Jeomseon.Unity.GameObjectPooling.Ownership.Samples.BasicUsage
{
    public partial class PoolingOwnershipBasicUsageSample : MonoBehaviour
    {
        [ManagedPooledObject] private GameObject _spawned;
        private GameObject _prefab;
        private GameObjectPoolScope _scope;
        private Handles.GameObjectPoolHandle _pool;
        private int _spawned;

        private void Start()
        {
            _prefab = GameObject.CreatePrimitive(PrimitiveType.Cube);
            _prefab.name = "Runtime Pool Prefab";
            _prefab.SetActive(false);
            _scope = new GameObject("Runtime Pool Scope").AddComponent<GameObjectPoolScope>();
            _pool = _scope.Register(new UnityGameObjectPoolConfiguration(_prefab), PoolLifetimeConfiguration.Scope);
            Replace();
        }

        private void Replace()
        {
            var lease = _pool.SpawnOwned();
            lease.Value.name = $"Owned Cube #{++_spawned}";
            lease.Value.transform.position = Vector3.right * ((_spawned % 5) - 2);
            SetSpawned(lease);
        }

        private void Clear() => ClearSpawned();

        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(20f, 20f, 480f, 170f), GUI.skin.box);
            GUILayout.Label("GameObject Pooling Ownership — Basic Usage");
            GUILayout.Label($"Current: {(Spawned != null ? Spawned.name : "<returned>")}");
            if (GUILayout.Button("Replace (previous cube returns to pool)")) Replace();
            if (GUILayout.Button("Clear (current cube returns to pool)")) Clear();
            GUILayout.Label("Delete this owner to verify automatic lifetime-host return.");
            GUILayout.EndArea();
        }
    }
}
