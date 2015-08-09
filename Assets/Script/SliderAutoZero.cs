using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SliderAutoZero : MonoBehaviour {
	private Slider mSlider;
	public bool fZero=false;//归零标志
	public float ZeroSpeed = 0.01f;
	// Use this for initialization
	void Start () {
		mSlider = GetComponent<Slider> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(fZero)
			Zero ();
	}
	void Zero()
	{
		if (mSlider.value == 0) {
			fZero = false;
			return;
		}
		if (mSlider.value > 0) {
			mSlider.value -= ZeroSpeed;
			if(mSlider.value<0)
			{
				mSlider.value = 0;
				fZero = false;
			}
		}
		else if (mSlider.value < 0) {
			mSlider.value += ZeroSpeed;
			if(mSlider.value>0)
			{
				mSlider.value = 0;
				fZero = false;
			}
		}
	}
}
