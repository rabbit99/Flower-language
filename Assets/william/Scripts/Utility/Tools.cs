using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tools
{
    public class Json
    {

        static public string ToJson(object obj)
        {
            if (obj is Dictionary<string, object> ||
                obj is Hashtable ||
                obj is Dictionary<string, string> ||
                !obj.GetType().IsSerializable)
            {
                throw new Exception("You have to use a concrete object that uses '[System.Serializable]' to serialize, generic dictionaries will not work!");
            }

            return JsonUtility.ToJson(obj);
        }

        static public T FromJson<T>(string jsonStr, bool strip = false)
        {
            if (typeof(T) is Dictionary<string, object> ||
                typeof(T) is Hashtable ||
                typeof(T) is Dictionary<string, string> ||
                !typeof(T).IsSerializable)
            {
                throw new Exception("You have to use a concrete object that uses '[System.Serializable]' to serialize, generic dictionaries will not work!");
            }

            if (string.IsNullOrEmpty(jsonStr))
            {
                return default(T);
            }

            try
            {
                if (strip)
                {
                    jsonStr = jsonStr.Replace("\\r\\n", "");
                    jsonStr = jsonStr.Replace("\\\"", "\"");
                    jsonStr = jsonStr.Replace("\"[", "[");
                    jsonStr = jsonStr.Replace("]\"", "]");
                }

                //better to use Unity JsonUtility, it's faster.
                T t = JsonUtility.FromJson<T>(jsonStr);
                return t;
            }
            catch (Exception e)
            {
                Debug.LogError(string.Format("Error! '{0}' '{1}'", e.Message, jsonStr));
            }
            return default(T);
        }
    }

    public class File
    {

        static public T LoadJsonObj<T>(string filePath) where T : class
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return null;
            }

            UnityEngine.TextAsset textAsset = Services.Get<ResourcesManager>().GetAsset<TextAsset>(filePath);
            if (textAsset == null)
            {
                return null;
            }

            if (textAsset == null)
            {
                return null;
            }

            if (string.IsNullOrEmpty(textAsset.text))
            {
                return null;
            }

            return Json.FromJson<T>(textAsset.text);
        }
#if SW_SaveGOAL
			static public void SaveObj(string file, string fileName)
			{
				string _filePath = "";

				_filePath = string.Format("{0}{1}d{2}.json", Application.dataPath,"/_szuweiTest/test/", fileName);

				System.IO.File.WriteAllText(_filePath, file);
			}
#endif

    }

    public class ScaleCam
    {
        /// <summary>
        /// Cams the scale action.
        /// </summary>
        /// <param name="_a">16:9 & 16:10 Doing</param>
        /// <param name="_b">3:2 Doing</param>
        /// <param name="_c">4:3 & 5:4 Doing</param>
        public static void CamScaleAction(Action _a = null, Action _b = null, Action _c = null)
        {
            float _camScale = (float)Screen.width / (float)Screen.height;

            if (_camScale >= 1.6f)
            {
                if (_a != null)
                    _a();
            }
            else if (_camScale > 1.4f)
            {
                if (_b != null)
                    _b();
            }
            else
            {
                if (_c != null)
                    _c();
            }
        }
    }

    public class Network
    {
        public static bool CheckNetwork()
        {
            bool _result = false;
            NetworkReachability reachabilty = Application.internetReachability;

            if (reachabilty == NetworkReachability.NotReachable)
                _result = false;
            else
                _result = true;

            return _result;
        }
    }

    public class GameObj
    {
        public static GameObject FindGameObject(Transform parent, string name)
        {
            if (parent != null)
            {
                Transform child;
                GameObject result;
                for (int i = 0; i < parent.childCount; ++i)
                {
                    child = parent.GetChild(i);
                    if (child.name.Equals(name))
                    {
                        return child.gameObject;
                    }
                    else
                    {
                        result = FindGameObject(child, name);
                        if (result != null)
                        {
                            return result;
                        }
                    }
                }
            }
            return null;
        }
    }
    
    public static bool SameListString(List<string> list1, List<string> list2)
    {
        if (null == list1 && null == list2)
            return true;
        if (null == list1 || null == list2)
            return false;
        if (list1.Count != list2.Count || !list1.All(list2.Contains))
            return false;
        list1.Sort();
        list2.Sort();
        int nCount = list1.Count;
        for (int n = 0; n < nCount; n++)
        {
            if (0 != string.Compare(list1[n], list2[n], false))
            {
                return false;
            }
        }
        return true;
    }
    
    /// <summary>
    /// RepeatInfo用来描述重复项
    /// </summary>
    class RepeatInfo
    {
        // 值
        public string Value { get; set; }
        // 重复次数
        public int RepeatNum { get; set; }
    }
    
    public static string GetStringRepeatMost(List<string> list)
    {
        // result集合存放扫描结果
        Dictionary<string, RepeatInfo> result =
                new Dictionary<string, RepeatInfo>();
        
        RepeatInfo RepeatMost = null;
        // 遍历整型列表集合，查找其中的重复项
        foreach (string v in list)
        {
            RepeatInfo item;
            if (result.ContainsKey(v))
            {
                result[v].RepeatNum += 1;
                item = result[v];
            }
            else
            {
                item =
                    new RepeatInfo() { Value = v, RepeatNum = 1 };
                result.Add(v, item);
            }

            if (RepeatMost == null)
            {
                RepeatMost = item;
            }
            else
            {
                if (RepeatMost.RepeatNum <= item.RepeatNum)
                {
                    RepeatMost = item;
                }
            }
        }

        if (RepeatMost != null)
        {
            return RepeatMost.Value;
        }
        else
            return null;
    }
}
