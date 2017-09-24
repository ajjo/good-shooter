using UnityEngine;
using System.Collections;
using GameFramework;

/// <summary>
/// Character movement.
/// Translate the gameObject along X and Y for a given duration
/// </summary>
public class CharacterMovement : BaseMonoBehavior
{
	public float _DeltaX;
	public float _DeltaY;
	public float _Duration;

	private Vector3 mTranslateTo;
	private Vector3 mOriginalPosition;
	private float mTime;
	private Transform mParentObject;

	private MinMax mXOffset = new MinMax();
	private MinMax mYOffset = new MinMax();

	void OnEnable()
	{
		mOriginalPosition = transform.localPosition;
		
		mXOffset._Min = mOriginalPosition.x - _DeltaX;	
		mXOffset._Max = mOriginalPosition.x + _DeltaX;

		mYOffset._Min = mOriginalPosition.y - _DeltaY;
		mYOffset._Max = mOriginalPosition.y + _DeltaY;
		
		SetTranslateToPosition ();
	}
	
	private void SetTranslateToPosition()
	{
		float randDeltaX = Random.Range (_DeltaX, -_DeltaX);
		float randDeltaY = Random.Range (_DeltaY, -_DeltaY);

		float newX = mOriginalPosition.x + randDeltaX;
		newX = mXOffset.Clamp (newX);

		float newY = mOriginalPosition.y + randDeltaY;
		newY = mYOffset.Clamp (newY);

		mTranslateTo = new Vector3 (newX, newY, mOriginalPosition.z);
		mTime = 0.0f;
	}

	public void Update()
	{
		mTime += Time.deltaTime;
		mTime /= _Duration;

		transform.localPosition = Vector3.Lerp (mOriginalPosition, mTranslateTo, mTime);

		if (mTime >= _Duration) 
		{
			mOriginalPosition = mTranslateTo;
			SetTranslateToPosition ();
		}
	}
	
	void OnDisable()
	{
		mTime = 0.0f;
		mTranslateTo = Vector3.zero;
	}
}
