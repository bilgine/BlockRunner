using System;
using PlayerSystem;
using ServiceLocatorSystem;
using UnityEngine;

public class CameraAnimationController : MonoBehaviour
{
        [SerializeField] private Transform cameraTransform;
        private Player _player;
        public float speed;

        public void Start()
        {
            _player = ContainerManager.GetContainer(Container.Game).ServiceLocator.Resolve<Player>();
        }

        private void RotateCamera()
        {
                transform.Rotate(0,speed* Time.deltaTime,0);
        }

        private void ResetCameraAngle()
        {
               transform.localEulerAngles = Vector3.zero; 
        }

        private void Update()
        {
            if (_player.isFinished)
                RotateCamera();
            else
            {
                ResetCameraAngle();
                transform.position = 
                    new Vector3(cameraTransform.position.x, transform.position.y, _player.transform.position.z);
                
            }
        }
}