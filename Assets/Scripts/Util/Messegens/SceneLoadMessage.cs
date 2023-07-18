using MinerMess.StudioTiziu;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadMessage : Message
{
    public readonly (string,float) Fade; 
    public readonly int SceneID;
    public readonly Action OnStartLoad;

    public SceneLoadMessage((string,float) fade, int sceneID, Action onStartLoad = null)
    {
        Fade = fade;
        SceneID = sceneID;
        OnStartLoad = onStartLoad;
    }
}
