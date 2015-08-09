using UnityEngine;
using System.Collections;
public class EnemyBullet : MonoBehaviour {

	public Vector3 BulletSpeed;
	public eShipType TargeType = eShipType.Player;
	// Use this for initialization
	void Start () {
	
	}
	void OnTriggerEnter(Collider other)
	{
		Ship tShip = other.GetComponent<Ship> ();
		if (tShip) {
			if (tShip.ShipType == TargeType) {
				Destroy (gameObject);
				tShip.Boomb ();
			}
		} else {
			Destroy(gameObject);
		}
	}
	// Update is called once per frame
	void Update () {
		transform.Translate (BulletSpeed);
	}
}
