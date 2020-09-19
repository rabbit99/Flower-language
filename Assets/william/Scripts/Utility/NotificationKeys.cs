
using System.Collections.Generic;

public class NotificationKeys
{
	public const string InTheLadder = "InTheLadder";
    public const string OutTheLadder = "OutTheLadder";
    public const string InTheTool = "InTheTool";
    public const string OutTheTool = "OutTheTool";

    public const string InTheFixablePipe = "InTheFixablePipe";
    public const string OutTheFixablePipe = "OutTheFixablePipe";
    public const string RepairFinish = "RepairFinish";
    public const string MonsterHurt = "MonsterHurt";
    public const string RobotHurt = "RobotHurt";
    public const string GameStart = "GameStart";
    public const string GameOver = "GameOver";

    public const string OnFire = "OnFire";
    public const string OutOfFire = "OutOfFire";
}

public class ItemLookUp
{
    public static Dictionary<string, int> ItemLookUpTable = new Dictionary<string, int>
    {
        { "cactus",5 },
    };
}