using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public enum eSliderValueType
{
	Normal,
	Percent
}
public class SliderValueText : MonoBehaviour {
	public Slider pSlider;
	public eSliderValueType mValueType = eSliderValueType.Normal;
	private Text pText;
	// Use this for initialization
	void Start () {
		pText = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		switch (mValueType) {
		case eSliderValueType.Normal:
			pText.text = pSlider.value.ToString("f2");
			break;
		case eSliderValueType.Percent:
			pText.text = (int)(pSlider.value * 100)+"%";
			break;
		}
	}
}
