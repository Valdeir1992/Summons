using UnityEngine;

namespace MinerMess.StudioTiziu
{ 
    /// <summary>
    /// Classe base responsável por implementar sistema de mensageria desacoplado.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MessageController<T> : MonoBehaviour where T : Message
    {
        protected virtual void OnEnable()
        {
            MessageSystem.Instance.Register<T>(this.MessageHandler);
        }

        protected virtual void OnDisable()
        {
            if (!MessageSystem.Ative) return;
            MessageSystem.Instance.UnRegister<T>(this.MessageHandler);
        }

        /// <summary>
        /// Método responsável por lidar com mensagens recebidas.
        /// </summary>
        /// <param name="message">Mensagem recebida</param>
        /// <returns></returns>
        protected abstract bool MessageHandler(Message message);
    }
}