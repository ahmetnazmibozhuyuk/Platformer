using UnityEngine;

namespace Pounds.Control
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerControl : MonoBehaviour
    {
        #region Inputs
        private bool _leftInput { get { return Input.GetKey(KeyCode.LeftArrow); } }
        private bool _rightInput { get { return Input.GetKey(KeyCode.RightArrow); } }
        private bool _jumpInput { get { return Input.GetKeyDown(KeyCode.Space); } }
        #endregion


        [SerializeField] private float movementSpeed;
        [SerializeField] private float jumpPower;

        private bool _isMovingLeft;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            MovementAction();
            JumpAction();



        }

        private void MovementAction()
        {
            if (_leftInput)
            {
                _rigidbody.AddForce(-30, 0, 0);
                Debug.Log("left input");
                return;
            }
            if (_rightInput)
            {
                _rigidbody.AddForce(30, 0, 0);
                Debug.Log("right input");
                return;
            }
        }
        private void JumpAction()
        {
            if (_jumpInput)
            {
                _rigidbody.AddForce(Vector3.up * jumpPower);
                Debug.Log("jump input");
            }
        }
    }
}
