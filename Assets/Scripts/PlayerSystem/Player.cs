using DG.Tweening;
using EndGamePanelSystem;
using ServiceLocatorSystem;
using StackSpawnerSystem;
using UnityEngine;

namespace PlayerSystem
{
    public class Player : MonoBehaviour, IPlayerFunctions
    {
        [SerializeField] private float playerMovementSpeed = 2.5f;
        [SerializeField] private Animator playerAnimator;
        public delegate void OnPlayerFall();
        public OnPlayerFall PlayerFell;
        public bool isFinished;
        private FinishStatusManager _finishStatusManager;
        private EndGamePanelController _endGamePanelController;

        public void Start()
        {
            _finishStatusManager = ContainerManager.GetContainer(Container.Game).ServiceLocator.Resolve<FinishStatusManager>();
            _endGamePanelController = ContainerManager.GetContainer(Container.Game).ServiceLocator.Resolve<EndGamePanelController>();
        }

        public void PlayerStartMovement()
        {
            if (isFinished) return;
            transform.Translate(Vector3.forward * (playerMovementSpeed * Time.deltaTime));
        }
        
        public void PlayerPositionAdjustment(float positionX)
        {
            transform.DOMoveX(
                positionX, .3f);
        }
        
        public void PlayerDance()
        {
            playerAnimator.Play("dance");
        }

        private void PlayerFallControl()
        {
            if(transform.position.y < -2f)
                PlayerFell?.Invoke();
        }

        public void PlayerFall()
        {
            StackSpawner stackSpawner = ContainerManager.GetContainer(Container.Game).ServiceLocator.Resolve<StackSpawner>();
            RestartPlayerProperties();
            stackSpawner.RestartStackSpawn();
        }

        public void RestartPlayerProperties()
        {
            var transform1 = transform;
            transform1.position = Vector3.zero;
            transform1.localRotation = Quaternion.identity;
            var rigidbodyComponent = GetComponent<Rigidbody>();
            rigidbodyComponent.angularVelocity=Vector3.zero;
            rigidbodyComponent.velocity=Vector3.zero;
        }

        public void NewGame()
        {
            playerAnimator.Play("Run");
            var transform1 = transform;
            transform1.position += Vector3.forward*.5f;
            transform1.localRotation = Quaternion.identity;
            var rigidbodyComponent = GetComponent<Rigidbody>();
            rigidbodyComponent.angularVelocity=Vector3.zero;
            rigidbodyComponent.velocity=Vector3.zero;
        }
        
        private void Update()
        {
            PlayerStartMovement();
            PlayerFallControl();
        }
        
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject != _finishStatusManager.FinishObject) return;
            _endGamePanelController.InstantiateEndGamePanel();
            isFinished= true;
            PlayerDance();
        }
    }
}