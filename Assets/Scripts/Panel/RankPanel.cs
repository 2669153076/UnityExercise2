using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankPanel : PanelBase<RankPanel>
{
    public UIButton closeBtn;
    public UIScrollView rankListSV;

    private List<RankItem> rankList = new List<RankItem>();

    public override void Init()
    {
        closeBtn.onClick.Add(new EventDelegate(() =>
        {
            HideSelf();
        }));


        HideSelf();
    }

    public override void ShowSelf()
    {
        base.ShowSelf();
        //获取本地存储的排行榜数据
        List<RankInfo> list = GameDataMgr.Instance.rankData.ranks;

        //根据数据 更新面板上 组合控件的信息
        //组合控件数量 只会增加 不会减少
        for (int i = 0; i < list.Count; i++)
        {
            if (rankList.Count > i)
            {
                rankList[i].InitInfo(i + 1, list[i].name, list[i].time);
            }
            //如果面板上组合控件不足 则添加新的控件
            else
            {
                GameObject obj = Instantiate(Resources.Load<GameObject>("UI/RankItem"));
                obj.transform.SetParent(rankListSV.transform, false);
                obj.transform.localPosition = new Vector3(0,105-i*45,0);

                //设置数据
                RankItem item = obj.GetComponent<RankItem>();
                item.InitInfo(i + 1, list[i].name, list[i].time);
                rankList.Add(item);
            }
        }
    }
}
