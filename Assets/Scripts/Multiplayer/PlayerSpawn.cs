using System;
using System.Collections.Generic;
using Gameplay.Components;
using Gameplay.Components.Input;
using Gameplay.Entities.Player;
using Gameplay.ObjectsPool;
using Photon.Pun;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using Random = UnityEngine.Random;

namespace Multiplayer
{
    public class PlayerSpawn : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject _canvas;
        [SerializeField] private GameObject _player;
        [SerializeField] private GameObject _joystickPrefab;
        [SerializeField] private GameObject _btnClicked;
        [SerializeField] private BulletPool _bulletPool;
        [SerializeField] private PhotonView _photonView;
        [SerializeField] private float _minX, _minY, _maxX, _maxY,_maxZ,_minZ;

        [SerializeField] private WinPopup _popup;
        


         private void Start()
        {
            Vector2 randomPosition = new Vector3(Random.Range(_minX, _maxX),0);
            
            GameObject playerObject=PhotonNetwork.Instantiate(_player.name, randomPosition, Quaternion.identity);
            
            JoystickInput playerController = playerObject.GetComponent<JoystickInput>();

            
            playerObject.GetComponent<HealthComponent>().SetPopup(_popup);
            
            // создаем новый джойстик для игрока
            GameObject joystickObject = Instantiate(_joystickPrefab,_canvas.transform);
            DynamicJoystick joystick = joystickObject.GetComponent<DynamicJoystick>();
            playerController.SetJoystick(joystick);


                //кнопка для игрока
            GameObject btn = Instantiate(_btnClicked, _canvas.transform);
            BtnClicked btnC = btn.GetComponent<BtnClicked>();

            
            var shooter = playerObject.GetComponent<PlayerShooter>();
            shooter.SetPool(_bulletPool);
           
            playerController.SetBtn(btnC);
            
            
            
        }
         public override void OnJoinedRoom()
         {
             Debug.Log("мы в комнате");
             Vector2 randomPosition = new Vector3(Random.Range(_minX, _maxX),0);
            
             GameObject playerObject=PhotonNetwork.Instantiate(_player.name, randomPosition, Quaternion.identity);
            
             JoystickInput playerController = playerObject.GetComponent<JoystickInput>();
            
            
             // создаем новый джойстик для игрока
             GameObject joystickObject = Instantiate(_joystickPrefab,_canvas.transform);
             DynamicJoystick joystick = joystickObject.GetComponent<DynamicJoystick>();
            
             //кнопка для игрока
             GameObject btn = Instantiate(_btnClicked, _canvas.transform);
             BtnClicked btnC = btn.GetComponent<BtnClicked>();

            
             var shooter = playerObject.GetComponent<PlayerShooter>();
             shooter.SetPool(_bulletPool);
             playerController.SetJoystick(joystick);
             playerController.SetBtn(btnC);
             
         }
        
    }
}
