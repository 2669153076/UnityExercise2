using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class RoleData 
{

    public List<RoleInfo> roleList = new List<RoleInfo>();
}

public class RoleInfo
{
    [XmlAttribute("HP")]
    public int hp;
    [XmlAttribute("Speed")]
    public int speed;
    [XmlAttribute("Volume")]
    public int volume;  //体积
    [XmlAttribute("ResPath")]
    public string resPath;  //资源路径
    [XmlAttribute("Scale")]
    public float scale;   //模型缩放大小
}
