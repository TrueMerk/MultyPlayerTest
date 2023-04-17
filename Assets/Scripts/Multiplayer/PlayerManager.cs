using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UI;
using UnityEngine;

namespace Multiplayer
{
    public class PlayerManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] private WinPopup _winPopup;

        private const byte PlayerDeathEventCode = 1;
        
        private bool[] alivePlayers;
        
        

        private void Start()
        {
            
            alivePlayers = new bool[PhotonNetwork.PlayerList.Length];
            for (int i = 0; i < alivePlayers.Length; i++)
            {
                alivePlayers[i] = true;
            }
        }

        
        public void OnPlayerDeath(string playerIndex)
        {
            
            // alivePlayers[playerIndex] = false;
            //
            // object[] data = new object[] { playerIndex };
            // RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            // PhotonNetwork.RaiseEvent(PlayerDeathEventCode, data, raiseEventOptions, SendOptions.SendReliable);
            //CheckPlayers();
            _winPopup.photonView.RPC("ShowWinner", RpcTarget.All,playerIndex);
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            
            int playerIndex = otherPlayer.ActorNumber - 1;
            alivePlayers[playerIndex] = false;
        }

        public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
        {
            base.OnRoomPropertiesUpdate(propertiesThatChanged);
            CheckPlayers();
        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
        {
            base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
            CheckPlayers();
        }

        public void OnEvent(EventData photonEvent)
        {
            if (photonEvent.Code == PlayerDeathEventCode)
            {
                
                object[] data = (object[])photonEvent.CustomData;
                int playerIndex = (int)data[0];
                alivePlayers[playerIndex] = false;

                
                CheckPlayers();
            }
        }


        private void CheckPlayers()
        {
            // Count the number of alive players
            int alivePlayerCount = 0;
            int lastAlivePlayerIndex = -1;
            for (int i = 0; i < alivePlayers.Length; i++)
            {
                if (alivePlayers[i])
                {
                    alivePlayerCount++;
                    lastAlivePlayerIndex = i;
                }
            }

            if (alivePlayerCount == 1)
            {
                // Get the name of the winner
                string winnerName = "";
                foreach (Player player in PhotonNetwork.PlayerList)
                {
                    if (player.ActorNumber == lastAlivePlayerIndex + 1)
                    {
                        winnerName = player.NickName;
                        break;
                    }
                }
                Debug.Log("Only one player is left alive!");
                //_winPopup.photonView.RPC("ShowWinner", RpcTarget.All);
            }
        }
    }
}

