using UnityEngine;
using System.Collections;
using GameFramework;

/// <summary>
/// Enemy 
/// Functionality for the enemy ships
/// </summary>
public class Enemy : Character
{
	public string _BulletMaskName = "Bullet";
	public string _PlayerMaskName = "Player";
	public string _PoolKeyName = "Enemy";
	private CharacterMovement mMovement;
	private float mTime = 0.0f;

	public override void Start()
	{
		base.Start ();

		mMovement = GetComponentInChildren<CharacterMovement> ();

		mShooter.LevelChangedEvent += HandleLevelChangedEvent;
		mShooter.GameOverEvent += HandleGameOverEvent;

		int level = mShooter.CurrentLevel ();
		ActivateWeapon (level);
	}

	void OnEnable()
	{
		if(mShooter != null)
		{
			int level = mShooter.CurrentLevel ();
			ActivateWeapon (level);
		}
	}

	void OnDisable()
	{
		mTime = 0.0f;
		mCurrentWeapon = null;
		transform.position = Vector3.zero;
	}

	public override void HandleGameOverEvent ()
	{
		base.HandleGameOverEvent ();

		mTime = 0.0f;
		PoolManager.AddObject (_PoolKeyName, gameObject);
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.layer == LayerMask.NameToLayer(_BulletMaskName))
		{
			Bullet bullet = other.gameObject.GetComponent<Bullet>();

			if(bullet._Weapon.gameObject.layer == LayerMask.NameToLayer(_PlayerMaskName))
			{
				PoolManager.AddObject (bullet._Weapon._BulletName, bullet.gameObject);

				mCurrentDamage += bullet._Weapon._Damage;

				if(mCurrentDamage >= _LevelInfo[mShooter.CurrentLevel() - 1]._MaxDamage)
				{
					PoolManager.AddObject (_PoolKeyName, gameObject);
					mCurrentDamage = 0;
				}

				mShooter.AddPoint(bullet._Weapon._Point);
			}
		}
	}

	void Update()
	{
		if(mCurrentWeapon != null)
		{
			mTime += Time.deltaTime;

			if (mTime >= mCurrentWeapon._CoolDownTime) 
			{
				mTime = 0.0f;
				Vector3 startPos = mMovement.transform.position;
				startPos.y -= 0.6f;

				mCurrentWeapon.Fire(startPos);
			}
		}

		CharacterLevelInfo currentLevel = _LevelInfo [mShooter.CurrentLevel () - 1];
		Vector3 newPos = transform.position;
		newPos.y -= (currentLevel._Speed * Time.deltaTime);
		transform.position = newPos;
		
		if (IsOutsideBottomScreen (newPos))
			PoolManager.AddObject (_PoolKeyName, gameObject);
	}
}
