using UnityEngine;
using System.Collections;

namespace GameFramework
{
	/// <summary>
	/// BaseMonoBehavior
	/// Extend MonoBehaviour to cache variables and add base helper functions
	/// </summary>
	public class BaseMonoBehavior : MonoBehaviour 
	{
		// Caching Unity's variables to avoid GetComponent calls
		[System.NonSerialized]
		public new Transform transform;

		public virtual void Awake()
		{
			transform = GetComponent<Transform> ();
		}

		public bool IsOutsideScreen(Vector3 currentPosition)
		{
			Vector3 screenPoint = Camera.main.WorldToScreenPoint (currentPosition);
			return (screenPoint.y < 0 || screenPoint.y > Screen.height || screenPoint.x < 0 || screenPoint.x > Screen.width);
		}

		public bool IsOutsideBottomScreen(Vector3 currentPosition)
		{
			Vector3 screenPoint = Camera.main.WorldToScreenPoint (currentPosition);
			return (screenPoint.y + 50 < 0); // 10 is buffer, ideally it should be the object bound
		}
	}
}
