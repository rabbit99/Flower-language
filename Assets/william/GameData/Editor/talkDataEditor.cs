using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using GDataDB;
using GDataDB.Linq;

using UnityQuickSheet;

///
/// !!! Machine generated code !!!
///
[CustomEditor(typeof(talkData))]
public class talkDataEditor : BaseGoogleEditor<talkData>
{	    
    public override bool Load()
    {        
        talkData targetData = target as talkData;
        
        var client = new DatabaseClient("", "");
        string error = string.Empty;
        var db = client.GetDatabase(targetData.SheetName, ref error);	
        var table = db.GetTable<talkDataData>(targetData.WorksheetName) ?? db.CreateTable<talkDataData>(targetData.WorksheetName);
        
        List<talkDataData> myDataList = new List<talkDataData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            talkDataData data = new talkDataData();
            
            data = Cloner.DeepCopy<talkDataData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
