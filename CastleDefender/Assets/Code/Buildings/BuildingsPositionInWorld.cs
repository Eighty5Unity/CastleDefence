using UnityEngine;

namespace Code.Buildings
{
    public static class BuildingsPositionInWorld
    {
        public static Vector3 StorePosition = new Vector3(10, 0, 0);
        public static Vector3 CastlePosition = new Vector3(0, 0, -10);
        public static Vector3 BarracksPosition = new Vector3(0, 0, 0);
        public static Vector3 SmithyPosition = new Vector3(10, 0, -10);
        
        public static Vector3 TowerPosition01 = new Vector3(-4.5f, 0, 6);
        public static Vector3 TowerRotation01 = new Vector3(0, 0, 0);
        public static Vector3 TowerPosition02 = new Vector3(16.5f, 0, 6);
        public static Vector3 TowerRotation02 = new Vector3(0, 90, 0);
        public static Vector3 TowerPosition03 = new Vector3(16.5f, 0, -15f);
        public static Vector3 TowerRotation03 = new Vector3(0, 180, 0);
        public static Vector3 TowerPosition04 = new Vector3(-4.5f, 0, -15f);
        public static Vector3 TowerRotation04 = new Vector3(0, 270, 0);
        
        public static Vector3 FoodPosition = new Vector3(0, 0, 18.91f);
        public static Vector3 WoodPosition = new Vector3(13.3f, 0, 16.76f);
        public static Vector3 StonePosition = new Vector3(22.23f, 0, 0);
        public static Vector3 IronPosition = new Vector3(22.13f, 0, -10.34f);
    }
}