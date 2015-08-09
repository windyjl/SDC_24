using UnityEngine;
using System.Collections;

public class TitleButton : MonoBehaviour {
	public eOperationType OperType;
	private MainScript pMain;
	// Use this for initialization
	void Start () {
		pMain = GameObject.Find ("MainScript").GetComponent<MainScript> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void OnMouseDown(){
		pMain.OnSwitchOperation (OperType);
	}
}
