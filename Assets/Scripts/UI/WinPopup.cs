using System;
using Multiplayer;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WinPopup : MonoBehaviourPun
    {

        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _nick;
        [SerializeField] private TMP_Text _score;
        [SerializeField] private PlayerSpawn _playerSpawn;


        
        [PunRPC]
        private void ShowWinner(string winnerName , string scoreText)
        {
            _image.gameObject.SetActive(true);
            _nick.text = winnerName;
            _score.text = scoreText;

        }
    }
}
