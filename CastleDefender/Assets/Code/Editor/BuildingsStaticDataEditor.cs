using System.Linq;
using Code.Buildings.CastleBuildings;
using Code.Buildings.ResourcesBuilgings;
using Code.StaticData;
using UnityEditor;
using UnityEngine;

namespace Code.Editor
{
    [CustomEditor(typeof(BuildingPointsStaticData))]
    public class BuildingsStaticDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            BuildingPointsStaticData buildingPointsData = (BuildingPointsStaticData)target;

            if (GUILayout.Button("Create"))
            {

                buildingPointsData.CastleSpawnPoinUnit = FindObjectOfType<CastleBuildingView>().SpawnUnitPoint.position;
                buildingPointsData.StorePoint = FindObjectOfType<StoreBuildingView>().StorePointPosition.position;

                var food = FindObjectOfType<FoodBuilding>();
                buildingPointsData.FoodType = food.ResourcesType;
                buildingPointsData.FoodCraftPoint = food.CraftPoint.position;

                var wood = FindObjectOfType<WoodBuilding>();
                buildingPointsData.WoodType = wood.ResourcesType;
                buildingPointsData.WoodCraftPoint = wood.CraftPoint.position;

                var stone = FindObjectOfType<StoneBuilding>();
                buildingPointsData.StoneType = stone.ResourcesType;
                buildingPointsData.StoneCraftPoint = stone.CraftPoint.position;

                var iron = FindObjectOfType<IronBuilding>();
                buildingPointsData.IronType = iron.ResourcesType;
                buildingPointsData.IronCraftPoint = iron.CraftPoint.position;
            }
            
            EditorUtility.SetDirty(target);
        }
    }
}