using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Garaj.GLog
{
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(CanvasRenderer))]
    [RequireComponent(typeof(GraphicRaycaster))]
    public class OnScreenLog : SingletonPersistentFromResource<OnScreenLog>
    {
        private Canvas canvas;
        private static int _num;
        private const int Capacity = 100;
        [SerializeField] private GameObject panel;
        [SerializeField] private TMP_Text text;
        private void Start()
        {
            canvas = gameObject.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = Camera.main;
            canvas.sortingLayerName = "GOD";
            canvas.sortingOrder = 500;

            _num = 0;
            Add("Start");
            panel.SetActive(false);
        }

        public static void Add(string log)
        {
            try
            {
                if (_num > Capacity)
                {
                    Ins.text.text += "";
                    _num = 0;
                }
                if(Ins.canvas.worldCamera is null)
                    Ins.canvas.worldCamera = Camera.main;
                Ins.text.text += $"\n{log}";
                _num++;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static void ShowHide() => Ins.panel.SetActive(!Ins.panel.activeSelf);
    }
}