using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.Text;
using System.IO;
using System;
using System.Threading;

public class SaveCsv : MonoBehaviour {

    public static void SaveData(string folderpath, List<float>time_, List<int> trial_, List<string> state_,
                                List<float>listx_, List<float>listy_)

    {
        string data = "";
        FileStream fs = new FileStream(folderpath, FileMode.Create, FileAccess.Write);
        StreamWriter writer = new StreamWriter(fs);

        writer.WriteLine(string.Format("{0},{1},{2},{3},{4}", "Time", "Trial", "State", "X", "Y"));
        using (var time = time_.GetEnumerator())
        using (var trial = trial_.GetEnumerator())
        using (var state = state_.GetEnumerator())
        using (var ex = listx_.GetEnumerator())
        using (var ey = listy_.GetEnumerator())
        {
            try
            {
                while ((time.MoveNext()) && (trial.MoveNext()) && (state.MoveNext()) && (ex.MoveNext()) && (ey.MoveNext()))
                {
                    var item1 = time.Current;
                    data += item1.ToString();
                    data += ",";
                    var item2 = trial.Current;
                    data += item2.ToString();
                    data += ",";
                    var item3 = state.Current;
                    data += item3.ToString();
                    data += ",";
                    var item4 = ex.Current;
                    data += item4.ToString();
                    data += ",";
                    var item5 = ey.Current;
                    data += item5.ToString();
                    data += ",";
                    data += "\n";
                }
            }
            catch
            {

            }
        }
        writer.Write(data);
        writer.Close();

    }

}
