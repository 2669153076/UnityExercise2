using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankItem : MonoBehaviour
{
    public UILabel idLabel;
    public UILabel nameLabel;
    public UILabel timeLabel;
    
    public void InitInfo(int id,string name,int time)
    {
        idLabel.text = id.ToString();
        nameLabel.text = name;
        string str = "";
        //时
        if (time / 3600 > 0)
            str += time / 3600 + "h";
        //分
        if (time % 3600 / 60 > 0||str!="")
            str += time % 3600 / 60 + "m";
        //秒
        str += time % 60 + "s";
        timeLabel.text = str;
    }
    
}
