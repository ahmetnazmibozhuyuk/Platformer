using UnityEngine;
using Pounds.Managers;

namespace Pounds.Control
{
    public class CamFollow : MonoBehaviour
    {
        [SerializeField] private float zOffset = 10;
        [SerializeField] private float yOffset = 10;

        private void LateUpdate()
        {
            SetPosition();
        }
        private void SetPosition()
        {
            transform.position = new Vector3(
    GameManager.Instance.PlayerObject.transform.position.x,
    GameManager.Instance.PlayerObject.transform.position.y + yOffset,
    zOffset);
        }
    }
}
