using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project
{
    public class QuestionController : MonoBehaviour
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

        public void Prepare(Action onWrongClick, Action onAllQuestionAnswers)
        {
            for (int i = 0; i < _buttons.Length; i++)
            {
                _buttons[i].Prepare(isCorrect =>
                {
                    OnButtonClick(onWrongClick, onAllQuestionAnswers, isCorrect);
                });
            }
        }

        private void OnButtonClick(Action onWrongClick, Action onAllQuestionAnswers, bool isCorrect)
        {
            if (isCorrect)
            {
                _currentQuestionIndex++;

                RefreshAnswerCounter();
                
                if (_currentQuestionIndex < _questionLength)
                {
                    ShowQuestion();
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

        public void Setup(LevelData levelData)
        {
            _questionPresets = levelData.QuestionPresets.Take(levelData.QuestionPresets.Length).ToArray();
            _questionPresets.Shuffle();
            _questionLength = _questionPresets.Length;
            _currentQuestionIndex = 0;

            RefreshAnswerCounter();
            ShowQuestion();
        }

        private void ShowQuestion()
        {
            var questionPreset = _questionPresets[_currentQuestionIndex];
            
            for (int i = 0; i < _buttons.Length; i++)
            {
                var answerPreset = questionPreset.Answers[i];
                _buttons[i].Refresh(answerPreset.Answer, answerPreset.IsCorrect);
            }

            _quationBar.sprite = questionPreset.Image;
        }
    }
}