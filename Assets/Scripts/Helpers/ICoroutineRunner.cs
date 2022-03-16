using System.Collections;
using UnityEngine;

namespace Helpers
{
  public interface ICoroutineRunner
  {
    Coroutine StartCoroutine(IEnumerator coroutine);
  }
}