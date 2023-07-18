using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MinerMess.StudioTiziu
{
    /// <summary>
    /// Classe responsável por gerenciar envio de mensagens.
    /// </summary>
    public sealed class MessageSystem : GenericSingleton<MessageSystem>
    {
        private Dictionary<Type, List<Func<Message, bool>>> _dicListeners = new Dictionary<Type, List<Func<Message, bool>>>();

        private void Awake()
        {
            if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// Método responsável por registrar ouvites das mensagens.
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="messageHandler">Ação executada pelo ouvinte</param>
        /// <returns></returns>
        public bool Register<U>(Func<Message, bool> messageHandler) where U : Message
        {
            Type messageType = typeof(U);
            if (_dicListeners.ContainsKey(messageType))
            {
                List<Func<Message, bool>> list = _dicListeners[messageType];

                foreach (var item in list)
                {
                    if (item == messageHandler)
                    {
                        return false;
                    }
                }
                list.Add(messageHandler);
                return true;
            }
            else
            {
                List<Func<Message, bool>> list = new List<Func<Message, bool>>();
                list.Add(messageHandler);
                _dicListeners.Add(messageType, list);
                return true;
            }
        }

        /// <summary>
        /// Método responsável por notificar ouvintes do recebimento de mensagens.
        /// </summary>
        /// <param name="message">Mensagem recebida</param>
        /// <returns></returns>
        public bool Notify(Message message)
        {
            Type messageType = message.GetType();
            if (_dicListeners.ContainsKey(messageType))
            {
                foreach (var item in _dicListeners[messageType])
                {
                    item.Invoke(message);
                }
            }
            return false;
        }

        /// <summary>
        /// Método responsável por notificar ouvintes do recebimento de mensagens com tempo de espera.
        /// </summary>
        /// <param name="message">Mensagem recebida</param>
        /// <param name="time">Tempo de espera</param>
        /// <returns></returns>
        public bool Notify(Message message, float time)
        {
            StartCoroutine(Coroutine_SendMessage(message, time));
            return true;
        }

        /// <summary>
        /// Método responsável por gerenciar envio de mensagem.
        /// </summary>
        /// <param name="message">Messagem enviada</param>
        /// <param name="time">Tempo de espera</param>
        /// <returns></returns>
        private IEnumerator Coroutine_SendMessage(Message message, float time)
        {
            yield return new WaitForSeconds(time);
            Notify(message);
        }

        /// <summary>
        /// Método responsável remover registro de ouvinte.
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="messageHandler">Ação executada pelo ouvinte</param>
        /// <returns></returns>
        public bool UnRegister<U>(Func<Message, bool> messageHandler)
        {
            Type messageType = typeof(U);
            if (_dicListeners.ContainsKey(messageType))
            {
                List<Func<Message, bool>> list = _dicListeners[messageType];

                if (list.Contains(messageHandler))
                {
                    list.Remove(messageHandler);
                    return true;
                }
            }
            return false;
        }
    }
}
