using System.Collections;
using Code.GameServices;
using UnityEngine;

namespace Code.Buildings.CastleBuildings
{
    public class CastleBuilding : MonoBehaviour
    {
        public Transform SpawnUnitPoint;
        private IGameFactory _gameFactory;
        public bool SpawnUnit = true;

        public void Constructor(IGameFactory factory)
        {
            _gameFactory = factory;
        }

        private void Update()
        {
            if (!SpawnUnit)
            {
                StartCoroutine(SpawnUnits());
                SpawnUnit = true;
            }
        }

        private IEnumerator SpawnUnits()
        {
            while (true)
            {
                yield return new WaitForSeconds(10f);
                _gameFactory.CreateUnit(SpawnUnitPoint.position);
            }
        }
    }
}