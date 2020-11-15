using Gamekit2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbIdleSMB : SceneLinkedSMB<PlayerCharacter>
{

    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.CheckForClimbIdle();
    }
}
