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
        private bool _jumpInputPressed { get { return Input.GetKeyDown(KeyCode.Space); } }
        private bool _jumpInputReleased { get { return Input.GetKeyUp(KeyCode.Space); } }
        #endregion
        #region Check Transforms
        [SerializeField] private Transform leftGroundCheckTransform;
        [SerializeField] private Transform rightGroundCheckTransform;
        #endregion

        [SerializeField] private Transform bodyTransform;
        [SerializeField] private float movementSpeed = 20f;
        [SerializeField] private float jumpPower = 120f;

        [SerializeField] private LayerMask jumpableLayer;

        private bool _isMovingLeft;
        private bool _isOnGround;

        private Tweener _bodyTween;

        private Vector3 _currentVelocityVector;

        private Rigidbody _rigidbody;


        [SerializeField] private float maxCoyoteTime = 0.15f;
        private float _coyoteTimeCounter;

        [SerializeField] private float maxJumpBufferTime = 0.15f;
        private float _jumpBufferCounter;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            MovementAction();
            JumpAction();

            PlayerOnLandCheck();
            GravityCheck();
            CoyoteTimeCheck();
            JumpBufferCheck();
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
            if (_jumpInputReleased && _rigidbody.velocity.y>0)  // input kalktiginda yukselisin durmasi icin
            {
                SetVelocityY(0);
                _coyoteTimeCounter = 0;
                return;
            }
            if (_jumpBufferCounter>0 && _coyoteTimeCounter>0)
            {
                SetVelocityY(jumpPower);
                _jumpBufferCounter = 0;
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
        private void PlayerOnLandCheck()
        {
            
            _isOnGround = Physics.Raycast(leftGroundCheckTransform.position, Vector3.up * -1, 0.3f, jumpableLayer) ||
                Physics.Raycast(rightGroundCheckTransform.position, Vector3.up * -1, 0.3f, jumpableLayer);
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
        //platformu biraktigi anda jump inputu kullanilmasýna olanak saglamak icin
        private void CoyoteTimeCheck()
        {
            if (_isOnGround)
            {
                _coyoteTimeCounter = maxCoyoteTime;
            }
            else
            {
                _coyoteTimeCounter -= Time.deltaTime;
            }
        }
        //platforma inmek uzereyken jump inputu kullanildiginda yere deger degmez tekrar sicramaya olanak saglamak icin
        private void JumpBufferCheck()
        {
            if (_jumpInputPressed)
            {
                _jumpBufferCounter = maxJumpBufferTime;
            }
            else
            {
                _jumpBufferCounter -= Time.deltaTime;
            }
        }
        #endregion
    }
}
