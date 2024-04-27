using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OSKeyRegular : MonoBehaviour
{
    [SerializeField] private Button          _button;
    [SerializeField] private TextMeshProUGUI _charDisplay;

    public void Init(string letter, OSK osk)
    {
        _charDisplay.text = letter;
        _button.onClick.AddListener(() =>
        {
            osk.AlphabetFunction(letter);
        });
    }
}