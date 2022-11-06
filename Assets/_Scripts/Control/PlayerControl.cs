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
        #region Check Transforms
        [SerializeField] private Transform leftGroundCheckTransform;
        [SerializeField] private Transform rightGroundCheckTransform;
        #endregion

        [SerializeField] private Transform bodyTransform;
        [SerializeField] private float movementSpeed = 20f;
        [SerializeField] private float jumpPower = 120f;

        private bool _isMovingLeft;
        private bool _isOnGround;

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
            CheckIfPlayerOnLand();
            GravityCheck();
        }
        private void MovementAction()
        {
            if (_leftInputReleased || _rightInputReleased)
            {
                SetVelocityX(0);
                return;
            }
            if (_leftInput)
            {
                CheckMovementRotation();
                SetVelocityX(-movementSpeed);
                _isMovingLeft = true;
                return;
            }
            if (_rightInput)
            {
                CheckMovementRotation();
                SetVelocityX(movementSpeed);
                _isMovingLeft = false;
                return;
            }
            SetVelocityX(0);
        }
        private void JumpAction()
        {
            if (_jumpInputReleased && _rigidbody.velocity.y>0)  // input kalktýðýnda yükseliþin durmasý için
            {
                SetVelocityY(0);
                return;
            }
            if (_jumpInput && _isOnGround)
            {
                SetVelocityY(jumpPower);
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
        #region Checks
        private void GravityCheck()
        {
            if (_isOnGround)
            {
                _rigidbody.useGravity = false;
            }
            else
            {
                _rigidbody.useGravity = true;
            }
        }
        private void CheckIfPlayerOnLand()
        {
            _isOnGround = Physics.Raycast(leftGroundCheckTransform.position, Vector3.up * -1, 0.3f) ||
                Physics.Raycast(rightGroundCheckTransform.position, Vector3.up * -1, 0.3f);
        }
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
        #endregion
    }
}
