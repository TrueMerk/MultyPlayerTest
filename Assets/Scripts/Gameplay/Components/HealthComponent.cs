using System;
using Gameplay.Entities.Enemy;
using Gameplay.Entities.Player;
using Multiplayer;
using Photon.Pun;
using UI;
using UnityEngine;
using Zenject;

namespace Gameplay.Components
{
   public class HealthComponent : MonoBehaviourPunCallbacks
   {
          [SerializeField] private int _health;
          
          private int _startHealth ;
          public WinPopup _winPopup;

          private UnitController _unitController;
          
          public event Action Dead;

          [Inject]
          public void Construct(UnitController unitController)
          {
              _unitController = unitController;
          }
          
          private void Awake()
          {
              _startHealth = _health;
          }
          
          private void OnEnable()
          {
              _health = _startHealth;
          }

          public int GetHealth()
          {
              return _health;
          }
          
          
          public void SetPopup(WinPopup popup)
          {
              _winPopup=popup;
          }
      
          [PunRPC]
          public void TakeDamage()
          {
              if (_health >1)
                  _health--;
              
              else
              {
                  _health--;
                  var playerIndex = gameObject.GetComponent<PlayerName>().nickName;
                  var score = gameObject.GetComponent<PlayerCoinManager>().GetCoins();
                  if (_winPopup == null)
                  {
                      Debug.Log("_winPopup is null");
                  }
                  else if (_winPopup.photonView == null)
                  {
                      Debug.Log("photonView is null");
                  }
                  else
                  {
                      _winPopup.photonView.RPC("ShowWinner", RpcTarget.All,playerIndex,score.ToString());
                  }
                  
                  gameObject.SetActive(false);
              }
          }
   }
}
