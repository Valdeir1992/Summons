using MinerMess.StudioTiziu;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MinerMess.StudioTiziu
{ 
    /// <summary>
    /// Clase responsável por gerenciar efeito de transicao
    /// </summary>
    public class FadeController: MessageController<FadeMessage>
    {
        private static FadeController _instance;
        private Image _background;
        private void Awake()
        {
            if (ReferenceEquals(_instance, null))
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
                _background = transform.GetChild(0).GetChild(0).gameObject.GetComponent<Image>();
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
        protected override bool MessageHandler(Message message)
        {
            FadeMessage fM = message as FadeMessage;
            if (!ReferenceEquals(fM, null))
            {
                if (fM.Action == "FadeIn")
                {
                    StartCoroutine(Coroutine_FadeIn(fM));
                }
                else if (fM.Action == "FadeOut")
                {
                    StartCoroutine(Coroutine_FadeOut(fM));
                }
                return true;
            }
            return false;
        }

        private IEnumerator Coroutine_FadeOut(FadeMessage fM)
        {
            if (!ReferenceEquals(fM.OnStartFade, null))
            {
                fM.OnStartFade?.Invoke();
            }
            Color finalColor = new Color(0, 0, 0, 1);
            for (float timeElaped = 0; timeElaped <= 1; timeElaped += Time.deltaTime / fM.Time)
            {
                _background.color = Color.Lerp(_background.color, finalColor, timeElaped);
                yield return null;
            }
            if (!ReferenceEquals(fM.OnEndFade, null))
            {
                fM.OnEndFade?.Invoke();
            }
        }

        private IEnumerator Coroutine_FadeIn(FadeMessage fM)
        {
            if (!ReferenceEquals(fM.OnStartFade, null))
            {
                fM.OnStartFade?.Invoke();
            }
            Color finalColor = new Color(0, 0, 0, 0);
            for (float timeElaped = 0; timeElaped <= 1; timeElaped += Time.deltaTime / fM.Time)
            {
                _background.color = Color.Lerp(_background.color, finalColor, timeElaped);
                yield return null;
            }
            if (!ReferenceEquals(fM.OnEndFade, null))
            {
                fM.OnEndFade?.Invoke();
            }
        }
    }

    public class FadeMessage : Message
    {
        public readonly string Action;
        public readonly float Time;
        public readonly Action OnStartFade;
        public readonly Action OnEndFade;

        public FadeMessage(string action, float time, Action onStart = null, Action onEnd = null)
        {
            Action = action;
            Time = time;
            OnStartFade = onStart;
            OnEndFade = onEnd;
        }
    }

}