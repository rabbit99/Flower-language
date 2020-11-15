using UnityEngine;
using UnityEditor;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
/// 
public partial class GoogleDataAssetUtility
{
    [MenuItem("Assets/Create/Google/talkData")]
    public static void CreatetalkDataAssetFile()
    {
        talkData asset = CustomAssetUtility.CreateAsset<talkData>();
        asset.SheetName = "花語對話文本";
        asset.WorksheetName = "talkData";
        EditorUtility.SetDirty(asset);        
    }
    
}