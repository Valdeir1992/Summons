using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGameplayController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _soultTextView;

    private void OnEnable()
    {
        GameplayManager.OnCollectSoul += UpdateText;
    }
    private void OnDisable()
    {
        GameplayManager.OnCollectSoul -= UpdateText;
    }

    public void UpdateText(short value)
    {
        _soultTextView.text = $"x{value}";
    }
}
