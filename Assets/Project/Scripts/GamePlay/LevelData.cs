using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project
{
    [CreateAssetMenu(menuName = "Scriptable/LavelData", fileName = "LevelData", order = 0)]
    public class LevelData : ScriptableObject
    {
        [Serializable]
        public class AnswerPreset
        {
            [field: SerializeField]
            public string Answer
            {
                get;
                private set;
            }

            [field: SerializeField]
            public bool IsCorrect
            {
                get;
                private set;
            }
        }

        [Serializable]
        public class QuestionPreset
        {
            [field: SerializeField, PreviewField(200, ObjectFieldAlignment.Center), HideLabel]
            public Sprite Image
            {
                get;
                private set;
            }

            [field: SerializeField, TableList]
            public AnswerPreset[] Answers
            {
                get;
                private set;
            } = new AnswerPreset[4];

            public void SetAnswer(AnswerPreset[] presets)
            {
                Answers = presets;
            }
        }

        [field: SerializeField, TableList]
        public QuestionPreset[] QuestionPresets
        {
            get;
            private set;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            for (int i = 0; i < QuestionPresets.Length; i++)
            {
                var answerPresets = QuestionPresets[i].Answers;

                if (answerPresets.Length >= 4)
                {
                    var presets = new AnswerPreset[4];

                    for (int j = 0; j < 4; j++)
                    {
                        presets[j] = answerPresets[j];
                    }

                    QuestionPresets[i].SetAnswer(presets);
                }
            }      
        }  
#endif
    }
}