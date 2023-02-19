using System.Linq;
using Code.Buildings.CastleBuildings;
using Code.Buildings.ResourcesBuilgings;
using Code.StaticData;
using UnityEditor;
using UnityEngine;

namespace Code.Editor
{
    [CustomEditor(typeof(BuildingsStaticData))]
    public class BuildingsStaticDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            BuildingsStaticData buildingsData = (BuildingsStaticData)target;

            if (GUILayout.Button("Create"))
            {

                buildingsData.CastleSpawnPoinUnit = FindObjectOfType<CastleBuilding>().SpawnUnitPoint.position;
                buildingsData.StorePoint = FindObjectOfType<StoreBuilding>().StorePointPosition.position;

                var food = FindObjectOfType<FoodBuilding>();
                buildingsData.FoodType = food.ResourcesType;
                buildingsData.FoodCraftPoint = food.CraftPoint.position;

                var wood = FindObjectOfType<WoodBuilding>();
                buildingsData.WoodType = wood.ResourcesType;
                buildingsData.WoodCraftPoint = wood.CraftPoint.position;

                var stone = FindObjectOfType<StoneBuilding>();
                buildingsData.StoneType = stone.ResourcesType;
                buildingsData.StoneCraftPoint = stone.CraftPoint.position;

                var iron = FindObjectOfType<IronBuilding>();
                buildingsData.IronType = iron.ResourcesType;
                buildingsData.IronCraftPoint = iron.CraftPoint.position;
            }
            
            EditorUtility.SetDirty(target);
        }
    }
}