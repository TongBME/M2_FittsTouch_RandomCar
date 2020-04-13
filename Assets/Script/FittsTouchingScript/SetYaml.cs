using UnityEngine;
using System;
using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using YamlDotNet.Serialization;

public class SetYaml : MonoBehaviour
{
    public static int circleTimeSet;
    public static int taskTimeSet;
    public static int breakTimeSet;
    public static int trialNum;
    public static List<float> randPos;
    public static List<float> circleSize;

    internal class FittsTask
    {
        public string changeDate { get; set; }
        public string taskName { get; set; }
        public int touchTime { get; set; }
        public int timeOut { get; set; }
        public int breakTime { get; set; }
        public List<float> circlePosition { get; set; }
        public List<float> circleSize { get; set; }
    }

    public static void IniTaskInfo()
    {
        // set task key imfo in .yaml
        var fittsData = new FittsTask
        {
            changeDate = DateTime.Now.ToUniversalTime().ToString(),
            taskName = "Fitts Touching Experiment",
            touchTime = 3,
            timeOut = 10,
            breakTime = 2,
            circlePosition = new List<float>()
            {
                1,2,3,4,5

            },             
            circleSize = new List<float>()
            {
                1f, 1.2f, 1.4f, 0.8f, 0.6f
            }
        };
        
        // serialization process
        var serializer = new Serializer();
        var fittsYaml = serializer.Serialize(fittsData);
        Debug.LogFormat("new serialization was created in .yaml file:\n{0}", fittsYaml);
        // save .yaml
        string TextName = System.Environment.CurrentDirectory + "\\reaching-exp\\" + GlobalVar.YAMLNAME + ".yaml";
        using (TextWriter writer = File.CreateText(TextName))
        {
            writer.Write(fittsYaml.ToString());
        }
    }

    public static void LoadTaskInfo()
    {
        // read
        string yamlName = System.Environment.CurrentDirectory + "\\reaching-exp\\" + GlobalVar.YAMLNAME + ".yaml";
        string content = File.ReadAllText(yamlName);
        var input = new StringReader(content);
        var deserializer = new DeserializerBuilder().Build();
        var yamlObject = deserializer.Deserialize<FittsTask>(input);
        // task parameter
        circleTimeSet = yamlObject.touchTime;
        taskTimeSet = yamlObject.timeOut;
        breakTimeSet = yamlObject.breakTime;
        randPos = yamlObject.circlePosition;
        circleSize = yamlObject.circleSize;

        if (randPos.ToArray().Length >= circleSize.ToArray().Length)
        {
            trialNum = circleSize.ToArray().Length;
        }
        else
        {
            trialNum = randPos.ToArray().Length;
        }
        
        Debug.Log("Task information changed time : " + yamlObject.changeDate);
        Debug.Log("Task name : " + yamlObject.taskName);
        Debug.Log("Touch time : " + yamlObject.touchTime);
        Debug.Log("Task time limit : " + yamlObject.timeOut);
        Debug.Log("Break time : " + yamlObject.breakTime);
        Debug.Log("Trial number : " + yamlObject.circlePosition.ToArray().Length);
    }
   

}
