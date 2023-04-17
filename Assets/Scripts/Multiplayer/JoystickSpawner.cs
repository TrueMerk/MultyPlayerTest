using UnityEngine;
using Photon.Pun;

namespace Multiplayer
{
    public class JoystickSpawner : MonoBehaviourPunCallbacks
    {
        
        [SerializeField] private GameObject joystickPrefab;

       
        public override void OnJoinedRoom()
        {
            joystickPrefab.SetActive(true);
            Debug.Log("Joined");
        }
        
    }
}
