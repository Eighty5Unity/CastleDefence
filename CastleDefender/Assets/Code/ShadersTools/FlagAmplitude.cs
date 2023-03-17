using UnityEngine;

namespace Code.ShadersTools
{
    public class FlagAmplitude : MonoBehaviour
    {
        [SerializeField] private Material _material;
        private const string PROPERTY_KEY = "_Amplitude";
        private float _amplitude = 1f;
        private int _positiveNegative = 1;

        void Update()
        {
            if (_amplitude < 1f)
            {
                _positiveNegative = 1;
            }
            else if (_amplitude > 4f)
            {
                _positiveNegative = -1;
            }
            
            _amplitude += Time.deltaTime * _positiveNegative;
            _material.SetFloat(PROPERTY_KEY, _amplitude);
        }
    }
}
