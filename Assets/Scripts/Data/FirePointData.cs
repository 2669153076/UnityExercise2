using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class FirePointData 
{
    [XmlArray("FirePoints")]
    public List<FirePointInfo> firePoints = new List<FirePointInfo>();
}

public class FirePointInfo
{
    [XmlAttribute]
    public int id;  //开火点ID
    [XmlAttribute]
    public int type;    //开火点开火类型 1.顺序 2.散弹
    [XmlAttribute]
    public int num; //一次发射子弹的数量
    [XmlAttribute]
    public float delay;   //顺序发射子弹时 每颗子弹间隔时间
    [XmlAttribute]
    public string ids;  //关联的子弹ID 用于随机生成不同类型的子弹
    [XmlAttribute]
    public float cd;    //每一轮的间隔时间

}
