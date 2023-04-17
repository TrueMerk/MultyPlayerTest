using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Components;
using Gameplay.Entities.Enemy;
using Gameplay.ObjectsPool;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace Gameplay.Entities.Player
{
    public class PlayerShooter : AttackComponent
    {
        
        [SerializeField] public Transform _shotDir;
        [SerializeField] private float _shootRate;
        private BulletPool _bulletPool;
        private GameObject _playerPos;
        private bool _isReload;
        private List<Enemy.Enemy> _enemies;
        private bool _enemyExist;

        public override bool CanAttack => !_isReload ;
        
        private void Shoot()
        {
            _bulletPool.Fire(transform.position,transform.rotation,this.gameObject.GetPhotonView().ViewID);
           //_bulletPool.photonView.RPC("Fire", RpcTarget.Others,transform.position,transform.rotation);
            StartCoroutine(Reload());
        }

        private IEnumerator Reload()
        {
            _isReload = true;
            yield return new WaitForSeconds(_shootRate);
            _isReload = false;
        }
        
        
        public override void Attack()
        {
            if (!_isReload)
            {
                Shoot();
            }
        }

        public void SetPool(BulletPool bulletPool)
        {
            _bulletPool = bulletPool;
        }
    }
}
