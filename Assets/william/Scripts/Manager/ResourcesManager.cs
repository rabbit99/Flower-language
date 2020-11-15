using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class ResourcePath : Attribute
{
    public string Path { get; private set; }
    public ResourcePath(string path) { Path = path; }
}

public class ResourcesManager
{
    public T[] GetAssets<T>(string path) where T : Object
    {
        return Resources.LoadAll(path, typeof(T)) as T[];
    }

    public T GetAsset<T>(string path) where T : Object
    {
        return Resources.Load<T>(path) as T;
    }

    public T GetAsset<T>() where T : Object
    {
        Type type = typeof(T);
        var pathAttribute = type.GetCustomAttributes(typeof(ResourcePath), false);

        if (pathAttribute.Length == 0)
        {
            Debug.LogError(string.Format("[ResourcesSystem::Spawn] Can't find data path attribute.", type.ToString()));
            return null;
        }
        string path = ((ResourcePath)pathAttribute[0]).Path;

        return Resources.Load<T>(path) as T;
    }

    public T GetAssetCompoment<T>() where T : Component
    {
        Type type = typeof(T);
        var pathAttribute = type.GetCustomAttributes(typeof(ResourcePath), false);

        if (pathAttribute.Length == 0)
        {
            Debug.LogError(string.Format("[ResourcesSystem::Spawn] Can't find data path attribute.", type.ToString()));
            return null;
        }
        string path = ((ResourcePath)pathAttribute[0]).Path;
        GameObject obj = Resources.Load<GameObject>(path);

        return obj.GetComponent<T>();
    }

    public T Spawn<T>() where T : Object
    {
        Type type = typeof(T);
        GameObject obj = Spawn(type);
        return obj.GetComponent<T>();
    }

    public T Spawn<T>(string path) where T : Object
    {
        Type type = typeof(T);
        GameObject obj = Spawn(path);
        return obj.GetComponent<T>();
    }

    public T Spawn<T>(Vector3 position, Quaternion rotation) where T : Object
    {
        Type type = typeof(T);
        GameObject obj = Spawn(type, position, rotation);
        return obj.GetComponent<T>();
    }

    public GameObject Spawn(Type type)
    {
        var pathAttribute = type.GetCustomAttributes(typeof(ResourcePath), false);

        if (pathAttribute.Length == 0)
        {
            Debug.LogError(string.Format("[ResourcesSystem::Spawn] Can't find data path attribute.", type.ToString()));
            return null;
        }
        string path = ((ResourcePath)pathAttribute[0]).Path;

        return Spawn(path);
    }

    public GameObject Spawn(Type type, Vector3 position, Quaternion rotation)
    {
        var pathAttribute = type.GetCustomAttributes(typeof(ResourcePath), false);

        if (pathAttribute.Length == 0)
        {
            Debug.LogError(string.Format("[ResourcesSystem::Spawn] Can't find data path attribute.", type.ToString()));
            return null;
        }
        string path = ((ResourcePath)pathAttribute[0]).Path;
        return Spawn(path, position, rotation);
    }

    public GameObject Spawn(string path, Vector3 position, Quaternion rotation)
    {
        GameObject obj = Spawn(path);

        if (obj != null)
        {
            obj.transform.localPosition = position;
            obj.transform.localRotation = rotation;

            return obj;
        }
        else
            return null;
    }

    public GameObject Spawn(string path)
    {
        // load prefab
        GameObject obj = Resources.Load<GameObject>(path) as GameObject;
        if (obj == null)
        {
            Debug.LogError("[ResourcesSystem::Spawn] Can't spawn object at path: " + path);
            return null;
        }
        // create instance to scene, or can't set to parent
        return Spawn(obj);
    }

    public GameObject Spawn(GameObject obj)
    {
        return ObjectPool.GetObject(obj);
    }

    public void DeSpawn(GameObject obj, bool isReleasePool = false)
    {
        if (ObjectPool.HasPool(obj.name))
        {
            if (isReleasePool)
                ObjectPool.GetObjectPool(obj).Release();
            else
                ObjectPool.GetObjectPool(obj).PushObjectToStore(obj);
        }
        else
            Debug.LogError("[ResourcesSystem::DeSpawn] You can't despawn not in pool system's object. Object name: " + obj.name);
    }
}
