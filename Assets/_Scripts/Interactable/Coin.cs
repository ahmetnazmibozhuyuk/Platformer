using UnityEngine;
using Pounds.Managers;
using DG.Tweening;

namespace Pounds.Interactable
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private float coinValue = 10f;

        private readonly Vector3 _rotation = new Vector3(0, 360, 0);

        private Tweener _rotateTweener;
        private void Start()
        {
            _rotateTweener = transform.DOLocalRotate(_rotation, 5f, RotateMode.Fast).SetLoops(-1).SetEase(Ease.Linear).SetRelative();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _rotateTweener.Kill();
                GameManager.Instance.CoinCollected();
                Destroy(gameObject);
            }
        }


    }
}
