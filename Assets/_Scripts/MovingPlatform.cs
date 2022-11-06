using UnityEngine;
using DG.Tweening;

namespace Pounds
{
    public class MovingPlatform : MonoBehaviour
    {
        [SerializeField] private Transform[] moveTransform;

        private int _currentPointIndex = 0;

        private void Update()
        {
            
        }
        private void Start()
        {
            RecursiveMovement();
        }
        private void RecursiveMovement()
        {
            transform.DOMove(moveTransform[_currentPointIndex].position, 5).OnComplete(() =>
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
