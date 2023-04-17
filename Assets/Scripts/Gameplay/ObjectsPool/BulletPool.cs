using System;
using Gameplay.Entities.Bullet;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace Gameplay.ObjectsPool
{
    public class BulletPool : MonoBehaviourPun
    {
        [SerializeField] private int _poolCount = 20;
        [SerializeField] private BulletDamageDealer _bulletPrefab;
        
        private Pool _pool;

        public PhotonView View;
        
        [Inject]
        public void Construct(Pool pool)
        {
            _pool = pool;
            CreatePool();
        }
        

        private void CreatePool()
        {
            _pool.CreatePool<BulletDamageDealer>(_bulletPrefab.gameObject,_poolCount,transform);
        }

        
        public void Fire(Vector3 position,Quaternion rotation,int view)
        {
            View.RPC("CreateBullet", RpcTarget.All, position, rotation,view);
            Debug.Log(view);
        }
        
        [PunRPC]
        private void CreateBullet(Vector3 bulletSpawnerPosition,Quaternion bulletSpawnerRotation,int view)
        {
            
            var dd = _pool.GetFreeElement<BulletDamageDealer>();
            dd.SetParentView(view);
            var o = dd.gameObject;
            o.transform.position = bulletSpawnerPosition;
             o.transform.rotation = bulletSpawnerRotation;
             o.transform.SetParent(null);

            
            
        }
    }
}
