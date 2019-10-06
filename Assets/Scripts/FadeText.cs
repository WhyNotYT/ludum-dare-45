using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeText : MonoBehaviour
{

	public float duration = 1;
	public TMPro.TMP_Text text;
	private float startTime;
	public Color FinalColor;
	private Color targetColor;
	void Start()
    {
		//text = this.GetComponent<TMPro.TMP_Text>();
		startTime = Time.time;
		targetColor = FinalColor;
	}



	private void Update()
	{
		if ((1 - ((Time.time - startTime) / duration) < 0.4f))
		{
			targetColor.a = (1 - ((Time.time - startTime + (0.4f * duration)) / duration));
			text.color = targetColor;
		}
		if(Time.time - startTime > duration)
		{
			Destroy(this.gameObject);
		}
	}


	public void setText(string displayText)
	{
		text.text = displayText;
	}
}
