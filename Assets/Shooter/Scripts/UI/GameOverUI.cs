using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GameFramework;
using GameFramework.UI;

/// <summary>
/// GameOverUI
/// Game Over HUD
/// </summary>
public class GameOverUI : UI 
{
	private UIButton mPlayAgain;

	private IShooter mShooter;

	void Start()
	{
		mPlayAgain = (UIButton) GetItem ("BtnPlayAgain");

		mShooter = ServiceLocator.GetService<IShooter>();
		mShooter.GameOverEvent += HandleGameOverEvent;
		mShooter.StartGameEvent += HandleStartGameEvent;
	}

	void HandleStartGameEvent ()
	{
		SetVisiblity (false);
	}

	void HandleGameOverEvent ()
	{
		SetVisiblity (true);
	}

	public override void OnClick(Selectable item)
	{
		if(item.name.Equals(mPlayAgain.name))
		{
			mShooter.RestartGame();
		}
	}
}
