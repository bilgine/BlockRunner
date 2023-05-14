using System.Collections;
using System.Collections.Generic;
using EndGamePanelSystem;
using PlayerSystem;
using ServiceLocatorSystem;
using SoundSystem;
using Stack;
using StackSpawnerSystem;
using UnityEngine;

namespace InputSystem
{
    public class InputFunctionality : MonoBehaviour, IInputFunctionality
    {
        private StackSpawner _stackSpawner;
        private IStackListManager _stackListManager;
        private Player _player;
        private ISound _soundManager;
        private FinishStatusManager _finishStatusManager;
        private EndGamePanelController _endGamePanelController;
        private bool _isMouseClickActive = true;
        private bool _isSpaceClickActive = true;
        public void Start()
        {
            _stackSpawner = ContainerManager.GetContainer(Container.Game).ServiceLocator.Resolve<StackSpawner>();
            _stackListManager = ContainerManager.GetContainer(Container.Game).ServiceLocator.Resolve<StackListManager>();
            _player = ContainerManager.GetContainer(Container.Game).ServiceLocator.Resolve<Player>();
            _soundManager = ContainerManager.GetContainer(Container.Game).ServiceLocator.Resolve<SoundManager>();
            _finishStatusManager = ContainerManager.GetContainer(Container.Game).ServiceLocator.Resolve<FinishStatusManager>();
            _endGamePanelController = ContainerManager.GetContainer(Container.Game).ServiceLocator.Resolve<EndGamePanelController>();
        }

        public void ClickInput()
        {
            if (_player.isFinished) return;
            if (!Input.GetMouseButtonDown(0) || !_isMouseClickActive) return;
            StartCoroutine(EnableClick());
            NormalStack currentStack = (NormalStack)_stackListManager.GetLastStack();
            if (currentStack != null)
            {
                currentStack.StackStopMovement();
                _player.PlayerPositionAdjustment(currentStack.transform.position.x);
            }
            _stackSpawner.SpawnStack();
        }

        public void SpaceInput()
        {
            if (!Input.GetKeyDown(KeyCode.Space) || !_isSpaceClickActive) return;
            StartCoroutine(EnableSpaceClick());
            _endGamePanelController.DestroyEndGamePanel();
            NormalStack lastStack = (NormalStack)_stackListManager.GetLastStack();
            lastStack.gameObject.SetActive(true);
            _finishStatusManager.AddFinishPosition(Vector3.forward * 15f * Random.Range(1, 4));
            _stackSpawner.SpawnStack();
            _player.NewGame();
            _soundManager.ResetSound();
            _player.isFinished = false;
        }
        
        private IEnumerator EnableClick()
        {
            _isMouseClickActive = false;
            yield return new WaitForSeconds(.5f);
            _isMouseClickActive = true;
        }
        
        private IEnumerator EnableSpaceClick()
        {
            _isSpaceClickActive = false;
            yield return new WaitForSeconds(.5f);
            _isSpaceClickActive = true;
        }

    }
}