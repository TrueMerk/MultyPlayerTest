using Photon.Pun;
using UnityEngine;

namespace Multiplayer
{
    public class SyncTransform : MonoBehaviourPun,IPunObservable
    {
    
        public float lerpSpeed = 5f;

    
        private Vector3 latestPos;
        private Quaternion latestRot;

    
        void Start()
        {
        
            latestPos = transform.position;
            latestRot = transform.rotation;
        }

        void Update()
        {
            // Если объект не принадлежит нам, интерполируем его положение и вращение к последним полученным значениям
            if (!photonView.IsMine)
            {
                transform.position = Vector3.Lerp(transform.position, latestPos, Time.deltaTime * lerpSpeed);
                transform.rotation = Quaternion.Lerp(transform.rotation, latestRot, Time.deltaTime * lerpSpeed);
            }
        }

    
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
       
            if (stream.IsWriting)
            {
                stream.SendNext(transform.position);
                stream.SendNext(transform.rotation);
            }
        
            else if (stream.IsReading)
            {
                latestPos = (Vector3)stream.ReceiveNext();
                latestRot = (Quaternion)stream.ReceiveNext();
            }
        }
    }
}
