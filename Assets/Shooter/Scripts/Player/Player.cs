using UnityEngine;
using System.Collections;
using GameFramework;

/// <summary>
/// Player.
/// Functionality for the player ship
/// </summary>
public class Player : Character
{
	public string _BulletMaskName = "Bullet";

	public override void Start()
	{
		base.Start ();

		mShooter.LevelChangedEvent += HandleLevelChangedEvent;
		mShooter.GameOverEvent += HandleGameOverEvent;
		int level = mShooter.CurrentLevel ();
		ActivateWeapon (level);
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.layer == LayerMask.NameToLayer(_BulletMaskName))
		{
			Bullet bullet = other.gameObject.GetComponent<Bullet>();
			PoolManager.AddObject (bullet._Weapon._BulletName, bullet.gameObject);

			mCurrentDamage += bullet._Weapon._Damage;
			CharacterLevelInfo levelInfo = _LevelInfo[mShooter.CurrentLevel() - 1];
			if(mCurrentDamage >= levelInfo._MaxDamage)
			{
				mShooter.LostLife();
				mCurrentDamage = 0;
			}
		}
	}

	void Update()
	{
		if (!mShooter.IsGameStarted () || mShooter.IsGamePaused())
			return;

		if(mCurrentWeapon != null && (Input.GetKeyUp(KeyCode.F) || Input.GetMouseButtonDown(0)))
		{
			Vector3 startPos = transform.position;
			startPos.y += 0.6f;
			mCurrentWeapon.Fire(startPos);
		}
	}
}
