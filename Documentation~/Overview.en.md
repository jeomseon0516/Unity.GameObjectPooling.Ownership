# Jeomseon Unity GameObject Pooling Ownership

[한국어](Overview.md) · [Installation](../README.en.md)

## Purpose

Wraps spawned instances in leases; generated ManagedPooledObject setters return them to the original pool on replacement, Clear, or owner destruction.

## Usage flow

Annotate a GameObject/Component field in a partial Component, pass pool.SpawnOwned to Set, and use the getter. Take moves the return responsibility to the caller.

## Contracts and limitations

Do not return the same instance both with Despawn and lease Dispose. Pool shutdown can invalidate leases. With Preserve, assign final destruction responsibility for detached instances separately.

## Package requirements

- Package ID: `com.jeomseon.unity.game-object-pooling.ownership`
- Version: `0.1.0`
- Unity: `6000.6` / `0f1`
- `com.jeomseon.unity.game-object-pooling`: `0.4.0`
- `com.jeomseon.unity.ownership`: `0.1.0`

## Samples and verification

Import the following from Package Manager > Samples. Open the included Scene or Editor tool; follow any Setup step in its sample README.

- [Basic Usage](../Samples~/BasicUsage/README.md)
  - [PoolingOwnershipBasicUsage](../Samples~/BasicUsage/PoolingOwnershipBasicUsage.unity)

[Test sources](../Tests)

Add the package ID to manifest `testables`, then run the relevant Unity Test Runner assemblies. These links describe available tests, not a new successful test run.

## Source map

- [GameObjectPoolOwnershipExtensions](../Runtime/GameObjectPoolOwnershipExtensions.cs)
- [ManagedPooledObjectAttribute](../Runtime/ManagedPooledObjectAttribute.cs)
- [PooledGameObjectLease](../Runtime/PooledGameObjectLease.cs)
- [PooledInstanceLease](../Runtime/PooledInstanceLease.cs)
