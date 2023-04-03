using UnityEngine;

namespace Code.Buildings
{
    public static class BuildingsPositionInWorld
    {
        public static Vector3 StorePosition = new Vector3(10, 0, 0);
        public static Vector3 CastlePosition = new Vector3(0, 0, -10);
        public static Vector3 BarracksPosition = new Vector3(0, 0, 0);
        public static Vector3 SmithyPosition = new Vector3(10, 0, -10);
        
        private static Vector3 _wallPosition01 = new Vector3(0, 0, 6);
        private static Vector3 _wallRotation01 = new Vector3(0, 0, 0);
        private static Vector3 _wallPosition02 = new Vector3(12, 0, 6);
        private static Vector3 _wallRotation02 = new Vector3(0, 0, 0);
        private static Vector3 _wallPosition03 = new Vector3(16.5f, 0, 1.5f);
        private static Vector3 _wallRotation03 = new Vector3(0, 90, 0);
        private static Vector3 _wallPosition04 = new Vector3(16.5f, 0, -4.5f);
        private static Vector3 _wallRotation04 = new Vector3(0, 90, 0);
        private static Vector3 _wallPosition05 = new Vector3(16.5f, 0, -10.5f);
        private static Vector3 _wallRotation05 = new Vector3(0, 90, 0);
        private static Vector3 _wallPosition06 = new Vector3(12, 0, -15);
        private static Vector3 _wallRotation06 = new Vector3(0, 180, 0);
        private static Vector3 _wallPosition07 = new Vector3(6, 0, -15);
        private static Vector3 _wallRotation07 = new Vector3(0, 180, 0);
        private static Vector3 _wallPosition08 = new Vector3(0, 0, -15);
        private static Vector3 _wallRotation08 = new Vector3(0, 180, 0);
        private static Vector3 _wallPosition09 = new Vector3(-4.5f, 0, -10.5f);
        private static Vector3 _wallRotation09 = new Vector3(0, -90, 0);
        private static Vector3 _wallPosition10 = new Vector3(-4.5f, 0, -4.5f);
        private static Vector3 _wallRotation10 = new Vector3(0, -90, 0);
        private static Vector3 _wallPosition11 = new Vector3(-4.5f, 0, 1.5f);
        private static Vector3 _wallRotation11 = new Vector3(0, -90, 0);
        public static Vector3[] WallsPosition =
        {
            _wallPosition01, _wallPosition02, _wallPosition03, _wallPosition04, _wallPosition05,
            _wallPosition06, _wallPosition07, _wallPosition08, _wallPosition09, _wallPosition10, _wallPosition11
        };
        public static Vector3[] WallsRotation =
        {
            _wallRotation01, _wallRotation02, _wallRotation03, _wallRotation04, _wallRotation05, 
            _wallRotation06, _wallRotation07, _wallRotation08, _wallRotation09, _wallRotation10, _wallRotation11
        };
        
        private static Vector3 _towerPosition01 = new Vector3(-4.5f, 0, 6);
        private static Vector3 _towerRotation01 = new Vector3(0, 0, 0);
        private static Vector3 _towerPosition02 = new Vector3(16.5f, 0, 6);
        private static Vector3 _towerRotation02 = new Vector3(0, 90, 0);
        private static Vector3 _towerPosition03 = new Vector3(16.5f, 0, -15f);
        private static Vector3 _towerRotation03 = new Vector3(0, 180, 0);
        private static Vector3 _towerPosition04 = new Vector3(-4.5f, 0, -15f);
        private static Vector3 _towerRotation04 = new Vector3(0, 270, 0);
        public static Vector3[] TowersPosition = {_towerPosition01, _towerPosition02, _towerPosition03, _towerPosition04};
        public static Vector3[] TowersRotation = {_towerRotation01, _towerRotation02, _towerRotation03, _towerRotation04};
        
        public static Vector3 GatePosition = new Vector3(6, 0, 6);
        public static Vector3 GateRotation = new Vector3(0, 0, 0);
        
        public static Vector3 FoodPosition = new Vector3(0, 0, 18.91f);
        public static Vector3 WoodPosition = new Vector3(13.3f, 0, 16.76f);
        public static Vector3 StonePosition = new Vector3(22.23f, 0, 0);
        public static Vector3 IronPosition = new Vector3(22.13f, 0, -10.34f);
    }
}