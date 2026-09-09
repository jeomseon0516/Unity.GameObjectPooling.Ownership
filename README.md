# Jeomseon Unity GameObject Pooling Ownership

원본 `GameObject` 또는 `Component` 필드에 `[ManagedPooledObject]`를 붙이고 `SpawnOwned` 결과를
생성된 setter에 전달합니다. 생성 코드는 기존 `PooledGameObjectLease` 또는 `PooledInstanceLease<T>`를
직접 보관하며, 교체·clear·owner 파괴 시 원래 `GameObjectPoolHandle`로 자동 반환합니다.
생성된 `Take{Name}()`은 풀에 반환하지 않고 Lease ownership을 호출자에게 이동합니다.
