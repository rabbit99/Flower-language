using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EGameEvents
{
    Invalid = 200,

    GameInitStart,
    GameInitFinished,
    StartupInitFinished,

    GameStart,
    GameOver,
    
    MainGame_Update,
    MainGame_FixedUpdate,
    MainGame_LateUpdate,

    EnterSniperGameState,
    LeaveSniperGameState,

    EnterLustSniperGameState,

    EnterHintwallState,
    LeaveHintwallState,
    SetLeaveHintwallStateMethod,

    OpenBulletTableUI,
    CloseBulletTableUI,

    StartWaveGame,
    StopWaveGame,
    SwitchTargetMode,
    SniperGameEnd,
    SetSniperGamePause,
    SniperGameEndFinished,
    SetLeaveSniperGameStateMethod,

    GotTheClue,
    GotTheClueAfterNpcTalked,
    TalkToNpc,
    TransferMap,
    ChangedState,
    OpenUI,
    Action,

    SetPlayerControl,
    SetSniperGameAccessPermission,
    SetBulletTableAccessPermission,
    SetHintwallStateAccessPermission,
    SetDoorAccessPermission,
    SetNpcAccessPermission,
    SetClueAccessPermission,
    SetBigMapBtnAccessPermission,
    SetMapCMCamera,
    SetCurrentPlace,
    SetState,
    SelectPlaceTo,

    SetKarmaValue,
    ShowLust,
    StartLustFight,
    OpenSurroundingAnimator,

    MapGameEnd,

    SetSkill,
    ShowDiary,
}
