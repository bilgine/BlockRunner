using PlayerSystem;
using ServiceLocatorSystem;
using Stack;
using StackSpawnerSystem;
using UnityEngine;

public class GameStatusManager :  IService
{
    private Player _player;
    private StackSpawner _stackSpawner;
    private StackListManager _stackListManager;
    private FinishStatusManager _finishStatusManager;
    
    
    public void OnRegisterComplete()
    {
    }

    public void OnContainerStart()
    {
        _player = ContainerManager.GetContainer(Container.Game).ServiceLocator.Resolve<Player>();
        _stackSpawner = ContainerManager.GetContainer(Container.Game).ServiceLocator.Resolve<StackSpawner>();
        _finishStatusManager = ContainerManager.GetContainer(Container.Game).ServiceLocator.Resolve<FinishStatusManager>();
        _player.PlayerFell += PlayerFell;
    }
    
    private void PlayerFell()
    {
        RestartGame();
    }

    private void RestartGame()
    {
        _player.RestartPlayerProperties();
        _stackSpawner.RestartStackSpawn();
        _finishStatusManager.SetInitialPosition();
    }

    
}    
