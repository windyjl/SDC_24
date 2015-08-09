using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShineText : MonoBehaviour {
	public int ShineInterval = 2;
	private bool ShineFlag = true;
	private float ShineTimeCount = 0;
	private Text pText;
	private Vector3	PositionOrig;
	// Use this for initialization
	void Start () 
	{
		pText = GetComponent<Text>();
		PositionOrig = pText.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		ShineTimeCount += Time.deltaTime;
		if (ShineTimeCount > ShineInterval) {
			ShineTimeCount = 0;
			if(ShineFlag)
			{
				ShineFlag = false;
				ShineTimeCount += Time.deltaTime;
				pText.transform.position = new Vector3(0,9999,0);
			}
			else
			{
				ShineFlag = true;
				pText.transform.position = PositionOrig;
			}
		}
	}
}
