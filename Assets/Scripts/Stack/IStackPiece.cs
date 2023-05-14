
namespace Stack
{
    public interface IStackPiece
    {
        public void InitializeStackPiece(IStackPiece previousStack);

        public void SetStackActiveStatus(bool status);

        public void SetPreviousStack(IStackPiece tempPreviousStack);

        public void SetStackInitialScale();

        public void SetStackInitialPosition();

        public void SetStackMaterial();
        
        public void StackStartMovement();

        public void StackStopMovement();
    
        public void StackAdjustmentAfterStop();

        public void SetStackDefault();
    }
}
    
