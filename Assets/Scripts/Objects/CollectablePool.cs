using System.Collections;
using UnityEngine;
using Zenject;
public class CollectablePool : MonoMemoryPool<Collectable>
{
	private int _rotationCoefficient = 0;
	protected override void OnCreated(Collectable item)
	{
		base.OnCreated(item);
		item.Construct();
		item.transform.localRotation = Quaternion.Euler(0, 0, 30 - _rotationCoefficient);
		_rotationCoefficient += 15;
	}

	protected override void OnSpawned(Collectable item)
	{
		base.OnSpawned(item);
		item.Initialize();
	}

	protected override void OnDespawned(Collectable item)
	{
		base.OnDespawned(item);
		item.Reset();
	}

}
