using Code.Buildings.CastleBuildings;
using Code.Buildings.ResourcesBuilgings;
using UnityEngine;

namespace Code.StaticData
{
    [CreateAssetMenu(menuName = "StaticData/Buildings", fileName = "Buildings")]
    public class BuildingsStaticData : ScriptableObject
    {
        public Vector3 CastleSpawnPoinUnit;
        public Vector3 StorePoint;

        public ResourcesType FoodType;
        public Vector3 FoodCraftPoint;
        
        public ResourcesType WoodType;
        public Vector3 WoodCraftPoint;
        
        public ResourcesType StoneType;
        public Vector3 StoneCraftPoint;
        
        public ResourcesType IronType;
        public Vector3 IronCraftPoint;
        
    }
}