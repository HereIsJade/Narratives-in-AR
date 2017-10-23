using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// © 2017 TheFlyingKeyboard
// theflyingkeyboard.net
public class TextScroll : MonoBehaviour {
	[SerializeField] private Text textToUse;
	[SerializeField] private bool useThisText = false;
	[TextAreaAttribute(4, 15)]
	[SerializeField] private string textToShow;
	[SerializeField] private bool useTextText = false;
	[SerializeField] private float fadeSpeedMultiplier = 0.25f;
	[SerializeField] private bool fade;
	private float colorFloat = 0.1f;
	private int colorInt;
	private int wordCounter = 0;
	private string shownText;
	private string[] words;
	private void Start()
	{
		if (useThisText)
		{
			textToUse = GetComponent<Text>();
		}
		if (useTextText)
		{
			textToShow = textToUse.text;
		}
		words = textToShow.Split(' ');
		if (fade)
		{
			Fade();
		}
	}
	private IEnumerator FadeInText()
	{
		while (wordCounter < words.Length)
		{
			if (colorFloat < 1.0f)
			{
				colorFloat += Time.deltaTime * fadeSpeedMultiplier;
				colorInt = (int)(Mathf.Lerp(0.0f, 1.0f, colorFloat) * 255.0f);
				textToUse.text = shownText + "<color=\"#FFFFFF" + string.Format("{0:X}", colorInt) + "\">" + words[wordCounter] + "</color>";
			}
			else
			{
				colorFloat = 0.1f;
				shownText += words[wordCounter] + ' ';
				wordCounter++;
			}
			yield return null;
		}
	}
	public void Fade()
	{
		StartCoroutine(FadeInText());
	}
}