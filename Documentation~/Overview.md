# Jeomseon Unity GameObject Pooling Ownership

[English](Overview.en.md) · [설치](../README.md)

## 목적

Spawn 결과를 lease로 묶고 ManagedPooledObject 생성 setter가 교체·Clear·owner 파괴 때 원래 풀에 반환합니다.

## 사용 흐름

partial Component의 GameObject/Component 필드에 ManagedPooledObject 선언 → pool.SpawnOwned 결과를 Set에 전달 → getter로 사용 → 자동 반환. Take는 반환 책임을 호출자로 이동합니다.

## 계약과 제한

같은 인스턴스를 별도 Despawn과 lease Dispose로 이중 반환하지 마세요. 풀을 먼저 종료한 경우 lease가 무효가 될 수 있습니다. 풀 Preserve 정책을 사용했다면 분리된 객체의 최종 파괴 책임을 별도로 정해야 합니다.

## 패키지 요구 사항

- Package ID: `com.jeomseon.unity.game-object-pooling.ownership`
- Version: `0.1.0`
- Unity: `6000.6` / `0f1`
- `com.jeomseon.unity.game-object-pooling`: `0.4.0`
- `com.jeomseon.unity.ownership`: `0.1.0`

## 샘플과 검증

Package Manager > Samples에서 다음 예제를 Import하세요. 포함된 Scene 또는 Editor 도구를 열고 샘플 README의 Setup 절차를 먼저 수행합니다.

- [Basic Usage](../Samples~/BasicUsage/README.md)
  - [PoolingOwnershipBasicUsage](../Samples~/BasicUsage/PoolingOwnershipBasicUsage.unity)

[테스트 코드](../Tests)

소비 프로젝트 manifest의 `testables`에 패키지 ID를 추가하고 Unity Test Runner의 관련 어셈블리를 실행합니다. 위 링크는 테스트 위치이며 이번 문서 수정으로 새 테스트 실행을 주장하지 않습니다.

## 코드 지도

- [GameObjectPoolOwnershipExtensions](../Runtime/GameObjectPoolOwnershipExtensions.cs)
- [ManagedPooledObjectAttribute](../Runtime/ManagedPooledObjectAttribute.cs)
- [PooledGameObjectLease](../Runtime/PooledGameObjectLease.cs)
- [PooledInstanceLease](../Runtime/PooledInstanceLease.cs)
