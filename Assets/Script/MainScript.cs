using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum eOperationType
{
	Engine,
	MainCanon,
	Scout,
	Communication
};
public struct sUnlockItem
{
    public int nMainCanonShootTimes;
//     sUnlockItem()
//     {
//         nMainCanonShootTimes = 0;
//     }
}
public class MainScript : MonoBehaviour {
	public int StorySchedule = 0;
	private	bool isStoryScheduleDone = false;
	private int StoryStepCount = 0;
	private float StoryTimeCount = 0;
	// Use this for initialization
	public Slider pEngineX;
	public Slider pEngineY;
	public GameObject ShipSystemUI;
	public GameObject MenuButton;
	private bool	isMenuOn = false;
	public GameObject MainPanel;
	public GameObject TitlePanel;
	public GameObject btnMainCanon;
	public GameObject btnEngine;
	public GameObject btnScout;
	public GameObject btnCommunication;
	public GameObject txtMainCanon;
	public GameObject txtEngine;
	public GameObject txtScout;
	public GameObject txtCommunication;
    //主面板内容
	public GameObject uigCommunication;
	public Text	txtValueCommunication;//通讯记录文字
    public GameObject uigEngine;
    public GameObject uigMainCanon;
    public GameObject uigScout;

	//游戏参数
	public float AimLineScale = 100.0f;
	//剧情要素——敌人
    public GameObject _tempTrackTarget;
    public GameObject EnemyBullet;
	public GameObject EnemyShip_1;
	public GameObject EnemyShip_2;
    //界面系统
    public UISystem pShipUISystem;
    //解锁要素
    //public GameObject btnMainCanonAimType;    //2015年8月10日1点37分46秒 交给UISystem负责
    public sUnlockItem UnlockItem;
	void Start () {
	    //UnlockItem = new sUnlockItem();
	}
	
	// Update is called once per frame
	void Update () {
		DirectStory ();
        //检测个别界面的开启条件
        if (UnlockItem.nMainCanonShootTimes>2)
        {
            if (!pShipUISystem.btnAimType.activeSelf)
            {
                pShipUISystem.btnAimType.SetActive(true);
            }
        }
	}

