using Code.GameServices;
using UnityEngine;

namespace Code.Unit.EnemyUnit
{
    public class SpawnEnemyPositions
    {
        private const float MIN = -100f;
        private const float MAX = 100f;
        private const float YPOSITION = 1.5f;
        private float[] _allVariants;
        private readonly IGameFactory _factory;

        public SpawnEnemyPositions(IGameFactory factory)
        {
            _factory = factory;
        }
        
        public void SpawnEnemy(int count)
        {
            for (int i = 0; i < count; i++)
            {
                _factory.CreateEnemy(RandomSpawnPosition());
            }
        }

        private Vector3 RandomSpawnPosition()
        {
            Vector3 position;
            float x;
            float z;
            _allVariants = new float[] { MIN, MAX, Random.Range(-99f, 99f) };
            int index = Random.Range(0, _allVariants.Length);
            x = _allVariants[index];

            if (x == MIN || x == MAX)
            {
                z = _allVariants[2];
            }
            else
            {
                int indexZ = Random.Range(0, 2);
                z = _allVariants[indexZ];
            }

            position = new Vector3(x, YPOSITION, z);
            return position;
        }
    }
}