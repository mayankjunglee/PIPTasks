using System;
using UnityEngine;
public class ResourceReader
{
    public T ReadFromResource<T>(string filename) where T: UnityEngine.Object
    {
        try
        {
            T t =Resources.Load<T>(filename);
            return t;
        }
        catch(Exception e)
        {
            Debug.LogError($"FilePath {filename} does not exists");
            return default(T);
        }
    }
    public void UnloadResource<T>(T resource) where T: UnityEngine.Object
    {
        if(resource!=null)
        {
            Resources.UnloadAsset(resource);
        }
    }
}