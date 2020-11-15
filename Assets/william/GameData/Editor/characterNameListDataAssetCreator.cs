using UnityEngine;
using UnityEditor;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
/// 
public partial class GoogleDataAssetUtility
{
    [MenuItem("Assets/Create/Google/characterNameListData")]
    public static void CreatecharacterNameListDataAssetFile()
    {
        characterNameListData asset = CustomAssetUtility.CreateAsset<characterNameListData>();
        asset.SheetName = "花語對話文本";
        asset.WorksheetName = "characterNameListData";
        EditorUtility.SetDirty(asset);        
    }
    
}