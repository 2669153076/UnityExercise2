using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

public class XMLDataMgr 
{
    private static XMLDataMgr instance = new XMLDataMgr();
    public static XMLDataMgr Instance=>instance;
    private XMLDataMgr() { }

    public void SaveData(object data, string fileName)
    {
        string path = Application.persistentDataPath +"/"+ fileName + ".xml";
        
        using(StreamWriter sw = new StreamWriter(path))
        {
            XmlSerializer xmlSerializer = new XmlSerializer(data.GetType());
            xmlSerializer.Serialize(sw, data);
        }
    }

    public object LoadData(Type type , string fileName) 
    {
        string path = Application.persistentDataPath + "/" + fileName + ".xml";
        if (!File.Exists(path))
        {
            path = Application.streamingAssetsPath + "/" + fileName + ".xml";
            if (!File.Exists(path))
            {
                return Activator.CreateInstance(type);
            }
        }

        using (StreamReader sr = new StreamReader(path))
        {
            XmlSerializer xmlSerializer = new XmlSerializer(type);
            return xmlSerializer.Deserialize(sr);
        }


    }
}
