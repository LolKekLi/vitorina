using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class UISystem : MonoBehaviour
    {
        private IWindow[] _windows = null;
        private List<IWindow> _openWindows = null;

        public Dictionary<string, object> WindowData
        {
            get;
            private set;
        }

        private void Awake()
        {
            _windows = GetComponentsInChildren<IWindow>(true);
            _openWindows = new List<IWindow>();

            for (int i = 0; i < _windows.Length; i++)
            {
                _windows[i].Prepare(this);

                if (_windows[i].GetType() == typeof(MainWindow))
                {
                    _windows[i].Show();
                    _openWindows.Add(_windows[i]);
                }
                else
                {
                    _windows[i].Hide();
                }
            }
        }

        public void ShowWindow<T>(Dictionary<string, object> data = null)
        {
            WindowData = data;
            
            for (int i = 0; i < _windows.Length; i++)
            {
                var window = _windows[i];

                if (window.GetType() == typeof(T))
                {
                    window.Show();

                    if (!window.IsPopup)
                    {
                        for (int j = 0; j < _openWindows.Count; j++)
                        {
                            _openWindows[j].Hide();
                        }
                        
                        _openWindows.Clear();
                    }

                    _openWindows.Add(window);

                    return;
                }
            }
        }
    }
}