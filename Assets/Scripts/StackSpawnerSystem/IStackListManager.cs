using System.Collections.Generic;
using Stack;

namespace StackSpawnerSystem
{
    public interface IStackListManager
    {
        public void AddStack(IStackPiece stack);
        
        public void RemoveStack(IStackPiece stack);

        public void RemoveFirstStack();
        
        public List<IStackPiece> GetStackList();
        
        public IStackPiece GetLastStack();
        
        public IStackPiece GetFirstStack();
        
    }
}