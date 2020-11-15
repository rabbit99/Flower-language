using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionData
{
    public List<string> Args = new List<string>();
    public string Action;
    public string Arg1;
    public string Arg2;
    public string Arg3;
    public string Arg4;
    public string Arg5;
    public string Arg6;
    public string Arg7;
}

public class ActionKey
{
    public static readonly string Say = "say";
    public static readonly string Choice = "choice";
    public static readonly string SetProperty = "setProperty";
    public static readonly string Operation = "operation";
}

public class OperationKey
{
    //public const string ShowInterViewResult = "showInterViewResult";
    //public const string Open = "open";
}

public enum SayArgument
{
    leftRoleName = 0,
    leftRoleMood,
    rightRoleName,
    rightRoleMood,
    characterName,
    content,
    bgImageName,
}

public enum ButtonArgument
{
    buttonText = 0,
    nextIndex,
    waitForClick,
}

public enum PropertyArgument
{
    propertyKey = 0,
    propertyValue,
}

public enum OperationArgument
{
    operationKey = 0,
}

public enum OpenArgument
{
    openMapName = 1,
}
public class CharacterData
{
    public string code;
    public string Charactername;
}
