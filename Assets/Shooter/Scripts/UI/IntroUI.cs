using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GameFramework;
using GameFramework.UI;

/// <summary>
/// IntroUI
/// Intro HUD
/// </summary>
public class IntroUI : UI 
{
	private UIButton mBtnStart;

	// Use this for initialization
	void Start () 
	{
		mBtnStart = (UIButton)GetItem ("BtnStart");
	}

	public override void OnClick(Selectable item)
	{
		if(item.name.Equals(mBtnStart.name))
		{
			IShooter shooter = ServiceLocator.GetService<IShooter>();
			shooter.StartGame();
			SetVisiblity(false);
		}
	}
}
