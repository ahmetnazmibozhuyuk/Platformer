using UnityEngine;

namespace Pounds.Control
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerControl : MonoBehaviour
    {
        #region Inputs
        private bool _leftInput { get { return Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A); } }
        private bool _leftInputReleased { get { return Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A); } }
        private bool _rightInput { get { return Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D); ; } }
        private bool _rightInputReleased { get { return Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKey(KeyCode.D); ; } }
        private bool _jumpInput { get { return Input.GetKeyDown(KeyCode.Space); } }
        private bool _jumpInputReleased { get { return Input.GetKeyUp(KeyCode.Space); } }
        #endregion


        [SerializeField] private float movementSpeed = 20f;
        [SerializeField] private float jumpPower = 120f;

        private bool _isMovingLeft;
        private bool _isInAir;

        private Vector3 _currentVelocityVector;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {

            if (CheckIfPlayerOnLand())
            {
                Debug.Log("on land");
            }

            MovementAction();
            JumpAction();



        }
        private void MovementAction()
        {
            if (_leftInput)
            {
                SetVelocityX(-movementSpeed);
                Debug.Log("left input");
                return;
            }
            if (_rightInput)
            {
                SetVelocityX(movementSpeed);
                Debug.Log("right input");
                return;
            }
        }
        private void JumpAction()
        {
            if (_jumpInput)
            {
                SetVelocityY(jumpPower);
                Debug.Log("jump input");
            }
        }

        private void SetVelocityX(float xVelocity)
        {
            _currentVelocityVector.Set(xVelocity, _rigidbody.velocity.y, _rigidbody.velocity.z);
            _rigidbody.velocity = _currentVelocityVector;
        }
        private void SetVelocityY(float yVelocity)
        {
            _currentVelocityVector.Set(_rigidbody.velocity.x, yVelocity, _rigidbody.velocity.z);
            _rigidbody.velocity = _currentVelocityVector;
        }
        private bool CheckIfPlayerOnLand()
        {
            //return Physics.BoxCast(transform.position, transform.position, Vector3.up);
            return Physics.Raycast(transform.position, Vector3.up, -1);
        }
    }
}
