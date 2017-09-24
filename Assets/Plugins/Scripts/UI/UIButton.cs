using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace GameFramework.UI
{
	/// <summary>
	/// UIButton
	/// UI Button implementation
	/// </summary>
	public class UIButton : UIItem 
	{
		public LocaleString _LocaleText;
		private Text mText;

		public override void Awake()
		{
			base.Awake ();

			mText = GetComponentInChildren<Text> ();

			Button button = GetComponent<Button> ();
			button.interactable = _IsInteractive;
		}

		public virtual void Start()
		{
			if(!string.IsNullOrEmpty(_LocaleText._Text))
				mText.text = _LocaleText.GetLocalisedString ();
		}
		
		public override void SetVisibility (bool visibility)
		{

		}

		public void SetText(string newText)
		{
			mText.text = newText;
		}
	}
}
