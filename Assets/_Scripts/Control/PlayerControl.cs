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

        private bool _isMovingLeft;
        private bool _isInAir;

        private Tweener _bodyTween;

        private Vector3 _currentVelocityVector;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            MovementAction();
            JumpAction();
            Debug.Log(CheckForward());

        }
        private void MovementAction()
        {
            if (_leftInputReleased || _rightInputReleased)
            {
                SetVelocityX(0);
                return;
            }
            if (_leftInput && !CheckForward())
            {
                CheckMovementRotation();
                SetVelocityX(-movementSpeed);
                _isMovingLeft = true;
                return;
            }
            if (_rightInput && !CheckForward())
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
            if (_jumpInput && CheckIfPlayerOnLand())
            {
                SetVelocityY(jumpPower);
            }
        }
        private bool CheckIfPlayerOnLand()
        {
            return Physics.Raycast(leftGroundCheckTransform.position, Vector3.up * -1, 0.1f) ||
                Physics.Raycast(rightGroundCheckTransform.position, Vector3.up * -1, 0.1f);
        }
        private bool CheckForward()
        {
            if (_isMovingLeft)
            {
                return Physics.Raycast(rightGroundCheckTransform.position, Vector3.left * -1, 0.1f);
            }
            else
            {
                return Physics.Raycast(leftGroundCheckTransform.position, Vector3.right * -1, 0.1f);
            }
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
