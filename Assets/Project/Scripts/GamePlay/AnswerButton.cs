using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project
{
    public class AnswerButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button = null;

        [SerializeField]
        private TextMeshProUGUI _text = null;

        private bool _isCorrect;
        
        private Action<bool> _onButtonClick;
        
        public void Prepare(Action<bool> onButtonClick)
        {
            _onButtonClick = onButtonClick;
            
            _button.onClick.AddListener(() =>
            {
                _onButtonClick?.Invoke(_isCorrect);
            });
        }
        
        public void Refresh(string answerText, bool isCorrect)
        {
            _isCorrect = isCorrect;
            _text.text = answerText;
        }
    }
}