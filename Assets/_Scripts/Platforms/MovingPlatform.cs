using UnityEngine;
using DG.Tweening;

namespace Pounds.Platforms
{
    public class MovingPlatform : MonoBehaviour
    {
        [SerializeField] private Transform[] moveTransform;
        [SerializeField] private float pointReachTime = 5;

        private int _currentPointIndex = 0;

        private void Start()
        {
            RecursiveMovement();
        }
        private void RecursiveMovement()
        {
            transform.DOMove(moveTransform[_currentPointIndex].position, pointReachTime).OnComplete(() =>
            {
                SwitchMoveTarget();
                RecursiveMovement();
            });
        }
        private void SwitchMoveTarget()
        {
            if(_currentPointIndex < moveTransform.Length-1)
                _currentPointIndex++;
            else
                _currentPointIndex = 0;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.transform.SetParent(transform);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.transform.SetParent(null);
            }
        }
    }
}
