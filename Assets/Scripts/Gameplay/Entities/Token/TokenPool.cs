using System.Collections;
using Gameplay.ObjectsPool;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace Gameplay.Entities.Token
{
    public class TokenPool : MonoBehaviour
    {
        [SerializeField] private int _poolCount = 3;
        [SerializeField] private bool _autoExpand = true;
        [SerializeField] private Coin _tokenPrefab;
        [SerializeField] private Transform _tokenSpawner;
        [SerializeField] private float _maxTokens;

        [SerializeField] private PhotonView _photonView;
        
        private Pool _pool;
        private System.Random _random = new System.Random();

        [Inject]
        public void Construct(Pool pool)
        {
            _pool = pool;
            CreatePool();
            StartCoroutine(TokenSpawner());
        }
        
        private void CreatePool()
        {
            _pool.CreatePool<Coin>(_tokenPrefab.gameObject,_poolCount,transform);
        }

        private IEnumerator TokenSpawner()
        {
            while (true)
            {

                if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
                {
                    int seed = (int)PhotonNetwork.Time;
                    _photonView.RPC("SetRandomSeed", RpcTarget.AllBuffered, seed);
                    for (int i = 0; i < _maxTokens; i++)
                    {
                        _photonView.RPC("CreateToken",RpcTarget.All);
                        yield return new WaitForSeconds(0.1f);
                    }
                }
                
                break;
            }
        }

        [PunRPC]
        private void SetRandomSeed(int seed)
        {
            _random = new System.Random(seed);
        }
        
        [PunRPC]
        private void CreateToken()
        {
            var token = _pool.GetFreeElement<Coin>();
            var o = token.gameObject;
            var randX = _random.Next(-5, 4);
            var randY = _random.Next(-8, 9);
            o.transform.position = new Vector3(randX, 0, randY);
            //o.transform.rotation = _tokenSpawner.rotation;
        }
    }
}
