using System;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
namespace Multiplayer
{
    public class Connector : MonoBehaviourPunCallbacks
    {
        private void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            SceneManager.LoadScene("Menu");
        }
    }
}

