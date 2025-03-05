using System;
using System.Collections;
using UnityEngine;

namespace Assets.Sources.Gameplay
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class Item : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private Collider _collider;

        public bool CanPickUped { get; private set; }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();

            CanPickUped = true;
        }

        public void PickUp(Transform container, float duration, Action callback)
        {
            CanPickUped = false;

            _rigidbody.isKinematic = true;
            _collider.enabled = false;

            StartCoroutine(PickUper(container, duration, callback));
        }

        public void Throw(Vector3 direction, float throwForce)
        {
            transform.parent = null;
            _rigidbody.isKinematic = false;
            _collider.enabled = true;
            CanPickUped = true;

            _rigidbody.AddForce(direction * throwForce, ForceMode.Impulse);
        }

        private IEnumerator PickUper(Transform container, float duration, Action callback)
        {
            float progress;
            float passedTime = 0;
            Vector3 startPosition = transform.position;

            transform.parent = null;

            while(transform.position != container.position)
            {
                progress = passedTime / duration;
                passedTime += Time.deltaTime;

                transform.position = Vector3.Lerp(startPosition, container.position, progress);

                yield return null;
            }

            transform.parent = container.transform;
            callback?.Invoke();
        }
    }
}
