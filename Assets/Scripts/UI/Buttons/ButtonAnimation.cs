using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonAnimation : MonoBehaviour,IPointerDownHandler
{
    private RectTransform rect;
        private readonly Vector3 defaultScale = new Vector3(1f, 1f, 1f);
        private readonly Vector3 pressedDownScale = new Vector3(.8f, .8f, .8f);
        private Coroutine popRoutine;

        private bool disabled;

        public bool Disabled
        {
            get => disabled;
            set => disabled = value;
        }

        void Start()
        {
            rect = GetComponent<RectTransform>();
            
        }

        

        public void OnPointerDown(PointerEventData eventData)
        {
            if (Disabled) return;
            rect.localScale = pressedDownScale;
            
            popRoutine = StartCoroutine(FramePopRoutine());
        }

        IEnumerator FramePopRoutine() 
        {
            
            var counter = 0f;
            while (counter<=.5f)
            {
                var t = Easings.QuadEaseOut(counter, 0f, 1f, .5f);
                var s = Mathf.Lerp(.8f, 1f, t);
                rect.localScale = defaultScale * s;
                counter += Time.deltaTime;
                yield return null;
            }
            rect.localScale = defaultScale;

        }
}
