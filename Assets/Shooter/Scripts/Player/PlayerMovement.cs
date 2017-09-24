using UnityEngine;
using System.Collections;
using GameFramework;

/// <summary>
/// Player movement.
/// Manages player movement using Mouse or Keyboard
/// </summary>
public class PlayerMovement : BaseMonoBehavior 
{
	public float _Speed;
	public bool _ControlUsingMouse = true;
	private IShooter mShooter;

	void Start()
	{
		mShooter = ServiceLocator.GetService<IShooter>();
	}
	
	void Update()
	{
		if (mShooter.IsGamePaused ())
			return;

		// Keyboard
		if (!_ControlUsingMouse) 
		{
			if (Input.GetKey (KeyCode.RightArrow)) 
			{
				Vector3 pos = transform.position;
				pos += Vector3.right * _Speed * Time.deltaTime;

				if (!IsOutsideScreen (pos))
					transform.position = pos;
			} 
			else if (Input.GetKey (KeyCode.LeftArrow)) 
			{
				Vector3 pos = transform.position;
				pos -= Vector3.right * _Speed * Time.deltaTime;

				if (!IsOutsideScreen (pos))
					transform.position = pos;
			}
		}
		else // Mouse
		{
			float val = Input.mousePosition.x;
			Vector3 worldPos = Camera.main.ScreenToWorldPoint (new Vector3 (val, 0, 0));
			worldPos.z = 0;
			worldPos.y += 0.6f;
			transform.position = worldPos;
		}
	}
}
