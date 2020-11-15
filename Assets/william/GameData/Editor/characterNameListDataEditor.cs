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
[CustomEditor(typeof(characterNameListData))]
public class characterNameListDataEditor : BaseGoogleEditor<characterNameListData>
{	    
    public override bool Load()
    {        
        characterNameListData targetData = target as characterNameListData;
        
        var client = new DatabaseClient("", "");
        string error = string.Empty;
        var db = client.GetDatabase(targetData.SheetName, ref error);	
        var table = db.GetTable<characterNameListDataData>(targetData.WorksheetName) ?? db.CreateTable<characterNameListDataData>(targetData.WorksheetName);
        
        List<characterNameListDataData> myDataList = new List<characterNameListDataData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            characterNameListDataData data = new characterNameListDataData();
            
            data = Cloner.DeepCopy<characterNameListDataData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
