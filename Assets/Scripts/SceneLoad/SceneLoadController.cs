using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MinerMess.StudioTiziu
{
    /// <summary>
    /// Clase responsável por gerenciar load de scenes
    /// </summary>
    public class SceneLoadController : MessageController<SceneLoadMessage>
    {
        private static SceneLoadController _instance;
        private void Awake()
        {
            if (ReferenceEquals(_instance, null))
            {
                _instance = this;
                DontDestroyOnLoad(gameObject); 
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
        protected override bool MessageHandler(Message message)
        {
            SceneLoadMessage slM = message as SceneLoadMessage;

            if (!ReferenceEquals(slM, null))
            {
                switch (slM.Fade.Item1)
                {
                    case "FadeOut":
                        StartCoroutine(Coroutine_SceneLoadWithFade(slM.Fade.Item2, slM.SceneID, slM.OnStartLoad));
                        break;
                }
                return true;
            }
            return false;
        }

        private IEnumerator Coroutine_SceneLoadWithFade(float item2, int sceneID, Action onStartLoad)
        {
            MessageSystem.Instance.Notify(new FadeMessage("FadeOut", item2));
            yield return new WaitForSeconds(item2);
            if (!ReferenceEquals(onStartLoad, null))
            {
                onStartLoad?.Invoke();
            } 
            AsyncOperation newScene = SceneManager.LoadSceneAsync(sceneID);
            newScene.allowSceneActivation = false;

            while(newScene.progress > 0.8f)
            {  
                yield return null;
            }
            newScene.allowSceneActivation = true;
        }
    }
} 
