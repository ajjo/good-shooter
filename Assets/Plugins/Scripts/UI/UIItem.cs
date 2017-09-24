using UnityEngine;
using System.Collections;

namespace GameFramework.UI
{
	/// <summary>
	/// UIItem
	/// Base class for all items in the game - text, buttons etc..
	/// </summary>
	public class UIItem : BaseMonoBehavior 
	{
		public bool _IsInteractive = true;

		public string pName
		{
			get { return transform.name; }
			set { transform.name = value; }
		}
		
		public virtual void SetVisibility(bool visibility)
		{
			transform.gameObject.SetActive(visibility);
		}

		public bool HasName(string itemName)
		{
			return (pName.Equals (itemName));
		}
	}
}
