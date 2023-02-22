using System.Collections.Generic;
using UnityEngine;

namespace Code.StaticData
{
    [CreateAssetMenu(menuName = "StaticData/DownInfo", fileName = "DownInfo")]
    public class DownInformationStaticData : ScriptableObject
    {
        public Sprite Icon;
        public string Name;
        public string Descriptions;
        public List<UIButtonInfo> Buttons;
    }
}