using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace GameFramework
{
	public class Utilities 
	{
		public static float ABS(float value)
		{
			if (value < 0)
				return Mathf.Abs (value);

			return value;
		}
	}

	/// <summary>
	/// MinMax
	/// Set the Min and Max of a type.
	/// </summary>
	public class MinMax
	{
		public float _Min;
		public float _Max;
		
		public float Clamp(float value)
		{
			return Mathf.Clamp (value, _Min, _Max);
		}
	}

	/// <summary>
	/// Service locator.
	/// Implementation of the Service Locator Pattern - IShooter is the only one for now
	/// </summary>
	public class ServiceLocator
	{
		private static Dictionary<System.Type, object> mServices = new Dictionary<System.Type, object> ();
		
		public static void AddService(System.Type type, object obj)
		{
			mServices.Add (type, obj);
		}
		
		public static T GetService<T>()
		{
			try
			{
				return (T)mServices[typeof(T)];
			}
			catch(Exception e)
			{
				Debug.Log (e.Message);
				throw new Exception(e.Message);
			}
		}
	}
}
