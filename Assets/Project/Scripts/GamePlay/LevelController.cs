using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project
{
    public class LevelController : MonoBehaviour
    {
        [Serializable]
        public class AnswerButton
        {
            [field: SerializeField]
            public Button Button
            {
                get;
                private set;
            }

            [field: SerializeField]
            public LevelData LevelData
            {
                get;
                private set;
            }
        }
        
        [SerializeField]
        private AnswerButton[] _levelButtons = null;
        [SerializeField]
        private QuestionController _questionController = null;

        public void Prepare(Action onLevelButtonClick)
        {
            for (int i = 0; i < _levelButtons.Length; i++)
            {
                var i1 = i;
                
                _levelButtons[i].Button.onClick.AddListener(() =>
                {
                    _questionController.Setup(_levelButtons[i1].LevelData);
                    onLevelButtonClick?.Invoke();
                });
            }
        }
    }
}