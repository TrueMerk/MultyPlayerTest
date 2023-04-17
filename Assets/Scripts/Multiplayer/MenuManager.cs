using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using Zenject;

namespace Multiplayer
{
    public class MenuManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] private TMP_InputField createField;
        [SerializeField] private TMP_InputField joinField;

        [SerializeField] private byte _maxPlayers;

        public void CreateRoom()
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = _maxPlayers;

            PhotonNetwork.CreateRoom(createField.text, roomOptions);
            
        }
        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinLobby();
        }
        
        public void JoinRoom()
        {
            PhotonNetwork.JoinRoom(joinField.text);
        }

        public override void OnJoinedRoom()
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount > 0)
            {
                PhotonNetwork.LoadLevel("Game");
            }
        }

    }
}
