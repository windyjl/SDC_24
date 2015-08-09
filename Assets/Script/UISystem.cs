using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UISystem : MonoBehaviour {
    public GameObject MainCanvas;
    public GameObject MenuButton;
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
    public GameObject uigCommunication;
    public Text txtValueCommunication;//通讯记录文字
    public GameObject uigEngine;
    public GameObject uigMainCanon;
//瞄准方式
    public GameObject btnAimType;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

//瞄准方式——相对/绝对坐标
    public void SwitchAimType()
    {
        if(WeaponSystem.getInstance().bAimType_Abs)
        {
            WeaponSystem.getInstance().bAimType_Abs = false;
            btnAimType.GetComponentInChildren<Text>().text = "相对";
        }
        else
        {
            WeaponSystem.getInstance().bAimType_Abs = true;
            btnAimType.GetComponentInChildren<Text>().text = "绝对";
        }
    }
}
