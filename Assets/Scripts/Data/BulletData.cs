using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;


public class BulletData 
{
    [XmlArray("BulletList")]
    public List<BulletInfo> bulletList = new List<BulletInfo>();
}

public class BulletInfo
{
    [XmlAttribute]
    public int id;
    [XmlAttribute]
    public int type;    //移动类型  
    [XmlAttribute]
    public float forwardSpeed;  //面朝向移动速度
    [XmlAttribute]
    public float rightSpeed;    //横向移动速度
    [XmlAttribute]
    public float roundSpeed;    //旋转速度
    [XmlAttribute]
    public string resPath;      //特效资源路径
    [XmlAttribute]
    public string deathEffResPath;  //子弹销毁时 特效
    [XmlAttribute]
    public float lifeTime;      //子弹持续时间

    
}
