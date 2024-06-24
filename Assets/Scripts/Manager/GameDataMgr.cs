using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataMgr 
{
    private static GameDataMgr instance = new GameDataMgr();
    public static GameDataMgr Instance=>instance;

    private GameDataMgr() { 
        settingData = XMLDataMgr.Instance.LoadData(typeof(SettingData),"settingData") as SettingData;
        rankData = XMLDataMgr.Instance.LoadData(typeof(RankData), "rankData") as RankData;
        roleData = XMLDataMgr.Instance.LoadData(typeof(RoleData),"roleData") as RoleData;
        bulletData = XMLDataMgr.Instance.LoadData(typeof(BulletData),"bulletData") as BulletData;
        firePointData = XMLDataMgr.Instance.LoadData(typeof(FirePointData), "firePointData") as FirePointData;
    }

    public SettingData settingData;
    public RankData rankData;
    public RoleData roleData;
    public BulletData bulletData;
    public FirePointData firePointData;

    public int nowSelAirplaneId = 0;

    #region 设置面板相关

    public void SavaSettingData()
    {
        XMLDataMgr.Instance.SaveData(settingData, "settingData");
    }
    public void SetMusicIsOpen(bool isOpen)
    {
        settingData.isOpenMusic = isOpen;
        BKMusic.Instance.SetBKMusicIsOpen(isOpen);
    }
    public void SetSoundIsOpen(bool isOpen)
    {
        settingData.isOpenSound = isOpen;
    }
    public void SetMusicVolumn(float volumn)
    {
        settingData.musicVolumn = volumn;
        BKMusic.Instance.SetBKMusicVolumn(volumn);
    }
    public void SetSoundVolumn(float volumn)
    {
        settingData.soundVolumn = volumn;
    }

    #endregion

    #region 排行榜相关

    public void SaveRankData()
    {
        XMLDataMgr.Instance.SaveData(rankData, "rankData");
    }

    public void AddRankData(string name,int time)
    {
        RankInfo info = new RankInfo();
        info.name = name;
        info.time = time;
        rankData.ranks.Add(info);

        //排序
        rankData.ranks.Sort((a, b) => { return a.time > b.time ? 1 : -1; });

        //移除大于20条的内容
        if (rankData.ranks.Count > 20)
        {
            //rankData.ranks.RemoveRange(20, rankData.ranks.Count - 20);
            rankData.ranks.RemoveAt(20);
        }

        //保存数据
        SaveRankData();
    }

    #endregion

    #region 角色数据相关
    /// <summary>
    /// 获取当前所选择的飞机的数据
    /// </summary>
    /// <returns></returns>
    public RoleInfo GetNowSelAirplaneInfo()
    {
        return roleData.roleList[nowSelAirplaneId];
    }
    #endregion
}
