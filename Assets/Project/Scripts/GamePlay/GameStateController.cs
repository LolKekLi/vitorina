using UnityEngine;

namespace Project
{
    public class GameStateController : MonoBehaviour
    {
        [SerializeField]
        private QuestionController _questionController = null;
        [SerializeField]
        private LevelController _levelController = null;

        private void Start()
        {
            _questionController.gameObject.SetActive(false);
            _levelController.gameObject.SetActive(true);

            _questionController.Prepare(OnWrongAnswer, OnAllQuestionAnswers);
            
            _levelController.Prepare(() =>
            {
                _levelController.gameObject.SetActive(false);
                _questionController.gameObject.SetActive(true);
            });
        }

        private void OnWrongAnswer()
        {
            _questionController.gameObject.SetActive(false);
            _levelController.gameObject.SetActive(true);
        }

        private void OnAllQuestionAnswers()
        {
           _questionController.gameObject.SetActive(false);
           _levelController.gameObject.SetActive(true);
        }
    }
}