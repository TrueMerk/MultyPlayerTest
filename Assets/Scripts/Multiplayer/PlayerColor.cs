using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Multiplayer
{
    public class PlayerColor : MonoBehaviourPun
    {
        [SerializeField] private MeshRenderer _mesh;

        private void Start()
        {
            var randomColor = Random.ColorHSV();

            gameObject.GetComponent<PlayerColor>().ChangeColor(randomColor);
        }

        [PunRPC]
        private void SetColor(float r, float g, float b)
        {
            var color = new Color(r, g, b);
            foreach (var material in _mesh.materials)
            {
                material.color = color;
            }
        }

        private void ChangeColor(Color color)
        {
            photonView.RPC("SetColor", RpcTarget.AllBuffered, color.r, color.g, color.b);
        }
    }
}
