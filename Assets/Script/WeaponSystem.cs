using UnityEngine;
using System.Collections;

public class WeaponSystem {
    private static WeaponSystem instance;
    public bool bAimType_Abs = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public static WeaponSystem getInstance()
    {
        if (instance==null)
        {
            instance = new WeaponSystem();
        }
        return instance;
    }
}
