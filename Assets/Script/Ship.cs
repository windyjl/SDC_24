using UnityEngine;
using Vectrosity;
using UnityEngine.UI;
using System.Collections;
public enum eShipType
{
	Enemy,
	Player
}
public class Ship : MonoBehaviour {
	public eShipType ShipType = eShipType.Player; 
// 	public Slider SliderSpeedX;
// 	public Slider SliderSpeedY;
	public InputField ShotDirX;
	public InputField ShotDirY;
	public GameObject FirePoint;
	public Slider	sldSpotSpeed;
// 	public Slider pPower;
// 	public GameObject btnEngineStart;
// 	public GameObject txtSpeed;
	private bool isEngineStarted = false;
	private ParticleSystem mBoombParticle;
	private MainScript pMain;
	public ParticleSystem preBoomb;
	public GameObject preBullet;
	public float SpeedLimit=3;
	public float SpeedUpRate = 0.1f;
	public Vector3 Speed;
	private VectorLine FireLine;
	// Use this for initialization
	void Start () 
	{
		pMain = GameObject.Find ("MainScript").GetComponent<MainScript> ();
		mBoombParticle = GameObject.Instantiate (preBoomb) as ParticleSystem;
		mBoombParticle.transform.parent = transform;
		mBoombParticle.transform.localPosition = new Vector3(0,0,0);
		FirePoint.transform.localPosition = new Vector3(0,0,0);
		ShowMoveTrek ();
		if (ShipType == eShipType.Enemy)//以下代码给PlayerShip使用
			return;
		ShotDirX.text = "0";
		ShotDirY.text = "0";
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Translate (Speed);
		
		if (ShipType == eShipType.Enemy)//以下代码给PlayerShip使用
			return;
		OnChangeDirection ();//移动也需要更新
		SpeedUp ();
		refreshTxtSpeed ();
	}
	void ShowMoveTrek()
	{
		if (ShipType != eShipType.Enemy)//仅敌人显示轨迹
			return;
		if (Speed.magnitude < float.Epsilon)
			return;
		Vector3 targetPos = new Vector3 (Speed.x,Speed.y,0);
		targetPos.Normalize ();
		if (FireLine == null) {
			Vector3 [] _points = new Vector3[6];
			for(int i=0;i<6;++i)
			{
				_points[i]=new Vector3(pMain.AimLineScale*0.1f*i,0,0);
			}
			Color []  _color = new Color[5];
			_color[0] = Color.red;
			_color[1] = Color.yellow;
			_color[2] = Color.green;
			_color[3] = Color.blue;
			_color[4] = Color.red;
			FireLine = VectorLine.SetLine(Color.white,_points);
			FireLine.smoothColor = true;
			FireLine.SetColors(_color);
			VectorManager.ObjectSetup(FirePoint,FireLine,Visibility.Always, Brightness.None);
		}
		float direct = Mathf.Acos (targetPos.x);
		direct = direct/Mathf.PI * 180;
		if (targetPos.y < 0)
			direct = 360-direct;
		FirePoint.transform.localPosition = new Vector3 (0, 0, 0);
		FirePoint.transform.rotation = Quaternion.Euler(new Vector3(0,0,direct));
	}
	void refreshTxtSpeed()
	{
		if (!isEngineStarted)
			return;
		//Text _text = txtSpeed.GetComponent<Text> ();
		//_text.text = "航速:" + Speed.x.ToString ("f2") + "," + Speed.y.ToString ("f2");
	}
	public void Boomb()
	{
		mBoombParticle.Play ();
		if (ShipType == eShipType.Player) {
			if (pMain.StorySchedule == 0) 
			{
				pMain.StoryDone(0);
			}
		}
		else if(ShipType == eShipType.Enemy)
		{
			pMain.StoryDone(pMain.StorySchedule);
			Destroy(gameObject);
		}
	}
	public void EngineStart()
	{
		if (ShipType == eShipType.Enemy)//以下代码给PlayerShip使用
			return;
		//btnEngineStart.SetActive (false);
		//txtSpeed.SetActive (true);
		isEngineStarted = true;
	}
	public void SpeedUp()
	{
//		if (pPower.value > 0 && isEngineStarted) 
//		{
//			Speed.x += pPower.value*pSliderX.value*SpeedUpRate;//1:1换算超出预期，临时减少
//			Speed.y += pPower.value*pSliderY.value*SpeedUpRate;
//

//		}
// 		Speed.x = pPower.value * SliderSpeedX.value * SpeedLimit; 
// 		Speed.y = pPower.value * SliderSpeedY.value * SpeedLimit;
        Speed.x = 0.5f * Input.GetAxis("Horizontal") * SpeedLimit;
        Speed.y = 0.5f * Input.GetAxis("Vertical") * SpeedLimit;
//		//临时限速
//		if(Speed.x>SpeedLimit)
//			Speed.x = SpeedLimit;
//		if(Speed.x<-SpeedLimit)
//			Speed.x = -SpeedLimit;
//		if(Speed.y>SpeedLimit)
//			Speed.y = SpeedLimit;
//		if(Speed.y<-SpeedLimit)
//			Speed.y = -SpeedLimit;
	}
	void OnDestroy() {
		print("Script was destroyed");
	}
	public void OnShot()
	{
		if (ShipType == eShipType.Enemy)//以下代码给PlayerShip使用
			return;
		if (ShotDirX.text == "" || ShotDirY.text == "")
			return;
        ++pMain.UnlockItem.nMainCanonShootTimes;
        UnlockSystem.getInstance().OnMainCanonFire();
		Vector3 targetPos = new Vector3 (int.Parse(ShotDirX.text),int.Parse(ShotDirY.text),0);
		GameObject cbullet = GameObject.Instantiate (preBullet) as GameObject;
//		cbullet.transform.position = FirePoint.transform.position;
//		cbullet.transform.LookAt (targetPos);
		cbullet.transform.position = transform.position;
        //如果是相对瞄准，不进行这步
        if (WeaponSystem.getInstance().bAimType_Abs)
        {
		    targetPos -= cbullet.transform.position;
        }
		targetPos.Normalize ();
        cbullet.GetComponent<EnemyBullet>().BulletSpeed = new Vector3(targetPos.x * sldSpotSpeed.value, targetPos.y * sldSpotSpeed.value, 0);
		cbullet.GetComponent<EnemyBullet> ().TargeType = eShipType.Enemy;
		cbullet.GetComponent<EnemySign> ().initRectLines ();
        //主炮出膛时，计算后坐力
        //TODO
// 		if (FireLine!=null)
// 			FirePoint.SetActive (false);
	}
	public void OnChangeDirection()
	{
		if (ShipType == eShipType.Enemy)//以下代码给PlayerShip使用
			return;
		if (!FirePoint.activeInHierarchy)
			return;
		if (ShotDirX.text == "" || ShotDirY.text == "")
			return;
		Vector3 targetPos = new Vector3 (int.Parse(ShotDirX.text),int.Parse(ShotDirY.text),0);
        //如果是相对瞄准，不进行这步
        if (WeaponSystem.getInstance().bAimType_Abs)
        {
            targetPos -= transform.position;
        }
		targetPos.Normalize ();
		if (FireLine == null) {
			Vector3 [] _points = new Vector3[6];
			for(int i=0;i<6;++i)
			{
				_points[i]=new Vector3(pMain.AimLineScale*0.1f*i,0,0);
			}
			Color []  _color = new Color[5];
			_color[0] = Color.red;
			_color[1] = Color.yellow;
			_color[2] = Color.green;
			_color[3] = Color.blue;
			_color[4] = Color.red;
			FireLine = VectorLine.SetLine(Color.white,_points);
			FireLine.smoothColor = true;
			FireLine.SetColors(_color);
			VectorManager.ObjectSetup(FirePoint,FireLine,Visibility.Always, Brightness.None);
		}
		float direct = Mathf.Acos (targetPos.x);
		direct = direct/Mathf.PI * 180;
		if (targetPos.y < 0)
			direct = 360-direct;
		FirePoint.transform.rotation = Quaternion.Euler(new Vector3(0,0,direct));
		//FirePoint.transform.rotation = Quaternion.Euler (0,0,90);
	}


    public void OnEnemyRespawn()
    {
        Vector3 targetPos = new Vector3(Speed.x, Speed.y, 0);
        //targetPos -= transform.position;
        targetPos.Normalize();
        if (FireLine == null)
        {
            Vector3[] _points = new Vector3[6];
            for (int i = 0; i < 6; ++i)
            {
                _points[i] = new Vector3(pMain.AimLineScale * 0.1f * i, 0, 0);
            }
            Color[] _color = new Color[5];
            _color[0] = Color.red;
            _color[1] = Color.yellow;
            _color[2] = Color.green;
            _color[3] = Color.blue;
            _color[4] = Color.red;
            FireLine = VectorLine.SetLine(Color.white, _points);
            FireLine.smoothColor = true;
            FireLine.SetColors(_color);
            VectorManager.ObjectSetup(FirePoint, FireLine, Visibility.Always, Brightness.None);
        }
        float direct = Mathf.Acos(targetPos.x);
        direct = direct / Mathf.PI * 180;
        if (targetPos.y < 0)
            direct = 360 - direct;
        FirePoint.transform.rotation = Quaternion.Euler(new Vector3(0, 0, direct));
        //FirePoint.transform.rotation = Quaternion.Euler (0,0,90);
    }
}
