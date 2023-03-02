using System;
using System.Collections;
using UnityEngine;

namespace Code.Unit.CraftUnit
{
    public class CraftUnitView : MonoBehaviour
    {
        public event Action CraftFinish;
        
        public void StartCoroutineCraft(float time)
        {
            StartCoroutine(Craft(time));
        }

        public void StopCoroutineCraft(float time)
        {
            StopCoroutine(Craft(time));
        }

        private IEnumerator Craft(float time)
        {
            yield return new WaitForSeconds(time);
            CraftFinish?.Invoke();
        }
    }
}