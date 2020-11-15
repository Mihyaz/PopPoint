using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class EnemyPool : MonoMemoryPool<Enemy>
{
	public List<Enemy> Enemies;
	protected override void OnCreated(Enemy item)
	{
		base.OnCreated(item);
		item.Construct();
	}

	protected override void OnSpawned(Enemy item)
	{
		base.OnSpawned(item);
		item.Initialize();
	}

	protected override void OnDespawned(Enemy item)
	{
		base.OnDespawned(item);
		item.Reset();
	}

	protected override void OnDestroyed(Enemy item)
	{
		base.OnDestroyed(item);
	}
}
