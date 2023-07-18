using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class Soul : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Lord")) return;
        GameplayManager.AddSoul(1);
        Addressables.ReleaseInstance(gameObject);
    }
}
