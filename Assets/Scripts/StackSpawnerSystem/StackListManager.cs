using System.Collections.Generic;
using Stack;

namespace StackSpawnerSystem
{
    public class StackListManager: IStackListManager
    {
        private readonly List<IStackPiece> _stackList = new List<IStackPiece>();
        
        public void AddStack(IStackPiece stack)
        {
            _stackList.Add(stack);
        }

        public void RemoveStack(IStackPiece stack)
        {
            _stackList.Remove(stack);
        }

        public void RemoveFirstStack()
        {
            _stackList.RemoveAt(0);
        }

        public List<IStackPiece> GetStackList()
        {
            return _stackList;
        }
        
        public IStackPiece GetLastStack()
        {
            return _stackList[^1];
        }

        public IStackPiece GetFirstStack()
        {
            return _stackList[0];
        }
    }
}