using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using UnityEngine;

public class CsvParser : SerializedMonoBehaviour
{
    // [ReadOnly][SerializeField] private List<MarkerData> _markersData;
    [ReadOnly][SerializeField] private Dictionary<int, string> _markersDict;
    
    [Button]
    public void LoadData()
    {
        // _markersData = new List<MarkerData>();
        _markersDict = new Dictionary<int, string>();
        ReadFile(Path.Combine(Application.streamingAssetsPath,"markerData.csv"));
    }
    
    public void ReadFile(string filePath)
    {
        using (var parser = new NotVisualBasic.FileIO.CsvTextFieldParser(filePath))
        {
            // Skip the header line
            if (!parser.EndOfData) parser.ReadFields();

            while (!parser.EndOfData)
            {
                var csvLine = parser.ReadFields();
                var id = Convert.ToInt32(csvLine[0]);
                var name = csvLine[1];
                // var markerData = new MarkerData(Convert.ToInt32(csvLine[0]), csvLine[1]);
                // _markersData.Add(markerData);
                if (!_markersDict.TryAdd(id, name))
                {
                    Debug.Log($"<color=red> Contains Key: {id} </color>");
                }
            }
        }
    }
}

[Serializable]
public class MarkerData
{
    public int    id;
    public string name;

    public MarkerData(int id, string name)
    {
        this.id = id;
        this.name = name;
    }
}