	void OnEngineStart()
	{

	}
	public void OnResetEngineDirection()
	{
		pEngineX.value = 0;
		pEngineY.value = 0;
	}
	//0--飞弹袭来，不可操作
	//1--显示通讯：报告舰长（干扰）冲击，计算（干扰）恢复。重复一遍，本舰受到适当冲击，计算机系统已经恢复！
	//2--显示指挥界面（仅“引擎”），可操作
	//3--固定敌机出现
	//4--移动敌机出现
	public void StoryDone(int index)
	{
		++StorySchedule;
		isStoryScheduleDone = false;
		StoryStepCount = 0;
		StoryTimeCount = 0;
	}
	public void DirectStory()
	{
		if (isStoryScheduleDone)
			return;
		//HashSignAim pHashAim = GameObject.Find("HashAimScript").GetComponent<HashSignAim>();
		switch (StorySchedule) 
		{
		case 0:
			if(StoryStepCount==0)
			{
				//GameObject obj = GameObject.Find("UICanvas");
				ShipSystemUI.SetActive(false);
                EnemyBullet.SetActive(true);
				++StoryStepCount;
			}
			if(StoryTimeCount>2.0f&&StoryStepCount==1)
			{
				//HashSignAim pHashAim = GameObject.Find("HashAimScript").GetComponent<HashSignAim>();
				//pHashAim.setTarget (_tempTrackTarget, 10);
                _tempTrackTarget.AddComponent<HashSignAim>();
				++StoryStepCount;
			}
			else if(StoryTimeCount>4.0f)
			{
				EnemySign _tempTrackTarget = GameObject.Find("Bullet001").GetComponent<EnemySign>();
				_tempTrackTarget.initRectLines();
				isStoryScheduleDone = true;
			}
			break;
		case 1:	// 操作界面恢复
			//显示标题文字
			if(StoryStepCount==0 && StoryTimeCount>2)
			{
				ShipSystemUI.SetActive(true);
				MainPanel.SetActive(true);
				++StoryStepCount;
				StoryTimeCount = 0;
			}
			else if(StoryStepCount==1 && StoryTimeCount>1)
			{
				MainPanel.SetActive(false);
				++StoryStepCount;
				StoryTimeCount = 0;
			}
			else if(StoryStepCount==2 && StoryTimeCount>0.1f)
			{
				MainPanel.SetActive(true);
				StoryTimeCount = 0;
				++StoryStepCount;
			}
			else if(StoryStepCount==3 && StoryTimeCount>0.1f)
			{
				MainPanel.SetActive(false);
				StoryTimeCount = 0;
				++StoryStepCount;
			}
			else if(StoryStepCount==4 && StoryTimeCount>0.1f)
			{
				MainPanel.SetActive(true);
				StoryTimeCount = 0;
				++StoryStepCount;
			}
			//显示主屏幕
			else if(StoryStepCount==5 && StoryTimeCount>1)
			{
				btnCommunication.SetActive(true);
				txtCommunication.GetComponent<Text>().color = Color.white;
				++StoryStepCount;
			}
			//显示通讯
			else if(StoryStepCount==6 && StoryTimeCount>2)
			{
				uigCommunication.SetActive(true);
				txtValueCommunication.text += "舰长";
				++StoryStepCount;
				StoryTimeCount = 0;
			}
			else if(StoryStepCount==7 && StoryTimeCount>0.5f)
			{
				uigCommunication.SetActive(true);
				txtValueCommunication.text += " （干扰） ";
				++StoryStepCount;
				StoryTimeCount = 0;
			}
			else if(StoryStepCount==8 && StoryTimeCount>1)
			{
				uigCommunication.SetActive(true);
				txtValueCommunication.text += "你妈";
				++StoryStepCount;
				StoryTimeCount = 0;
			}
			else if(StoryStepCount==9 && StoryTimeCount>0.5f)
			{
				uigCommunication.SetActive(true);
				txtValueCommunication.text += " （干扰） ";
				++StoryStepCount;
				StoryTimeCount = 0;
			}
			else if(StoryStepCount==10 && StoryTimeCount>1)
			{
				uigCommunication.SetActive(true);
				txtValueCommunication.text += "炸了";
				++StoryStepCount;
				StoryTimeCount = 0;
			}
			else if(StoryStepCount==11 && StoryTimeCount>1)
			{
				uigCommunication.SetActive(true);
				txtValueCommunication.text += "\n重复一遍";
				++StoryStepCount;
				StoryTimeCount = 0;
			}
			else if(StoryStepCount==12 && StoryTimeCount>1)
			{
				uigCommunication.SetActive(true);
				txtValueCommunication.text += "\n本舰受到适度撞击，战舰机能恢复。";
				++StoryStepCount;
				StoryTimeCount = 0;
			}
			else if(StoryStepCount==13 && StoryTimeCount>2)
			{
				uigCommunication.SetActive(true);
				txtValueCommunication.text += "\n本舰受到适度撞击，战舰机能恢复。";
				++StoryStepCount;
			}
			else if(StoryStepCount==14 && StoryTimeCount>5)
			{
				uigCommunication.SetActive(true);
				txtValueCommunication.text += "\n（这你妈都可以——嘘！通讯还没关——）";
				++StoryStepCount;
			}
			else if(StoryStepCount==15 && StoryTimeCount>7)
			{
				TitlePanel.SetActive(false);
				MainPanel.SetActive(false);
				StoryTimeCount = 0;
				++StoryStepCount;
				//isStoryScheduleDone = true;
			}
			//显示操作按钮
			
			else if(StoryStepCount==16)
			{
				TitlePanel.SetActive(false);
				MainPanel.SetActive(false);
				isStoryScheduleDone = true;
				StoryDone(1);
			}
			break;
		case 2:
			if(StoryStepCount==0)
            {
				ShipSystemUI.SetActive(true);	//主界面解锁
				TitlePanel.SetActive(false);//菜单隐藏
				MainPanel.SetActive(false);	//隐藏操作框（菜单下面的大白板）
				MenuButton.SetActive(true);	//解锁主菜单按钮
				btnEngine.SetActive(true);	//解锁引擎菜单
				btnMainCanon.SetActive(true);
				btnCommunication.SetActive(true);//解锁通讯菜单
				++StoryStepCount;
			}
			else if(StoryStepCount==1&&StoryTimeCount>1)
			{
				isStoryScheduleDone = true;
				StoryDone(2);
			}
			break;
		case 3:
			EnemyShip_1.SetActive(true);
			//pHashAim.setTarget(EnemyShip_1,10);
            EnemyShip_1.AddComponent<HashSignAim>();
			isStoryScheduleDone = true;
			break;
		case 4:
			EnemyShip_2.SetActive(true);
			//pHashAim.setTarget(EnemyShip_2,10);
            EnemyShip_2.AddComponent<HashSignAim>();
			isStoryScheduleDone = true;
			break;
//         case 5:
//             //临时额外操作，为子弹增加新的瞄准算法
//             EnemyBullet.AddComponent<HashSignAim>();
//             isStoryScheduleDone = true;
//                 break;
		}
		StoryTimeCount += Time.deltaTime;
	}
	void SetUIVisible(string _name,bool visible)
	{
		GameObject obj = GameObject.Find (_name);
		obj.SetActive (visible);
	}
	//切换主控菜单
	public void OnMenuButtonClick()
	{
		if (!isMenuOn)
		{
			TitlePanel.SetActive (true);
            MainPanel.SetActive(true);
            Ship ship = GameObject.Find("This ship").GetComponent<Ship>();
            ship.FirePoint.SetActive(true);
			isMenuOn = true;
		} 
		else
		{
			TitlePanel.SetActive (false);
			MainPanel.SetActive (false);
			isMenuOn = false;
		}
	}
	/*
	 * 0-引擎
	 * 1-主炮
	 * 2-观测
	 * 3-通讯
	 */

