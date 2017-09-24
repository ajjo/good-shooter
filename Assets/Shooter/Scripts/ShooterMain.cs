using UnityEngine;
using GameFramework;

/// <summary>
/// IShooter
/// The interface for manager
/// </summary>
public interface IShooter
{
	int CurrentLevel();
	void LostLife();
	void AddPoint(int point);
	void StartGame();
	void RestartGame ();
	void PauseGame();
	void ResumeGame();
	bool IsGamePaused();
	bool IsGameStarted ();
	bool IsGameOver();
	
	// This could also be replaced with a state machine too...
	event System.Action<int> LevelChangedEvent;
	event System.Action GameOverEvent;
	event System.Action StartGameEvent;
	event System.Action<bool> PauseGameEvent;
	event System.Action<int> HighscoreUpdatedEvent;
	event System.Action<int> LivesUpdatedEvent;
	event System.Action<int> PointUpdatedEvent;
}

/// <summary>
/// Shooter main.
/// The main controller class. Implements IShooter
/// </summary>
public class ShooterMain : BaseMonoBehavior, IShooter
{
	public TextAsset _LocaleTextAsset;

	// The duration after which the game moves to the next level
	public int _LevelIncrementInterval;
	public int _MaxLevels;
	public int _MaxLives = 3;
	public string _HighScoreKey = "GS_HS"; //GS_HS = GoodShooter Highscore

	private float mTime = 0.0f;
	private int mCurrentLevel = 0;
	private int mLives = 0;
	private int mTotalPoint = 0;

	private bool mInit = false;
	private bool mIsPaused = false;
	private bool mIsGameOver = false;
	private bool mIntroDone = false;

	public event System.Action<int> LevelChangedEvent;
	public event System.Action GameOverEvent;
	public event System.Action StartGameEvent;
	public event System.Action<bool> PauseGameEvent;
	public event System.Action<int> HighscoreUpdatedEvent;
	public event System.Action<int> LivesUpdatedEvent;
	public event System.Action<int> PointUpdatedEvent;

	public override void Awake ()
	{
		base.Awake ();

		LocaleData.Initialize (_LocaleTextAsset);
		// Add to the service locator
		ServiceLocator.AddService (typeof(IShooter), this);
	}

	// Use this for initialization
	void Start () 
	{
		mCurrentLevel = 1;
		mLives = _MaxLives;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Wait for the intro to be done
		if (!mIntroDone)
			return;

		if (!mInit) 
		{
			int highscore = 0;
			if(PlayerPrefs.HasKey(_HighScoreKey))
				highscore = PlayerPrefs.GetInt(_HighScoreKey);

			HighscoreUpdatedEvent(highscore);
			LivesUpdatedEvent(mLives);
			mInit = true;
		}

		mTime += Time.deltaTime;

		if (mTime > _LevelIncrementInterval) 
		{
			mTime = 0.0f;
			mCurrentLevel++;
			if(mCurrentLevel > _MaxLevels) // Reset back to first level
				mCurrentLevel = 1;

			LevelChangedEvent(mCurrentLevel);
		}

		if (!mIsGameOver && Input.GetKeyUp (KeyCode.P)) 
		{
			if(mIsPaused)
				ResumeGame();
			else
				PauseGame();
		}

		// Cheat to trigger the game over HUD
		/*
		if(Input.GetKeyUp(KeyCode.O))
		{
			mLives = 1;
			LostLife();
		}*/
	}
	
	void OnDestroy()
	{
		LevelChangedEvent = null;
		GameOverEvent = null;
		StartGameEvent = null;
		PauseGameEvent = null;
		HighscoreUpdatedEvent = null;
		LivesUpdatedEvent = null;
		PointUpdatedEvent = null;
	}

	public int CurrentLevel()
	{
		return mCurrentLevel;
	}

	public void LostLife()
	{
		mLives = Mathf.Clamp (mLives - 1, 0, _MaxLives);
		LivesUpdatedEvent (mLives);

		if(mLives == 0)
		{
			SaveHighscores();
			mIsGameOver = true;
			GameOverEvent();
		}
	}

	private void SaveHighscores()
	{
		if(PlayerPrefs.HasKey(_HighScoreKey))
		{
			int highscore = PlayerPrefs.GetInt(_HighScoreKey);
			if(mTotalPoint > highscore)
			{
				HighscoreUpdatedEvent(mTotalPoint);
				PlayerPrefs.SetInt(_HighScoreKey, mTotalPoint);
				PlayerPrefs.Save();	
			}
		}
		else
		{
			HighscoreUpdatedEvent(mTotalPoint);
			PlayerPrefs.SetInt(_HighScoreKey, mTotalPoint);
			PlayerPrefs.Save();
		}
	}

	public void AddPoint(int point)
	{
		mTotalPoint += point;
		PointUpdatedEvent (mTotalPoint);
	}

	public void RestartGame()
	{
		mCurrentLevel = 1;
		mLives = _MaxLives;
		mIsPaused = false;
		mIsGameOver = false;
		mTotalPoint = 0;
		mTime = 0.0f;

		LivesUpdatedEvent (mLives);
		LevelChangedEvent (mCurrentLevel);
		PointUpdatedEvent (mTotalPoint);

		StartGameEvent ();
	}

	public bool IsGamePaused()
	{
		return mIsPaused;
	}

	public void PauseGame()
	{
		mIsPaused = true;
		PauseGameEvent (true);
		Time.timeScale = 0;
	}

	public void ResumeGame()
	{
		mIsPaused = false;
		PauseGameEvent (false);
		Time.timeScale = 1;
	}

	public void StartGame()
	{
		mIntroDone = true;
		RestartGame ();
	}

	public bool IsGameStarted()
	{
		return mIntroDone;
	}

	public bool IsGameOver()
	{
		return mIsGameOver;
	}
}
