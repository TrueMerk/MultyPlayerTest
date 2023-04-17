using System;
using System.Collections.Generic;
using Gameplay.Components;
using Gameplay.Components.Input;
using UnityEngine;
using Photon.Pun;

namespace Gameplay.Entities.Player
{
    public class UnitController : MonoBehaviourPun
    {
        [SerializeField] private MovementComponent _movementComponent;
        [SerializeField] private InputComponent _inputComponent;
        [SerializeField] private AnimationComponent _animationComponent;
        [SerializeField] private AttackComponent _attackComponent;
        
        [SerializeField] private PhotonView _view;
        
        public void Update()
        {
            var direction = _inputComponent.GetMovementDirection();
           
            var rotation = _inputComponent.GetRotation();
            
            var isAttack = _inputComponent.IsAttacking();
            var canAttack = _attackComponent.CanAttack;
            
            if (_view.IsMine)
            {
               
                _movementComponent.Rotate(rotation);
                if (direction!=Vector3.zero)
                {
                    //_animationComponent.PlayRunningAnimation();
                    _movementComponent.Move(Vector3.forward*1000*Time.deltaTime);
                }
        
                else if (!canAttack || !isAttack)
                {
                    // _animationComponent.PlayIdleAnimation();
                }
        
                if(canAttack && isAttack)
                {
                    //_animationComponent.PlayAttackAnimation();
                    _attackComponent.Attack();
                }

            }
            
        }

        
    }
}
