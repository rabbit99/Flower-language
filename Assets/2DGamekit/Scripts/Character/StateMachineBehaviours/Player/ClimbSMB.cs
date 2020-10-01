using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Gamekit2D
{
    public class ClimbSMB : SceneLinkedSMB<PlayerCharacter>
    {
        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //m_MonoBehaviour.TeleportToColliderBottom();
        }

        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //m_MonoBehaviour.UpdateFacing();
            m_MonoBehaviour.CheckForCrouching();
            //m_MonoBehaviour.CheckForHoldingGun();
            m_MonoBehaviour.CheckForGrounded();
            m_MonoBehaviour.CheckForClimbed();
            //if (m_MonoBehaviour.CheckForFallInput())
            //    m_MonoBehaviour.MakePlatformFallthrough();
            //m_MonoBehaviour.GroundedVerticalMovement();
            //m_MonoBehaviour.GroundedHorizontalMovement(false);
        }
    }
}