using System;
using System.Collections;
using UnityEngine;

namespace Project
{
    public class StickerRotate : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _startRotation = Vector3.zero;
        [SerializeField]
        private Vector3 _targetRotation = Vector3.zero;
        [SerializeField]
        private float _rotateTIme = 0f;

        private Transform thisTransform;

        private void Awake()
        {
            thisTransform = transform;
        }

        private void OnEnable()
        {
            //FFFFFFF
            StartCoroutine(StartRotate());
        }

        private IEnumerator StartRotate()
        {
            var startRotation = _startRotation;
            var targetRotation = _targetRotation;
            var waitForSeconds = new WaitForSeconds(_rotateTIme);

            while (true)
            {
                var transformRotation = thisTransform.rotation;
                transformRotation.eulerAngles = startRotation;
                thisTransform.rotation = transformRotation;

                (startRotation, targetRotation) = (targetRotation, startRotation);

                yield return waitForSeconds;
            }
        }
    }
}