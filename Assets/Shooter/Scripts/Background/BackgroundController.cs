using UnityEngine;
using System.Collections;
using GameFramework;

/// <summary>
/// Background controller.
/// Prepares the screens for scrolling
/// </summary>
public class BackgroundController : BaseMonoBehavior
{
	public GameObject[] _Screens;

	public override void Awake()
	{
		base.Awake ();

		Vector3 pos = Vector3.zero;

		foreach(GameObject go in _Screens)
		{
			go.transform.localPosition = pos;
			pos.y += Screen.height;
		}
	}
}
