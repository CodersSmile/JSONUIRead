using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UIDeserialization : MonoBehaviour
{
    public UIObjectData uiData;

    public void LoadChanges()
    {
    
        string json = File.ReadAllText("ui_objects.json");
        uiData = UIObjectData.FromJson(json);
        UpdateUIFromData();
    }

    private void UpdateUIFromData()
    {
        foreach (UIObject obj in uiData.objects)
        {
        }
    }
}