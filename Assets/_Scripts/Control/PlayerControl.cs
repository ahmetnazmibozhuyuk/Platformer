using UnityEngine;
using DG.Tweening;

namespace Pounds.Control
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerControl : MonoBehaviour
    {
        #region Inputs
        private bool _leftInput { get { return Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A); } }
        private bool _leftInputReleased { get { return Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A); } }
        private bool _rightInput { get { return Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D); ; } }
        private bool _rightInputReleased { get { return Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D); ; } }
        private bool _jumpInput { get { return Input.GetKeyDown(KeyCode.Space); } }
        private bool _jumpInputReleased { get { return Input.GetKeyUp(KeyCode.Space); } }
        #endregion

        [SerializeField] private Transform bodyTransform;
        [SerializeField] private float movementSpeed = 20f;
        [SerializeField] private float jumpPower = 120f;

        [SerializeField] private Transform leftGroundCheckTransform;
        [SerializeField] private Transform rightGroundCheckTransform;

        [SerializeField] private Transform leftTopCheckTransform;
        [SerializeField] private Transform rightTopCheckTransform;

        private bool _isMovingLeft;
        private bool _isOnGround;

        private Tweener _bodyTween;

        private Vector3 _currentVelocityVector;

        private Rigidbody _rigidbody;

        [SerializeField] private float maxJumpInputPressTime = 1f;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            MovementAction();
            JumpAction();
            CheckIfPlayerOnLand();
        }
        private void MovementAction()
        {
            if (_leftInputReleased || _rightInputReleased)
            {
                SetVelocityX(0);
                return;
            }
            if (_leftInput && (_isOnGround || !CheckLeftSide()))
            {
                CheckMovementRotation();
                SetVelocityX(-movementSpeed);
                _isMovingLeft = true;
                return;
            }
            if (_rightInput && (_isOnGround || !CheckRightSide()))
            {
                CheckMovementRotation();
                SetVelocityX(movementSpeed);
                _isMovingLeft = false;
                return;
            }
        }

        // jump multiplier eklenecek input süresine göre ayarlanacak þekilde

        private void JumpAction()
        {
            if (_jumpInput && _isOnGround)
            {
                SetVelocityY(jumpPower);
            }
        }
        private void CheckIfPlayerOnLand()
        {
            _isOnGround = Physics.Raycast(leftGroundCheckTransform.position, Vector3.up * -1, 0.1f) ||
                Physics.Raycast(rightGroundCheckTransform.position, Vector3.up * -1, 0.1f);
        }
        private bool CheckLeftSide()
        {
            return Physics.Raycast(leftGroundCheckTransform.position, Vector3.left, 0.2f);
        }
        private bool CheckRightSide()
        {
            return Physics.Raycast(rightGroundCheckTransform.position, Vector3.right, 0.2f);
        }

        #region Set Velocity
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
        #endregion

        private void CheckMovementRotation()
        {
            if (_isMovingLeft)
            {
                _bodyTween = bodyTransform.DOLocalRotate(new Vector3(0, 180, 0), 0.3f);
            }
            else
            {
                _bodyTween = bodyTransform.DOLocalRotate(new Vector3(0, 0, 0), 0.3f);
            }
        }
    }
}
