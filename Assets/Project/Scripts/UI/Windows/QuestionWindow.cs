using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project
{
    public class QuestionWindow : MonoBehaviour, IWindow
    {
        [SerializeField]
        private AnswerButton[] _buttons = null;

        [SerializeField]
        private Image _quationBar = null;

        [SerializeField]
        private TextMeshProUGUI _answerCounter = null;

        private int _questionLength;
        private int _currentQuestionIndex = 0;

        private LevelData.QuestionPreset[] _questionPresets = null;
        private UISystem _uiSystem;

        public bool IsPopup
        {
            get => false;
        }
        
        public void Prepare(UISystem uiSystem)
        {
            _uiSystem = uiSystem;
        }
        
        public void Setup(Action onWrongClick, Action onAllQuestionAnswers)
        {
            for (int i = 0; i < _buttons.Length; i++)
            {
                _buttons[i].Prepare(isCorrect =>
                {
                    OnButtonClick(onWrongClick, onAllQuestionAnswers, isCorrect);
                });
            }
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
            
            Refresh((LevelData)_uiSystem.WindowData[GameStateController.LevelDataKey]);
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }
        
        private void Refresh(LevelData levelData)
        {
            _questionPresets = levelData.QuestionPresets.Take(levelData.QuestionPresets.Length).ToArray();
            _questionPresets.Shuffle();
            _questionLength = _questionPresets.Length;
            _currentQuestionIndex = 0;

            RefreshAnswerCounter();
            RefreshQuestion();
        }
        
        private void OnButtonClick(Action onWrongClick, Action onAllQuestionAnswers, bool isCorrect)
        {
            if (isCorrect)
            {
                _currentQuestionIndex++;

                RefreshAnswerCounter();
                
                if (_currentQuestionIndex < _questionLength)
                {
                    RefreshQuestion();
                }
                else
                {
                    onAllQuestionAnswers.Invoke();
                }
            }
            else
            {
                onWrongClick?.Invoke();
            }
        }

        private void RefreshAnswerCounter()
        {
            _answerCounter.text = $"{_currentQuestionIndex} / {_questionLength}";
        }
        
        private void RefreshQuestion()
        {
            var questionPreset = _questionPresets[_currentQuestionIndex];
            var questionPresetAnswers = questionPreset.Answers.Take(questionPreset.Answers.Length).ToArray();
            
            questionPresetAnswers.Shuffle();
            
            for (int i = 0; i < _buttons.Length; i++)
            {
                var answerPreset = questionPresetAnswers[i];
                _buttons[i].Refresh(answerPreset.Answer, answerPreset.IsCorrect);
            }

            _quationBar.sprite = questionPreset.Image;
        }
    }
}