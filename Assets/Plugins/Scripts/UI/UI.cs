using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace GameFramework.UI
{
	/// <summary>
	/// UI
	/// Base UI for all UIs in the game. Contains only the methods required for this game.
	/// </summary>
	public class UI : BaseMonoBehavior 
	{
		public bool _Visible;

		private Canvas mCanvas;
		private List<UIItem> mItems;
		
		public override void Awake()
		{
			base.Awake();

			mCanvas = GetComponent<Canvas> ();

			UIItem [] items = GetComponentsInChildren<UIItem> ();
			mItems = new List<UIItem>(items);

			foreach(UIItem item in items)
			{
				if(item is UIButton)
				{
					Button button = item.GetComponent<Button> ();
					button.onClick.AddListener(() => { OnClick(button); });
				}
			}

			if (!_Visible)
				SetVisiblity (false);
		}

		public virtual void SetVisiblity(bool visibility)
		{
			mCanvas.enabled = visibility;
		}
		
		public bool IsVisible()
		{
			return mCanvas.enabled;
		}

		public UIItem GetItem(string itemName)
		{
			foreach (UIItem item in mItems) 
			{
				if(item.HasName(itemName))
					return item;
			}

			return null;
		}

		public virtual void OnClick(Selectable item)
		{

		}
	}
}
