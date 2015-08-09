using UnityEngine;
using System.Collections;
/*
 * UnlockSystem完成三项功能
 * 提供统计项的具体统计方法
 * 判断解锁项目条件完成
 * 修改功能对应界面的标识，以保证新功能的正常使用
 */
/*
 * 具体的条件录入通过编辑器实现
 */
public class UnlockSystem  {
    private static UnlockSystem instance;

    public int nMainCanonFireTimesCount = 0;

    public static UnlockSystem getInstance()
    {
        if (instance == null)
        {
            //初始化
            instance = new UnlockSystem();
            //读取存档信息
        }
        return instance;
    }
    private void DataUpdate()
    {
        Debug.Log("尼玛，居然有数据更新了!");
        return;
    }
    public void OnMainCanonFire()
    {
        ++nMainCanonFireTimesCount;
        DataUpdate();
    }
}
