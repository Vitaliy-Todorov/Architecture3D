using System.Collections;
using UnityEngine;

namespace Scrips.Infrastructure
{
    public interface ICoroutineRanner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}