	public void OnSwitchOperation(eOperationType eOper)
	{
		MainPanel.SetActive (true);
		txtMainCanon.GetComponent<Text>().color = Color.gray;
		txtEngine.GetComponent<Text>().color = Color.gray;
		txtScout.GetComponent<Text>().color = Color.gray;
		txtCommunication.GetComponent<Text>().color = Color.gray;
		bool fMainCanon		= false;
		bool fEngine		= false;
		bool fScout			= false;
		bool fCommunication	= false;
		switch (eOper) {
		case eOperationType.MainCanon:
			
			Ship ship = GameObject.Find("This ship").GetComponent<Ship>();
			txtMainCanon.GetComponent<Text>().color = Color.white;
			fMainCanon	= true;
			break;
		case eOperationType.Engine:
			txtEngine.GetComponent<Text>().color = Color.white;
			fEngine 	= true;
			break;
		case eOperationType.Scout:
			txtScout.GetComponent<Text>().color = Color.white;
			fScout		= true;
			break;
		case eOperationType.Communication:
			txtCommunication.GetComponent<Text>().color = Color.white;
			fCommunication	= true;
			break;
		}
		uigMainCanon.SetActive(fMainCanon);
		uigEngine.SetActive(fEngine);
//		btnScout.SetActive(fScout);
		uigCommunication.SetActive(fCommunication);
	}
	//方向Slider归零
	public void OnStartZero()
	{
		//GameObject.FindGameObjectsWithTag ("DirectionSlider");
		foreach(GameObject obj in GameObject.FindGameObjectsWithTag ("DirectionSlider"))
		{
			obj.GetComponent<SliderAutoZero>().fZero = true;
		}
	}
}
