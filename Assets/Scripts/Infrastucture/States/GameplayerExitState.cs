using Infrastucture;
using UnityEngine;

namespace Infrastucture.States
{
    public class GameplayerExitState : IState
    {
        public void Enter()
        {
            var spawner = ResolveSpawner();
            if (spawner != null)
            {
                spawner.DespawnEnemyAll();
            }

            ServiceLocator.Resolved<IPlayerFactory>().Release();

            var loading = ResolveLoading();
            if (loading != null)
            {
                loading.LoadScene(GlobalConstants.Scenes.Main);
            }
        }

        public void Exit()
        {
        }

        private Loading ResolveLoading()
        {
            try
            {
                return ServiceLocator.Resolved<Loading>();
            }
            catch
            {
                var loadings = Resources.FindObjectsOfTypeAll<Loading>();
                return loadings.Length > 0 ? loadings[0] : null;
            }
        }

        private SpawnerEnemy ResolveSpawner()
        {
            try
            {
                return ServiceLocator.Resolved<SpawnerEnemy>();
            }
            catch
            {
                var spawners = Resources.FindObjectsOfTypeAll<SpawnerEnemy>();
                return spawners.Length > 0 ? spawners[0] : null;
            }
        }
    }
}
