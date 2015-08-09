using UnityEngine;
using Vectrosity;
using System.Collections;
/*
 * 2015年8月4日0点51分40秒
 * 修改为非单例形式，作为每个GameObject的Component
 */
public delegate bool TrackFunction();
public class HashSignAim : MonoBehaviour 
{
	private GameObject LineObj;	//线条载体预设，实际上没什么用，空对象都行，就是不知道怎么new
	public int nLockSpeed = 1;
	GameObject _tempTrackTarget;
	bool	hasInit = false;
	GameObject[] arrHashObj;
	VectorLine[] arrHash;
	Vector3[][]	 arrHashPoints;
	//步骤标记
	int			nRunStep;
	TrackFunction[] fnTrackFuncArray;
	//定位信息
	Vector3		posTarget;
	bool		isTrackGameobject = false;
	GameObject	pTargetObj = null;
	int			minSize;
	bool[]		hasRandomMoveOnce;	//受否随机运动过一次
	int[]		nRandomMoveOff;		//随机移动的距离
	bool[]		hadTrackTargetPos;	//是否已经追踪到目标
	bool		isAllLineTrackedTarget;	//是否所有线都追踪到了目标
	//定位后淡出
	public float	fFadeOutDuration = 2;
	float		fFadeOutCount;
	// Use this for initialization
	void Awake ()
	{
		fnTrackFuncArray = new TrackFunction[3];
		fnTrackFuncArray[0] = LockOn_1st;
		fnTrackFuncArray[1] = LockOn_2nd;
		fnTrackFuncArray[2] = LockOn_3rd;
	}
	void Start () {
		nRunStep = 0;
        setTarget(gameObject,10);
		//test
		//setTarget (0, 0, 10);
		//setTarget (_tempTrackTarget, 10);
	}
	public void setTarget(GameObject pObj,int _minSize)
	{
		isTrackGameobject = true;
		pTargetObj = pObj;
		minSize = _minSize;
		initHashArray ();
	}
	public void setTarget(int x,int y,int _minSize)
	{
		posTarget = new Vector3 (x, y, 0);
		minSize = _minSize;
		initHashArray ();
	}
	void initHashArray()
	{
		hasInit = true;
		isAllLineTrackedTarget = false;
		//_line.transform.SetParent (pHashContainer);
		arrHash = new VectorLine[4];
		arrHashPoints = new Vector3[4][];
		arrHashObj	= new GameObject[4];
		hasRandomMoveOnce = new bool[4];
		hadTrackTargetPos = new bool[4];
		nRandomMoveOff = new int[4];

		fFadeOutCount = fFadeOutDuration;
		for(int i=0;i<4;++i)
		{
			//容纳线条的GameObject容器
			//arrHashObj[i] = GameObject.Instantiate (preLineObj) as GameObject;
            arrHashObj[i] = new GameObject();
			//初始化线条点阵
			Vector3[] linePoints = {new Vector3(0, 100,0),new Vector3(0,-100,0)};
			arrHashPoints[i] = linePoints;
			//创建线条对象
			arrHash[i] = VectorLine.SetLine(Color.white,linePoints);
			// Draw the line
			arrHash[i].Draw();
			arrHash[i].SetWidth(2);
			//绑定线条到GameObject容器上
			VectorManager.ObjectSetup(arrHashObj[i],arrHash[i],Visibility.Always, Brightness.None);
			//制作4根位置不同的线条
			//随机出生，随机移动

			int udEdge = 57;
			int lrEdge = 57*16/9;
			switch(i)
			{
			case 0:
				arrHashObj[i].transform.Rotate(0,0,90);
				arrHashObj[i].transform.position=new Vector3(0,-udEdge,0);
				nRandomMoveOff[i] = Random.Range(0,udEdge);
				break;
			case 1:
				arrHashObj[i].transform.Rotate(0,0,90);
				arrHashObj[i].transform.position=new Vector3(0,udEdge,0);
				nRandomMoveOff[i] = Random.Range(-udEdge,0);
				break;
			case 2:
				arrHashObj[i].transform.position=new Vector3(-lrEdge,0,0);
				nRandomMoveOff[i] = Random.Range(0,lrEdge);
				break;
			case 3:
				arrHashObj[i].transform.position=new Vector3(lrEdge,0,0);
				nRandomMoveOff[i] = Random.Range(-lrEdge,0);
				break;
			}
		}
	}
	// Update is called once per frame
	void Update () 
	{
		//UpdateLockOn ();

        if (isTrackGameobject)
            posTarget = pTargetObj.transform.position;
        if (!hasInit)
            return;
        if (nRunStep<3)
        {
            if(fnTrackFuncArray[nRunStep]())
                ++nRunStep;
        }
        else if (nRunStep<4)
        {
            for (int i=0;i<4;++i)
            {
                Destroy(arrHashObj[i]);
            }
            ++nRunStep;
        }
	}
    void FixedUpdate()
    {
        //强制通过脚本让已经文成追踪的线条，同步坐标到目标边框
        if (nRunStep<4)
        {
            FollowAfterTracked();
        }
    }
	// void LaterUpdate()
	// {
	// 	for (int i=0; i<4; ++i) 
	// 	{

