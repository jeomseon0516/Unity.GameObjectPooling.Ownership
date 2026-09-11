# Jeomseon Unity GameObject Pooling Ownership

`SpawnOwned` returns a `PooledGameObjectLease` or `PooledInstanceLease<T>` that returns its instance to the
original `GameObjectPoolHandle` when disposed. Mark a raw `GameObject` or `Component` field with
`[ManagedPooledObject]` to receive a
generated value property and exchange setter. Replacement and owner destruction return the previous instance
automatically.

The generated value property is borrowed. The generated code stores the existing lease directly.
Generated `Take{Name}()` transfers that lease without returning the instance to its pool.
