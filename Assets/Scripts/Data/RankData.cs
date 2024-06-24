using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class RankData
{
    public List<RankInfo> ranks = new List<RankInfo>();


}

public class RankInfo
{
    [XmlAttribute("name")]
    public string name;
    [XmlAttribute("time")]
    public int time;
}
