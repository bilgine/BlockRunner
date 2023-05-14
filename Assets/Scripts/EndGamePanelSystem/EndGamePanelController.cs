using UnityEngine;

namespace EndGamePanelSystem
{
    public class EndGamePanelController : MonoBehaviour
    {
        [SerializeField] private GameObject endGamePanelPrefab; 
        private GameObject _endGamePanel;
        [SerializeField] private Transform endGamePanelParent;


        public void InstantiateEndGamePanel()
        {
            _endGamePanel = Instantiate(endGamePanelPrefab, endGamePanelParent);
        }
        
        public void DestroyEndGamePanel()
        {
            if(_endGamePanel != null)
                Destroy(_endGamePanel);
        }
    }
}