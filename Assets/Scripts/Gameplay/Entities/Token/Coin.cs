using System;
using Gameplay.Entities.Player;
using Multiplayer;
using Photon.Pun;
using UnityEngine;

namespace Gameplay.Entities.Token
{
    public class Coin : MonoBehaviourPun
    {
        void OnTriggerEnter(Collider other)
        {
            var unit = other.GetComponent<UnitController>();
            
            if (unit!=null) 
            { 
                var playerCoinManager = other.GetComponent<PlayerCoinManager>();
                
                if (playerCoinManager != null ) 
                {
                    playerCoinManager.photonView.RPC("AddCoin", RpcTarget.All);
                }
                
                gameObject.SetActive(false);
            }
        }

        

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                // Отправка локальной позиции
                stream.SendNext(transform.position);
            }
            else
            {
                // Получение удаленной позиции
                transform.position = (Vector3)stream.ReceiveNext();
            }
        }
    }
    
}
