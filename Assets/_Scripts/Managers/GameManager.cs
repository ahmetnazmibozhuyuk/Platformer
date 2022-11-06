using UnityEngine;
using TMPro;

namespace Pounds.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public GameObject PlayerObject { get { return playerObject; } }
        [SerializeField]private GameObject playerObject;

        [SerializeField] private TextMeshProUGUI scoreText;

        private float _score;

        public void CoinCollected(float coinValue)
        {
            _score += coinValue;
            scoreText.SetText("Score: "+_score.ToString());
        }
    }
}
