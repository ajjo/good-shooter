using UnityEngine;
using System.Collections;
using GameFramework;

/// <summary>
/// CharacterLevelInfo
/// Character level info for each level of game play
/// </summary>
[System.Serializable]
public class CharacterLevelInfo
{
	public float _MaxDamage;
	public Color _LevelColor;
	public float _Speed;
}

/// <summary>
/// Character.
/// Base class for the Player and Enemy ship
/// </summary>
public class Character : BaseMonoBehavior 
{
	public CharacterLevelInfo[] _LevelInfo;

	protected Weapon [] mWeapons;
	protected Weapon mCurrentWeapon = null;
	protected IShooter mShooter;
	protected int mCurrentDamage = 0;

	public virtual void Start()
	{
		mWeapons = GetComponents<Weapon> ();

		mShooter = ServiceLocator.GetService<IShooter>();
		mShooter.LevelChangedEvent += HandleLevelChangedEvent;
		mShooter.GameOverEvent += HandleGameOverEvent;
	}

	public virtual void HandleGameOverEvent ()
	{
		mCurrentDamage = 0;
		mCurrentWeapon = null;
		ActivateWeapon (-1);
	}
	
	public virtual void HandleLevelChangedEvent (int newLevel)
	{
		ActivateWeapon (newLevel);
	}

	protected void ActivateWeapon(int level)
	{
		foreach(Weapon weapon in mWeapons)
		{
			if(weapon._Level == level)
			{
				weapon.enabled = true;
				mCurrentWeapon = weapon;

				if(_LevelInfo.Length > 0)
				{
					Renderer [] renderers = GetComponentsInChildren<Renderer>();
					foreach(Renderer r in renderers)
					{
						Material mat = r.material;
						mat.color = _LevelInfo[level - 1]._LevelColor;
						r.material = mat;
					}
				}
			}
			else
				weapon.enabled = false;
		}
	}
}
