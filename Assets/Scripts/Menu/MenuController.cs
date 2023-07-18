using MinerMess.StudioTiziu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button _btnStart;
    [SerializeField] private Button _btnCredits;

    private void Awake()
    {
        _btnStart.onClick.AddListener(() =>
        {
            MessageSystem.Instance.Notify(new SceneLoadMessage(("FadeOut",1),1));
        });
    }
    private void Start()
    {
        MessageSystem.Instance.Notify(new FadeMessage("FadeIn",1)); 
    }
}
