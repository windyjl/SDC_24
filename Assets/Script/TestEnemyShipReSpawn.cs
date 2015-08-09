using UnityEngine;
using System.Collections;

public struct sRespawnInfo
{
    Vector3 pos;
    Vector3 speed;
}
public class TestEnemyShipReSpawn : MonoBehaviour {
    private Ship pShip;
    private sRespawnInfo[] arrRespawnInfo;
	// Use this for initialization
	void Start () {
	    pShip = GetComponent<Ship>();
        arrRespawnInfo = new sRespawnInfo[4];
	}
	
	// Update is called once per frame
	void Update () {
        
	    CheckRespawn();
	}
    void CheckRespawn()
    {
        bool res = false;
        if (pShip.Speed.x != 0)
        {
            if (pShip.Speed.x < 0)
            {
                if (transform.position.x < -102.64f)
                {
                    res = true;
                }
            }
            else if (pShip.Speed.x > 0)
            {
                if (transform.position.x > 102.64f)
                {
                    res = true;
                }
            }

        }
        else if (pShip.Speed.y != 0)
        {
            if (pShip.Speed.y < 0)
            {
                if (transform.position.y < -57.7f)
                {
                    res = true;
                }
            }
            else if (pShip.Speed.y > 0)
            {
                if (transform.position.y > 57.7f)
                {
                    res = true;
                }
            }
        }
        if (res)
        {
            int[] pon = { -1, 1 };//positive of negative
            int[] xoy = {103,58};// x o y
            Vector3 pos = new Vector3(0,0,0);
            Vector3 speed = new Vector3(0, 0, 0);
            int rXoY = Random.Range(0,xoy.Length);
            int rPoN = Random.Range(0, pon.Length);
            //出生点超出X
            if (rXoY==0)
            {
                pos.x = xoy[rXoY]*pon[rPoN];
                if (pon[rPoN]>0)
                    speed.x = -0.1f;
                else 
                    speed.x = 0.1f;
                
                //重新随机Y轴取正负值
                rPoN = Random.Range(0, pon.Length);
                pos.y = xoy[1 - rXoY] * pon[rPoN] * 0.8f; //另一个轴取80%处
            }
            //超出Y轴
            else
            {
                pos.y = xoy[rXoY] * pon[rPoN];
                if (pon[rPoN] > 0)
                    speed.y = -0.1f;
                else
                    speed.y = 0.1f;
                //重新随机Y轴取正负值
                rPoN = Random.Range(0, pon.Length);
                pos.x = xoy[1 - rXoY] * pon[rPoN] * 0.8f; //另一个轴取80%处
            }
            transform.position = pos;
            pShip.Speed = speed;
            pShip.OnEnemyRespawn();
        }
    }
}
