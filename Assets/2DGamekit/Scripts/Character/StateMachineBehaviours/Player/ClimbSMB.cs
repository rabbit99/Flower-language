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

            animator.SetBool("Grounded", true);
        }

        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //m_MonoBehaviour.UpdateFacing();
            m_MonoBehaviour.CheckForCrouching();
            //m_MonoBehaviour.CheckForHoldingGun();
            //m_MonoBehaviour.CheckForGrounded();
            m_MonoBehaviour.CheckForClimbed();
            m_MonoBehaviour.CheckForClimbIdle();
        }

        public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.speed = 1;
        }
    }
}