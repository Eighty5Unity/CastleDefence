using System.Collections;
using Code.GameServices;
using UnityEngine;

namespace Code.Buildings.CastleBuildings
{
    public class CastleBuildingController
    {
        private readonly IGameFactory _gameFactory;
        private readonly CastleBuildingView _castleView;

        public CastleBuildingController(IGameFactory factory, CastleBuildingView castleView)
        {
            _gameFactory = factory;
            _castleView = castleView;
        }

        private IEnumerator SpawnUnits()
        {
            yield return new WaitForSeconds(10f);
            _gameFactory.CreateUnit(_castleView.SpawnUnitPoint.position);
        }
    }
}