using UnityEngine;

namespace Code.ShadersTools
{
    public class DrawOutline : MonoBehaviour
    {
        [SerializeField] private Material _material;
        private const string PROPERTY_KEY = "_Outline";
        private const float UNVISIBLE = 0.0f;
        private const float VISIBLE = 1f;

        public void MakeVisible()
        {
            _material.SetFloat(PROPERTY_KEY, VISIBLE);
        }

        public void MakeUnvisible()
        {
            _material.SetFloat(PROPERTY_KEY, UNVISIBLE);
        }
    }
}