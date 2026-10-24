# Jeomseon Unity GameObject Pooling Ownership

한국어 | [English](README.en.md)

Spawn 결과를 lease로 묶고 ManagedPooledObject 생성 setter가 교체·Clear·owner 파괴 때 원래 풀에 반환합니다.

## 빠르게 시작하기

partial Component의 GameObject/Component 필드에 ManagedPooledObject 선언 → pool.SpawnOwned 결과를 Set에 전달 → getter로 사용 → 자동 반환. Take는 반환 책임을 호출자로 이동합니다.

전체 샘플 코드: [PoolingOwnershipBasicUsageSample.cs](Samples~/BasicUsage/PoolingOwnershipBasicUsageSample.cs).

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

## 계약과 주의사항

같은 인스턴스를 별도 Despawn과 lease Dispose로 이중 반환하지 마세요. 풀을 먼저 종료한 경우 lease가 무효가 될 수 있습니다. 풀 Preserve 정책을 사용했다면 분리된 객체의 최종 파괴 책임을 별도로 정해야 합니다.

## 요구 사항

- Unity `6000.6.0f1`
- 패키지: `com.jeomseon.unity.game-object-pooling.ownership` `0.1.0`
- `com.jeomseon.unity.game-object-pooling`: `0.4.0`
- `com.jeomseon.unity.ownership`: `0.1.0`

## 설치

프로젝트 `Packages/manifest.json`의 기존 dependencies와 registry를 유지하면서 다음 항목을 병합합니다.

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

또는 Package Manager의 **Install package from git URL**을 사용합니다:

```text
https://github.com/jeomseon0516/Unity.GameObjectPooling.Ownership.git#v0.1.0
```

## 샘플 실행

**Package Manager > Samples**에서 예제를 Import합니다. 포함된 Scene 또는 Editor 도구를 열고 필요한 Setup 메뉴를 먼저 실행합니다.

- [Basic Usage](Samples~/BasicUsage/README.md)
  - [PoolingOwnershipBasicUsage](Samples~/BasicUsage/PoolingOwnershipBasicUsage.unity)

## 테스트

[테스트 코드](Tests)

manifest의 `testables`에 패키지 ID를 추가하고 Unity Test Runner의 관련 EditMode/PlayMode 어셈블리를 실행합니다.

## 추가 문서

[전체 코드 지도와 패키지 구성](Documentation~/Overview.md)
- [Changelog](CHANGELOG.md)

## 라이선스

[License](LICENSE.md)
