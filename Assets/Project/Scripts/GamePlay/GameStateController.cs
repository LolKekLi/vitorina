using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class GameStateController : MonoBehaviour
    {
        public const string LevelDataKey = "LevelDataKey";
        
        [SerializeField]
        private QuestionWindow questionWindow = null;

        [SerializeField]
        private MainWindow _mainWindow = null;

        [SerializeField]
        private UISystem _uiSystem = null;

        private void Start()
        {
            questionWindow.Setup(OnWrongAnswer, OnAllQuestionAnswers);

            _mainWindow.Setup((levelData) =>
            {
               _uiSystem.ShowWindow<QuestionWindow>(new Dictionary<string, object>()
               {
                   {LevelDataKey, levelData}
               });
            });
        }

        private void OnWrongAnswer()
        {
            _uiSystem.ShowWindow<FailPopup>();
        }

        private void OnAllQuestionAnswers()
        {
            _uiSystem.ShowWindow<ResultPopup>();
        }
    }
}