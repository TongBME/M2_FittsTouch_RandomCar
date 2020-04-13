using UnityEngine;
using System;
using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using YamlDotNet.Serialization;

public class YamlTest : MonoBehaviour
{ 
					
   void Start()
    {
        var yaml = @"
1:
    typeID: 1
    sub_item:
        name: foo
2:
    typeID: 2
    sub_item:
        name: bar
";
        string yamlName = "E:\\test1.yaml";
        string content = File.ReadAllText(yamlName);
        var input = new StringReader(content);

        var deserializer = new DeserializerBuilder()
            .Build();
        var trialInfo = deserializer.Deserialize<Item>(input);
        //var blueprintsByID = deserializer.Deserialize<Dictionary<num, Item>>(input);
        //print(trialInfo.trial.circle);
    }
}

public class Item
{
    [YamlMember(Alias = "task")]
    public string task { get; set; }
    [YamlMember(Alias = "timer")]
    public SubTimer timer { get; set; }
    [YamlMember(Alias = "trial")]
    public SubItemTrial trial { get; set; }
    }

public class SubTimer
{
    [YamlMember(Alias = "touch")]
    public int touch { get; set; }
    [YamlMember(Alias = "limit")]
    public int limit { get; set; }
    [YamlMember(Alias = "break")]
    public int break_ { get; set; }
}
public class SubItemTrial
{ 
    [YamlMember(Alias = "circle")]
    public List<Array> circle { get; set; }
    [YamlMember(Alias = "mode")]
    public List<Array> mode { get; set; }
}