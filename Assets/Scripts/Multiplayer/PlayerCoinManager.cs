using Photon.Pun;
using UnityEngine;

namespace Multiplayer
{
    public class PlayerCoinManager : MonoBehaviour
    {
        public PhotonView photonView;
        private int coins = 0;

        private void Start() 
        {
            photonView = GetComponent<PhotonView>();
            if (photonView.IsMine) 
            {
                SetCoins(0);
            }
        }

        [PunRPC]
        private void AddCoin() 
        {
            
            SetCoins(coins + 1);
            Debug.Log("монетка собрана");
        }

        public int GetCoins()
        {
            Debug.Log(coins);
            return coins;
        }

        private void SetCoins(int value) 
        {
            coins = value;
            ExitGames.Client.Photon.Hashtable properties = new ExitGames.Client.Photon.Hashtable();
            properties.Add("coins", coins);
            PhotonNetwork.LocalPlayer.SetCustomProperties(properties);
        }

        private void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps) {
            if (!photonView.IsMine) {
                Photon.Realtime.Player player = playerAndUpdatedProps[0] as Photon.Realtime.Player;
                ExitGames.Client.Photon.Hashtable props = playerAndUpdatedProps[1] as ExitGames.Client.Photon.Hashtable;
                if (player != null && props != null && player.ActorNumber == photonView.OwnerActorNr) {
                    if (props.ContainsKey("coins")) {
                        coins = (int)props["coins"];
                    }
                }
            }
        }
    }
}
