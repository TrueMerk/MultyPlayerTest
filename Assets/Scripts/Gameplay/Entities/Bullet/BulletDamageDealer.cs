using Gameplay.Components;
using Gameplay.Entities.Player;
using Photon.Pun;
using UnityEngine;

namespace Gameplay.Entities.Bullet
{
    public class BulletDamageDealer : MonoBehaviourPunCallbacks
    {


        private int _view;
        
        private void OnTriggerEnter(Collider other)
        {

            var player = other.GetComponent<UnitController>();
            var health = other.GetComponent<HealthComponent>();


            // if (player != null && !player.photonView.IsMine)
            // {
            //     // Вызвать метод TakeDamage у компонента здоровья через RPC
            //     health.photonView.RPC("TakeDamage", RpcTarget.Others);
            //
            //     // Деактивировать пулю
            //     gameObject.SetActive(false);
            // }

            if (player != null && player.photonView.ViewID !=_view )
            {
                health.photonView.RPC("TakeDamage", RpcTarget.All);
            }

        }

        public void SetParentView(int view)
        {
            _view = view;
        }
        
        
    }
}

