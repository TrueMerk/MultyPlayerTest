using Gameplay.Entities.Enemy;
using Gameplay.ObjectsPool;
using Zenject;


namespace Gameplay.Context.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Pool>().AsTransient().Lazy(); 
            
            Container.Bind<EnemyContainer>().AsSingle().NonLazy();
        }
        
    }
}