	// 	}
	// }
	void UpdateLockOn()
	{
		if (!hasInit)
			return;
		if (isTrackGameobject)
			posTarget = pTargetObj.transform.position;
		for (int i=0; i<4; ++i) {
			//			for(int j=0;j<arrHashPoints[i].Length;++j)
			//			{
			//				arrHashPoints[i][j].x += 1;
			//				arrHash[i].Draw();
			//			}
			//arrHashObj[i].transform.position += new Vector3(1,0,0);
			//第一步随机移动
			if(!hasRandomMoveOnce[i])
			{
				//横线
//				if(i<2)
//				{
//					int off = nRandomMoveOff[i] - (int)arrHashObj[i].transform.position.y;
//					if(Mathf.Abs(off)>minSize)
//					{
//						if(off>0)
//							arrHashObj[i].transform.position += new Vector3(0,nLockSpeed,0);
//						else
//							arrHashObj[i].transform.position -= new Vector3(0,nLockSpeed,0);
//					}
//					else
//						hasRandomMoveOnce[i] = true;
//				}
//				//竖线
//				else
//				{
//					int off = nRandomMoveOff[i] - (int)arrHashObj[i].transform.position.x;
//					if(Mathf.Abs(off)>minSize)
//					{
//						if(off>0)
//							arrHashObj[i].transform.position += new Vector3(nLockSpeed,0,0);
//						else
//							arrHashObj[i].transform.position -= new Vector3(nLockSpeed,0,0);
//					}
//					else
//						hasRandomMoveOnce[i] = true;
//				}
				if(TrackPosition(nRandomMoveOff[i],i))
					hasRandomMoveOnce[i] = true;
			}
			//第二步 追踪到目的地
			else if(!isAllLineTrackedTarget && !hadTrackTargetPos[i])
			{
				if(i<2)
				{	
					if(TrackPosition((int)posTarget.y,i))
					{
						//arrHashObj[i].transform.parent = pTargetObj.transform;
						hadTrackTargetPos[i] = true;
					}
				}
				else
				{
					if(TrackPosition((int)posTarget.x,i))
					{
						//arrHashObj[i].transform.parent = pTargetObj.transform;
						hadTrackTargetPos[i] = true;
					}
				}
			}
			//第三步 淡出
			else if(fFadeOutCount>0)
			{
				if(i==3)
				{
					fFadeOutCount-=Time.deltaTime;
					if(fFadeOutCount<0)	fFadeOutCount=0;
				}
				float _scale = fFadeOutCount/fFadeOutDuration;
				//小动作，将横纵方向原本不滚动的轴坐标对齐到目标中心，确保跟踪不在屏幕中心的对象时，淡出缩放正确
				Vector3 _newPos = arrHashObj[i].transform.position;
				Vector3 _tarPos = posTarget;
				if(i<2)
					_tarPos.y = _newPos.y;
				else
					_tarPos.x = _newPos.x;
				_newPos = Vector3.Lerp(_tarPos,_newPos,_scale);	// _scale是反的
				arrHashObj[i].transform.position = _newPos;
				arrHashObj[i].transform.localScale = new Vector3(_scale,_scale,_scale);
			}
			else
			{
				print("all done! byebye~");
     			Destroy(arrHashObj[i]);
				if(i==3)
					hasInit = false;	//临时强写第四个删除后，就当什么都没发生过
			}
		}
		//所有点追踪成功瞬间
		if (isAllLineTrackedTarget != (hadTrackTargetPos [0] && hadTrackTargetPos [1] && hadTrackTargetPos [2] && hadTrackTargetPos [3])) 
		{
			isAllLineTrackedTarget = true;
//			for(int i=0;i<4;++i)
//			{
//				arrHashObj[i].transform.parent = pTargetObj.transform;
//			}
		}
	}
	//包装的区别调整四条线
	bool TrackPosition(int targetCoordinate,int lineIndex)
	{
		return TrackPosition (targetCoordinate, lineIndex, false);
	}
	bool TrackPosition(int targetCoordinate,int lineIndex,bool isRemainTracking)
	{
		int off = 0;
		bool fContinue = true;	//继续追踪
		if (lineIndex < 2) 
		{
			off = targetCoordinate - (int)arrHashObj [lineIndex].transform.position.y;
		} else 
		{
			off = targetCoordinate - (int)arrHashObj [lineIndex].transform.position.x;
		}
		//是否已经到达追踪区域
		if(Mathf.Abs(off)>minSize/2)
			fContinue = true;
		else
			fContinue = false;
		//不写复杂了，反正到达区域后，继续移动到对应的一边才算成功
		switch(lineIndex) 
		{
		case 0:
			//off-=2;
			off-=minSize/2;
			break;
		case 3:
			off-=minSize/2;
			break;
		case 1:
			off+=minSize/2;
			break;
		case 2:
			off+=minSize/2;
			break;
		default:
			break;
		}
		fContinue |= Mathf.Abs(off)>nLockSpeed;
		//横线
		if(lineIndex<2)
		{
			if(fContinue|isRemainTracking)
			{
				if(off>0)
					arrHashObj[lineIndex].transform.position += new Vector3(0,nLockSpeed,0);
				else
					arrHashObj[lineIndex].transform.position -= new Vector3(0,nLockSpeed,0);
			}
			else
				return true;
		}
		//竖线
		else
		{
			if(fContinue|isRemainTracking)
			{
				if(off>0)
					arrHashObj[lineIndex].transform.position += new Vector3(nLockSpeed,0,0);
				else
					arrHashObj[lineIndex].transform.position -= new Vector3(nLockSpeed,0,0);
			}
			else
				return true;
		}
		return false;
	}
	bool LockOn_1st()
	{
        bool isAllDone = true;
		for (int i=0; i<4; ++i) 
        {
			if(!hasRandomMoveOnce[i])
			{
				if(TrackPosition(nRandomMoveOff[i],i))
					hasRandomMoveOnce[i] = true;
			}
            isAllDone = isAllDone && hasRandomMoveOnce[i];
        }
        if (isAllDone)
        {
            return true;
        }
        return false;
	}
	bool LockOn_2nd()
	{
        bool isAllDone = true;
		for (int i=0; i<4; ++i) 
		{
			if(i<2)
			{	
				if(TrackPosition((int)posTarget.y,i))
				{
					//arrHashObj[i].transform.parent = pTargetObj.transform;
					hadTrackTargetPos[i] = true;
				}
			}
			else
			{
				if(TrackPosition((int)posTarget.x,i))
				{
					//arrHashObj[i].transform.parent = pTargetObj.transform;
					hadTrackTargetPos[i] = true;
				}
			}
            isAllDone = isAllDone && hadTrackTargetPos[i];
		}

        if (isAllDone)
        {
            return true;
        }
        return false;
	}
	bool LockOn_3rd()
	{
		for (int i=0; i<4; ++i) 
		{
			float _scale = fFadeOutCount/fFadeOutDuration;
			//小动作，将横纵方向原本不滚动的轴坐标对齐到目标中心，确保跟踪不在屏幕中心的对象时，淡出缩放正确
			Vector3 _newPos = arrHashObj[i].transform.position;
			Vector3 _tarPos = posTarget;
			if(i<2)
				_tarPos.y = _newPos.y;
			else
				_tarPos.x = _newPos.x;
			_newPos = Vector3.Lerp(_tarPos,_newPos,_scale);	// _scale是反的
			arrHashObj[i].transform.position = _newPos;
			arrHashObj[i].transform.localScale = new Vector3(_scale,_scale,_scale);
		}

		fFadeOutCount-=Time.deltaTime;
		if(fFadeOutCount<0)	
		{
			fFadeOutCount=0;
            return true;
		}
        return false;
	}
    void FollowAfterTracked()
    {
		for (int i=0; i<4; ++i) 
		{
            if (hadTrackTargetPos[i] == true)
            {
                float targetX = posTarget.x;
                float targetY = posTarget.y;
                switch (i)
                {
                    case 0:
                        //off-=2;
                        targetY -= minSize / 2;
                        break;
                    case 3:
                        targetX -= minSize / 2;
                        break;
                    case 1:
                        targetY += minSize / 2;
                        break;
                    case 2:
                        targetX += minSize / 2;
                        break;
                    default:
                        break;
                }
                //arrHashObj[i].transform.position = new Vector3(targetX,targetY,0);
                arrHashObj[i].transform.position = Vector3.Lerp(arrHashObj[i].transform.position, new Vector3(targetX, targetY, 0), 0.1f);
            }
        }
    }
}
