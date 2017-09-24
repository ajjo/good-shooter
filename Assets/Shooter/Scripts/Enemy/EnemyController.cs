using UnityEngine;
using System.Collections;
using GameFramework;

/// <summary>
/// EnemyControllerLevelInfo
/// Enemy controller info for each level of game play
/// </summary>
[System.Serializable]
public class EnemyControllerLevelInfo
{
	public float _SpawnDuration;
}

/// <summary>
/// Enemy controller.
/// Decides the spawning of the enemy ships
/// </summary>
public class EnemyController : BaseMonoBehavior
{
	public string _EnemyName = "Enemy";
	public EnemyControllerLevelInfo[] _LevelInfo;

	private IShooter mShooter;

	private float mTime = 0.0f;

	void Start()
	{
		mShooter = ServiceLocator.GetService<IShooter>();
	}

	void Update()
	{
		if (!mShooter.IsGameStarted () || mShooter.IsGameOver())
			return;

		mTime += Time.deltaTime;

		float duration = _LevelInfo [mShooter.CurrentLevel () - 1]._SpawnDuration;

		if(mTime >= duration)
		{
			mTime = 0.0f;
			GameObject obj = PoolManager.GetObject(_EnemyName);
			obj.transform.parent = transform;

			float xPos = Random.Range(20,Screen.width-20);
			float yPos = Random.Range(Screen.height + 50, Screen.height + 100);
			Vector3 screenPos = new Vector3(xPos, yPos, 0.0f);

			Vector3 worldsPos = Camera.main.ScreenToWorldPoint(screenPos);
			worldsPos.z = 0;

			obj.transform.position = worldsPos;
			//obj.name = _EnemyName;
			obj.SetActive (true);
		}
	}
}
