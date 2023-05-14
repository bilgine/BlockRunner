using System;
using DG.Tweening;
using ServiceLocatorSystem;
using SoundSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Stack
{
    public class NormalStack: MonoBehaviour,IStackPiece
    {
        [SerializeField] private MeshRenderer meshRenderer;
        private NormalStack _previousStack;
        private StackDirection _stackDirection;
        private FinishStatusManager _finishStatusManager;

        public void InitializeStackPiece(IStackPiece tempPreviousStack)
        {
            SetContainers();
            SetPreviousStack(tempPreviousStack);
            SetStartStackDirection();
            SetStackInitialScale();
            SetStackInitialPosition();
            SetStackMaterial();
            CheckLastStack();
        }

        private void SetContainers()
        {
            _finishStatusManager = ContainerManager.GetContainer(Container.Game).ServiceLocator.Resolve<FinishStatusManager>();
        }

        private void CheckLastStack()
        {
            if (transform.position.z >= _finishStatusManager.FinishPosition.z)
            {
                transform.DOKill();
                transform.localScale = new Vector3(2.5f, 1f, 2.5f);
                transform.position = new Vector3(0, transform.position.y, transform.position.z);
                SetStackActiveStatus(false);
            }
            else
            {
                SetStackActiveStatus(true);
                StackStartMovement();
            }
        }

        public void SetStackActiveStatus(bool status)
        {
            gameObject.SetActive(status);
        }

        public void SetPreviousStack(IStackPiece tempPreviousStack)
        {
            _previousStack = (NormalStack)tempPreviousStack;
            transform.localScale = _previousStack.transform.localScale;
        }
        
        public void SetStackInitialScale()
        {
            transform.localScale = _previousStack.transform.localScale;
        }

        public void SetStackInitialPosition()
        {
            var position = _previousStack.transform.position;
            float xPosition;
            if (_stackDirection == StackDirection.Left)
                xPosition = position.x - 3f;
            else xPosition = position.x + 3f;
            Vector3 startPosition = new Vector3(xPosition,
                position.y + 0.55f, position.z + transform.localScale.z);
            transform.Translate(startPosition);
        }

        private void SetStartStackDirection()
        {
            _stackDirection = (StackDirection)Random.Range(0, 2);
        }

        public void SetStackMaterial()
        {
            StackMaterials stackMaterials = ContainerManager.GetContainer(Container.Game).ServiceLocator.Resolve<StackMaterials>();
            meshRenderer.material = stackMaterials.stackMaterialList[Random.Range(0,stackMaterials.stackMaterialList.Count)];
        }
        
        public void StackStartMovement()
        {
            if (_stackDirection == StackDirection.Left)
                transform.DOMoveX(3f, 4f);
            else if (_stackDirection == StackDirection.Right)
                transform.DOMoveX(-3f, 4f);
        }

        public void StackStopMovement()
        {
            transform.DOKill();
            float hangover = transform.position.x - _previousStack.transform.position.x;
            SetStreakSound(hangover);
            if(Mathf.Abs(hangover) > transform.localScale.x)
                return;
            if (Mathf.Abs(hangover) < 0.1f)
                hangover = 0f;
            float direction = hangover > 0 ? 1f : -1f;
            SplitCubeOnX(hangover, direction);
        }

        private void SplitCubeOnX(float hangover, float direction)
        {
            float newSize = _previousStack.transform.localScale.x- Math.Abs(hangover);
            float fallingBlockSize = transform.localScale.x - newSize;
        
            float newXPosition = _previousStack.transform.position.x + (hangover / 2);
            transform.localScale = new Vector3(newSize, transform.localScale.y, transform.localScale.z);
            transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);
        
            float cubeEdge = transform.position.x + (newSize / 2f * direction);
            float fallingCubeXPosition = cubeEdge + (fallingBlockSize / 2f * direction);

            if (hangover == 0f) return;
            SpawnDropCube(fallingCubeXPosition, fallingBlockSize);
        }

        private void SpawnDropCube(float fallingCubeXPosition, float fallingBlockSize)
        {
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.localScale = new Vector3(fallingBlockSize, transform.localScale.y, transform.localScale.z);
            cube.transform.position = new Vector3(fallingCubeXPosition, transform.position.y, transform.position.z);
            cube.AddComponent<Rigidbody>();
            cube.GetComponent<Renderer>().material = GetComponent<Renderer>().material;
            Destroy(cube.gameObject, 1f);
        }

        private void SetStreakSound(float hangover)
        {
            SoundManager soundManager = ContainerManager.GetContainer(Container.Game).ServiceLocator.Resolve<SoundManager>();
            if (Mathf.Abs(hangover) < 0.1f)
            {
                soundManager.PitchSound();
                soundManager.PlaySound();
            }
            else soundManager.ResetSound();
        }

        public void StackAdjustmentAfterStop()
        {
            throw new NotImplementedException();
        }

        public void SetStackDefault()
        {
            transform.DOKill();
            gameObject.SetActive(false);
            transform.position = new Vector3(0f, -0.55f, 0f);
        }
    }
}

public enum StackDirection
{
    Left = 0,
    Right = 1
}
