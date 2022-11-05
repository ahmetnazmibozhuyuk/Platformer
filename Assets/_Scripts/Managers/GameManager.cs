using UnityEngine;

namespace Pounds.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public GameObject PlayerObject { get { return playerObject; } }
        [SerializeField]private GameObject playerObject;



    }
}
