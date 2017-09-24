using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GameFramework;
using GameFramework.UI;

/// <summary>
/// PauseUI
/// Pause HUD
/// </summary>
public class PauseUI : UI 
{
	private UIButton mBtnResume;
	private UIButton mBtnQuit;
	private IShooter mShooter;

	void Start()
	{
		mBtnResume = (UIButton)GetItem ("BtnResume");
		mBtnQuit = (UIButton)GetItem ("BtnQuit");

		mShooter = ServiceLocator.GetService<IShooter>();
		mShooter.PauseGameEvent += HandlePauseGameEvent;
	}

	void HandlePauseGameEvent (bool pause)
	{
		if (pause)
			SetVisiblity (true);
		else
			SetVisiblity (false);
	}

	public override void OnClick(Selectable item)
	{
		if(item.name.Equals(mBtnResume.name))
		{
			mShooter.ResumeGame();
		}
		else if(item.name.Equals(mBtnQuit.name))
		{
			if(Application.isEditor)
				Debug.Log ("Nothing happens in the editor");
			else
				Application.Quit();
		}
	}
}
