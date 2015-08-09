using UnityEngine;
using Vectrosity;
using System.Collections;

public class GateHalo : MonoBehaviour {
	public Vector3 centerPos;
	public float radius;
	// Use this for initialization
	void Start () {
		Vector3[] points = new Vector3[20];
		int startAngle = 0;
		for (int i=0; i<=20; ++i) 
		{
			startAngle += (int)(360.0f/19)*i;
			points[i] = centerPos + new Vector3(Mathf.Cos(startAngle)*radius,Mathf.Sin(startAngle)*radius,0);
		}
		VectorLine _line = VectorLine.SetLine(Color.yellow,points);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
