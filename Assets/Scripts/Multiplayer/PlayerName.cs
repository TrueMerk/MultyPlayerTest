using Photon.Pun;
using UnityEngine;

namespace Multiplayer
{
    public class PlayerName : MonoBehaviour
    {
        public string nickName;
        void Start()
        {
            PhotonNetwork.NickName = gameObject.GetPhotonView().ViewID.ToString();
            nickName = gameObject.GetPhotonView().ViewID.ToString();
        }
    }
}
