# Jeomseon Unity GameObject Pooling Ownership

[한국어](README.md) | English

Wraps spawned instances in leases; generated ManagedPooledObject setters return them to the original pool on replacement, Clear, or owner destruction.

## Quick start

Annotate a GameObject/Component field in a partial Component, pass pool.SpawnOwned to Set, and use the getter. Take moves the return responsibility to the caller.

Complete sample: [PoolingOwnershipBasicUsageSample.cs](Samples~/BasicUsage/PoolingOwnershipBasicUsageSample.cs).

```csharp
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
        private int _spawnCount;

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
            lease.Value.name = $"Owned Cube #{++_spawnCount}";
            lease.Value.transform.position = Vector3.right * ((_spawnCount % 5) - 2);
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
```

## Contracts and limitations

Do not return the same instance both with Despawn and lease Dispose. Pool shutdown can invalidate leases. With Preserve, assign final destruction responsibility for detached instances separately.

## Requirements

- Unity `6000.6.0f1`
- Package: `com.jeomseon.unity.game-object-pooling.ownership` `0.1.0`
- `com.jeomseon.unity.game-object-pooling`: `0.4.0`
- `com.jeomseon.unity.ownership`: `0.1.0`

## Installation

Merge this into your project's `Packages/manifest.json`; preserve existing dependencies and registries.

```json
{
  "scopedRegistries": [
    {
      "name": "OpenUPM",
      "url": "https://package.openupm.com",
      "scopes": [
        "com.jeomseon"
      ]
    }
  ],
  "dependencies": {
    "com.jeomseon.unity.game-object-pooling.ownership": "0.1.0"
  }
}
```

Or use **Install package from git URL** in Package Manager:

```text
https://github.com/jeomseon0516/Unity.GameObjectPooling.Ownership.git#v0.1.0
```

## Samples

Import the desired entry from **Package Manager > Samples**. Use the included Scene/Editor tool and complete its Setup step first, if required.

- [Basic Usage](Samples~/BasicUsage/README.md)
  - [PoolingOwnershipBasicUsage](Samples~/BasicUsage/PoolingOwnershipBasicUsage.unity)

## Tests

[Test sources](Tests)

Add the package ID to manifest `testables`, then execute its relevant EditMode/PlayMode assemblies in Unity Test Runner.

## Further reading

[Complete source map and package metadata](Documentation~/Overview.en.md)
- [Changelog](CHANGELOG.md)

## License

[License](LICENSE.md)
