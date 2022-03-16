using System.Collections;
using UnityEngine;

namespace UI
{
  public class LoadingCurtain : MonoBehaviour
  {
    public CanvasGroup Curtain;

    private void Awake()
    {
      gameObject.SetActive(false);
      DontDestroyOnLoad(this);
    }

    public void Show()
    {
      gameObject.SetActive(true);
      Curtain.alpha = 1;
    }
    
    public void Hide() => StartCoroutine(DoFadeIn());
    
    private IEnumerator DoFadeIn()
    {
      while (Curtain.alpha > 0)
      {
        Curtain.alpha -= 0.05f;
        yield return new WaitForSeconds(0.05f);
      }
      
      gameObject.SetActive(false);
    }
  }
}