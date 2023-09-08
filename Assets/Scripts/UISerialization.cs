using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UISerialization : MonoBehaviour
{
    public UIObjectData uiData;

    public void SaveChanges()
    {
       
        string json = uiData.ToJson();
        File.WriteAllText("uiObjects.json", json);
    }
}