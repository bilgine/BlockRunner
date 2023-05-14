using EndGamePanelSystem;
using PlayerSystem;
using SoundSystem;
using Stack;
using Stack.StackPoolSystem;
using StackSpawnerSystem;

namespace ServiceLocatorSystem
{
    public class GameContainer : BaseContainer
    {
        public override Container Type => Container.Game;

        protected override void Install()
        {
            ServiceLocator.RegisterMono<Pool>();
            ServiceLocator.RegisterMono<StackSpawner>();
            ServiceLocator.RegisterMono<Player>();
            ServiceLocator.RegisterMono<SoundManager>();
            ServiceLocator.RegisterMono<StackMaterials>();
            ServiceLocator.Register<StackListManager>();
            ServiceLocator.Register<GameStatusManager>();
            ServiceLocator.RegisterMono<FinishStatusManager>();
            ServiceLocator.RegisterMono<EndGamePanelController>();
        }
    }
}