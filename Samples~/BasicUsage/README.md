# GameObject Pooling Ownership Basic Usage

Open `PoolingOwnershipBasicUsage.unity` and enter Play Mode. Replacing the generated `Spawned` property returns
the previous cube to its original pool. Clearing it returns the current cube. Deleting the owner demonstrates
automatic return through `OwnershipLifetimeHost`.
