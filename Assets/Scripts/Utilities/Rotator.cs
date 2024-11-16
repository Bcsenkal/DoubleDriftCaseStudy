using System;
using Managers;
using UnityEngine;

namespace Utilities
{
    public class Rotator : MonoBehaviour
    {
        private bool _canRotate;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private Vector3 rotationVector;
        private Transform cam;


        private void OnEnable()
        {
            _canRotate = true;
            cam =Camera.main.transform;
        }

        private void OnDisable()
        {
            _canRotate = false;
        }

        

        void Update()
        {
            _canRotate = Mathf.Abs(cam.position.z - transform.position.z) < 100f;
            if (!_canRotate) return;
            transform.Rotate(rotationVector*(rotationSpeed*Time.deltaTime),Space.Self);
        }
    }
}
