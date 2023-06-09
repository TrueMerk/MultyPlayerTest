using Photon.Pun;
using UnityEngine;

namespace Gameplay.Components.Input
{
    public abstract class InputComponent : MonoBehaviourPun
    {
        public abstract Vector3 GetMovementDirection();
        public abstract Quaternion GetRotation();
        public abstract bool IsAttacking();
        
    }
}
