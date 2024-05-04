using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class CsvParser : SerializedMonoBehaviour
{
    public static event Action<List<MarkerData>> MarkerDataLoaded;
    
    public List<int> listTopperIDs = new List<int>();
    [ReadOnly][SerializeField] private List<MarkerData> _markersData;

    private void Start()
    {
        LoadData();
    }

    [Button]
    public void LoadData()
    {
        _markersData = new List<MarkerData>();
        ReadFile(Path.Combine(Application.streamingAssetsPath,"markerData.csv"));
    }

    private void ReadFile(string filePath)
    {
        using var parser = new NotVisualBasic.FileIO.CsvTextFieldParser(filePath);
        
        // Skip the header line
        if (!parser.EndOfData) parser.ReadFields();

        while (!parser.EndOfData)
        {
            var csvLine = parser.ReadFields();
            var id = Convert.ToInt32(csvLine[0]);
            var name = csvLine[1];
            var markerData = new MarkerData(Convert.ToInt32(csvLine[0]), csvLine[1]);
            _markersData.Add(markerData);
        }
        
        ProcessData();
        
        MarkerDataLoaded?.Invoke(_markersData);
    }

    private void ProcessData()
    {
        List<MarkerData> _removedMarkers = new List<MarkerData>();
        
        // Check and remove markers that should be at the top
        for (int i = _markersData.Count - 1; i >= 0; i--)
        {
            if (listTopperIDs.Contains(_markersData[i].id))
            {
                _removedMarkers.Add(_markersData[i]);
                _markersData.RemoveAt(i);
            }
        }

        // _markersData = _markersData.OrderBy(m => m.name).ToList();
        _markersData.Sort((x, y) => String.Compare(x.name, y.name, StringComparison.Ordinal));
        
        
        // Insert the removed markers at the beginning of the list
        _removedMarkers.Reverse();
        _markersData.InsertRange(0, _removedMarkers);
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
