using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField]
        private Image _filler;
        [SerializeField]
        private TextMeshProUGUI _text;
    
        public void SetProgress(float progress, float maxProgress)
        {
            _filler.fillAmount = progress / maxProgress;
            _text.text = progress.ToString("0");
        }
    }
}