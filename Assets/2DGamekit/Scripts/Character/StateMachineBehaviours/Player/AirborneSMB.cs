using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamekit2D
{
    public class AirborneSMB : SceneLinkedSMB<PlayerCharacter>
    {
        private int jumpConut = 0;

        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            jumpConut = m_MonoBehaviour.OriJumpConut;
            m_MonoBehaviour.TeleportToColliderBottom();
        }
        public override void OnSLStateNoTransitionUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            m_MonoBehaviour.UpdateFacing();
            m_MonoBehaviour.UpdateJump();
            m_MonoBehaviour.AirborneHorizontalMovement();
            m_MonoBehaviour.AirborneVerticalMovement();
            m_MonoBehaviour.CheckForGrounded();
            m_MonoBehaviour.CheckForHoldingGun();
            //if(m_MonoBehaviour.CheckForMeleeAttackInput())
            //    m_MonoBehaviour.MeleeAttack ();
            m_MonoBehaviour.CheckAndFireGun ();

            if (m_MonoBehaviour.CheckForJumpInput() && jumpConut > 0)
            {
                //Debug.Log("jump again , conut = "+ jumpConut);
                jumpConut--;
                m_MonoBehaviour.SetLastStandPosY();
                m_MonoBehaviour.SetVerticalMovement(m_MonoBehaviour.jumpSpeed);
            }

            m_MonoBehaviour.CheckForCrouching ();
            m_MonoBehaviour.CheckForClimbed();


        }
    }
}