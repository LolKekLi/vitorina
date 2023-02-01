using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Project
{
    public class MainWindow : MonoBehaviour, IWindow
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

            [field: SerializeField]
            public DOTweenAnimation OnClickTween
            {
                get;
                private set;
            }
        }

        public bool IsPopup
        {
            get =>
                false;
        }

        [SerializeField]
        private AnswerButton[] _levelButtons = null;

        private UISystem _uiSystem;

        public void Prepare(UISystem uiSystem)
        {
            _uiSystem = uiSystem;
        }

        public void Setup(Action<LevelData> onLevelButtonClick)
        {
            for (int i = 0; i < _levelButtons.Length; i++)
            {
                var i1 = i;

                _levelButtons[i].Button.onClick.AddListener(() =>
                {
                    var levelButton = _levelButtons[i1];

                    StartCoroutine(OnButtonClick(onLevelButtonClick, levelButton));
                });
            }
        }

        private IEnumerator OnButtonClick(Action<LevelData> onLevelButtonClick, AnswerButton levelButton)
        {
            var tween = levelButton.OnClickTween;
            
            tween.DOPlay();

            yield return new WaitForSeconds(tween.duration * tween.loops);

            onLevelButtonClick?.Invoke(levelButton.LevelData);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}