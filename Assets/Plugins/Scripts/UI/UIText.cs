using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace GameFramework.UI
{
	/// <summary>
	/// UIText
	/// UI Text implementation
	/// </summary>
	public class UIText : UIItem 
	{
		public LocaleString _LocaleText;
		private Text mText;

		public override void Awake()
		{
			base.Awake ();

			mText = GetComponent<Text> ();
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
