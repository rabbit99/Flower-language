using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    #region Static Member

    private static GameObject _poolParent = null;
    private static Dictionary<string, ObjectPool> _poolMap = new Dictionary<string, ObjectPool>();
    private static int defaultPreloadCount = 1;

    #endregion

    private GameObject _object;
    private List<GameObject> _objects = new List<GameObject>();
    private Queue<GameObject> _deSpawnedObjects = new Queue<GameObject>();
    private int _preloadCount;
    private bool _autoResize;
    private bool _dontDestroyOnLoad;

    private Vector3 _defaultPosition;
    private Vector3 _defaultScale;
    private Quaternion _defaultRotation;

    private const string _objectPoolName = "[ObjectPool] ";
    private const string _spawnedMethod = "OnSpawned";
    private const string _deSpawnedMethod = "OnDespawned";
    private const string _unityCloneObjectName = "(Clone)";

    #region Static Method

    public static bool HasPool(string name)
    {
        return _poolMap.ContainsKey(name);
    }

    public static GameObject GetObject(GameObject obj)
    {
        if (obj != null)
        {
            GameObject spawnObj = GetObjectPool(obj).GetObject();
            return spawnObj;
        }
        else
        {
            Debug.LogError("[ObjectPool::GetObject] GameObject can not be null.");
            return null;
        }
    }

    public static ObjectPool GetObjectPool(GameObject obj)
    {
        ObjectPool objectPool = null;

        //TODO: Must to fix init pool vaule must from somewhere.
        if (!_poolMap.TryGetValue(obj.name, out objectPool))
            objectPool = CreateObjectPool(obj, defaultPreloadCount, true, true);
        return objectPool;
    }

    private static void RemovePoolInMap(GameObject obj)
    {
        _poolMap.Remove(obj.name);
    }

    private static ObjectPool CreateObjectPool(GameObject obj, int preloadCount, bool dontDestroyOnLoad, bool autoResize = true)
    {
        //if pool system root not create yet, Create pool system root.
        if (_poolParent == null)
            _poolParent = new GameObject("[Pools]");

        //if object is not in pool system, then create it.
        if (!_poolMap.ContainsKey(obj.name))
        {
            GameObject objectPool = new GameObject(_objectPoolName + obj.name);
            objectPool.transform.SetParent(_poolParent.transform, false);
            ObjectPool objectPoolComp = objectPool.AddComponent<ObjectPool>();
            objectPoolComp.Init(obj, preloadCount, dontDestroyOnLoad, autoResize);
            _poolMap.Add(obj.name, objectPoolComp);
        }
        return _poolMap[obj.name];
    }

    #endregion

    public void Init(GameObject prefab, int preloadCount, bool dontDestroyOnLoad, bool autoResize = true)
    {
        _dontDestroyOnLoad = dontDestroyOnLoad;
        _preloadCount = preloadCount;
        _autoResize = autoResize;
        _object = prefab;
        _defaultPosition = _object.transform.localPosition;
        _defaultScale = _object.transform.localScale;
        _defaultRotation = _object.transform.localRotation;

        if (_dontDestroyOnLoad)
        {
            DontDestroyOnLoad(this.gameObject.transform.parent);
        }

        for (int i = 0; i < _preloadCount; ++i)
        {
            GameObject newObject = CreateObject(prefab);
            _objects.Add(newObject);
            _deSpawnedObjects.Enqueue(newObject);
        }
    }

    public GameObject GetObject()
    {
        GameObject returnObject = null;

        if (_deSpawnedObjects.Count > 0)
        {
            returnObject = _deSpawnedObjects.Dequeue();
        }
        else
        {
            if (_autoResize)
            {
                returnObject = CreateObject(_object);
                _objects.Add(returnObject);
            }
        }

        returnObject.SetActive(true);
        returnObject.transform.SetParent(null, false);
        returnObject.SendMessage(_spawnedMethod, SendMessageOptions.DontRequireReceiver);
        return returnObject;
    }

    public void PushObjectToStore(GameObject obj)
    {
        if (_objects.Contains(obj))
        {
            obj.transform.SetParent(this.gameObject.transform, false);
            obj.transform.localPosition = _defaultPosition;
            obj.transform.localRotation = _defaultRotation;
            obj.transform.localScale = _defaultScale;
            obj.SendMessage(_deSpawnedMethod, SendMessageOptions.DontRequireReceiver);
            obj.SetActive(false);
            _deSpawnedObjects.Enqueue(obj);
        }
    }

    public void Release()
    {
        // under other parent, should do destroy self
        foreach (var obj in _objects)
            Destroy(obj);
        _objects.Clear();
        // destroy current container
        _deSpawnedObjects.Clear();
        Destroy(gameObject);
        RemovePoolInMap(_object);
        _object = null;// release reference
    }

    private GameObject CreateObject(GameObject prefab)
    {
        GameObject storeObject = UnityEngine.GameObject.Instantiate(prefab);

        if (_dontDestroyOnLoad)
            DontDestroyOnLoad(storeObject);

        storeObject.name = storeObject.name.Replace(_unityCloneObjectName, null);
        storeObject.transform.SetParent(this.gameObject.transform, false);
        storeObject.SetActive(false);

        return storeObject;
    }
}
