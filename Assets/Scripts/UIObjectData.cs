using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UIObjectData
{
    public List<UIObject> objects;

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

   
    public static UIObjectData FromJson(string json)
    {
        return JsonUtility.FromJson<UIObjectData>(json);
    }
}

[System.Serializable]
public class UIObject
{
    public string name;
    public List<UIComponent> components;
}

[System.Serializable]
public class UIComponent
{
    public string type;
    public UIComponentProperties properties;
}
[System.Serializable]
public class UIComponentProperties
{
    public Vector2 position;
    public string text;
    public Color color; 
    public float cornerRadius;
    public string name;
   
}
