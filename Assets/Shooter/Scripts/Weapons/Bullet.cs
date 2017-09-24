using UnityEngine;
using System.Collections;
using GameFramework;

/// <summary>
/// Bullet
/// Bullet fired from a weapon
/// </summary>
public class Bullet : BaseMonoBehavior
{
	[System.NonSerialized]
	public Weapon _Weapon;

	void Update()
	{
		Vector3 pos = transform.position;
		pos.y -= (_Weapon._Speed * Time.deltaTime) * _Weapon._Direction;
		transform.position = pos;
		
		if(IsOutsideScreen(pos))
			PoolManager.AddObject (_Weapon._BulletName, gameObject);
	}
}
