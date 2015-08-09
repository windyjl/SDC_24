using UnityEngine;
using Vectrosity;
using System.Collections;

public class EnemySign : MonoBehaviour {
	public int nRectSize = 10;
	VectorLine oLine;
	public float ShineInterval = 1.0f;
	private float ShineCount;
	private bool hasInit = false;
	// Use this for initialization
	void Start () {
		//initRectLines ();
	}
	public void initRectLines()
	{
		hasInit = true;
		ShineCount = ShineInterval;
		Vector3[] linePoints = {new Vector3 (-nRectSize/2, nRectSize/2, 0)
								,new Vector3 (nRectSize/2, nRectSize/2, 0)
								,new Vector3 (nRectSize/2, -nRectSize/2, 0)
								,new Vector3 (-nRectSize/2, -nRectSize/2, 0)
								,new Vector3 (-nRectSize/2, nRectSize/2, 0)};
		oLine = VectorLine.SetLine(Color.red,linePoints);
		VectorManager.ObjectSetup(gameObject,oLine,Visibility.Always, Brightness.None);
	}
	// Update is called once per frame
	void Update () {
		if(hasInit)
			Shine ();
	}
	void Shine()
	{
		ShineCount -= Time.deltaTime;
		if (ShineCount < 0) 
		{
			ShineCount = ShineInterval;
		}
		float scheShine = ShineCount / ShineInterval;
		oLine.SetColor (Color.Lerp(Color.black,Color.red,scheShine));
	}
}
