using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GameFramework;
using GameFramework.UI;

/// <summary>
/// GameUI
/// In game HUD
/// </summary>
public class GameUI : UI 
{
	private UIText mItemLife;
	private UIText mItemScore;
	private UIText mHighscore;
	private UIButton mBtnPause;

	private IShooter mShooter;
	
	void Start()
	{
		mItemLife = (UIText) GetItem ("LblLivesValue");
		mItemScore = (UIText) GetItem ("LblScoreValue");
		mHighscore = (UIText) GetItem ("LblHighscoreValue");
		mBtnPause = (UIButton)GetItem ("BtnPause");

		mShooter = ServiceLocator.GetService<IShooter>();
		mShooter.GameOverEvent += HandleGameOverEvent;
		mShooter.StartGameEvent += HandleStartGameEvent;
		mShooter.PauseGameEvent += HandlePauseGameEvent;
		mShooter.LivesUpdatedEvent += HandleLivesUpdatedEvent;
		mShooter.HighscoreUpdatedEvent += HandleHighscoreUpdatedEvent;
		mShooter.PointUpdatedEvent += HandlePointUpdatedEvent;
	}

	void HandlePointUpdatedEvent (int score)
	{
		SetScore (score);
	}

	void HandleHighscoreUpdatedEvent (int highscore)
	{
		SetHighscore (highscore);
	}

	void HandleLivesUpdatedEvent (int lives)
	{
		SetLives (lives);
	}

	void HandlePauseGameEvent (bool pause)
	{
		if (pause)
			SetVisiblity (false);
		else
			SetVisiblity (true);
	}

	void HandleStartGameEvent ()
	{
		SetVisiblity (true);
	}

	void HandleGameOverEvent ()
	{
		SetVisiblity (false);
	}

	public void SetLives(int lives)
	{
		mItemLife.SetText (lives.ToString ());
	}

	public void SetScore(int score)
	{
		mItemScore.SetText (score.ToString ());
	}

	public void SetHighscore(int highscore)
	{
		mHighscore.SetText (highscore.ToString ());
	}

	public override void OnClick(Selectable item)
	{
		if(item.name.Equals(mBtnPause.name))
		{
			mShooter.PauseGame();
		}
	}
}
