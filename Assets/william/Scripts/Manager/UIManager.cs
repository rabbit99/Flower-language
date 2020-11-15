using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private Dictionary<string, GameObject> _uis = new Dictionary<string, GameObject>();

    private Canvas _worldCanvas;
    private Canvas _canvas;

    public UIManager(Canvas worldCanvas, Canvas canvas)
    {
        _worldCanvas = worldCanvas;
        _canvas = canvas;
    }

    public T OpenUI<T>(string uiName, int siblingIndex = -1)
    {
        GameObject uiObj = findUI(uiName, _canvas);

        if (uiObj == null)
            return default(T);
        else
            uiObj.SetActive(true);

        if (siblingIndex == -1)
        {
            uiObj.transform.SetAsLastSibling();
        }
        else
        {
            uiObj.transform.SetSiblingIndex(siblingIndex);
        }

        return uiObj.GetComponent<T>();
    }

    public T OpenWorldUI<T>(string uiName, int siblingIndex = -1)
    {
        GameObject uiObj = findUI(uiName, _worldCanvas);

        if (uiObj == null)
            return default(T);

        if (siblingIndex == -1)
        {
            uiObj.transform.SetAsLastSibling();
        }
        else
        {
            uiObj.transform.SetSiblingIndex(siblingIndex);
        }
        
        return uiObj.GetComponent<T>();
    }

    private GameObject findUI(string uiName, Canvas _targetCanvas)
    {
        GameObject uiObj;

        if (!_uis.ContainsKey(uiName))
        {
            uiObj = Services.Get<ResourcesManager>().Spawn("UI/" + uiName);
            if (uiObj != null)
            {
                uiObj.transform.SetParent(_targetCanvas.transform, false);
                _uis.Add(uiName, uiObj);
            }
            else
            {
                Debug.LogError("[UISystem::OpenUI] Can't find prefab at " + uiName);
                return null;
            }
        }
        else
        {
            uiObj = _uis[uiName];
        }
        uiObj.SetActive(true);
        return uiObj;
    }

    public void CloseUI(GameObject uiObj)
    {
        uiObj.SetActive(false);
    }

}
