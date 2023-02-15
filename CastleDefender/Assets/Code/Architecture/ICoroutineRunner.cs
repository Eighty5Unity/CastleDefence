using System.Collections;
using UnityEngine;

namespace Code.Architecture
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}