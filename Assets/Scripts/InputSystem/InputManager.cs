using UnityEngine;

namespace InputSystem
{
    public class InputManager : MonoBehaviour
    {
        private InputFunctionality _inputFunctionality;

        private void Awake()
        {
            _inputFunctionality = GetComponent<InputFunctionality>();
        }

        private void Update()
        {
            _inputFunctionality.ClickInput();
            _inputFunctionality.SpaceInput();
        }
    
    }
}