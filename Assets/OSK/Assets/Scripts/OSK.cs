using DG.Tweening;
using TMPro;
using UnityEngine;

public class OSK : MonoBehaviour
{
    public UIController   uiController;
    public CanvasGroup    canvasGroup;
    public TMP_InputField textField;
    public GameObject     engLower, engUpper;
    public GameObject     clearBtn;
    
    [SerializeField] private float duration = 1f;
    [SerializeField] private Ease  ease = Ease.InSine;

    public void AlphabetFunction(string alphabet)
    {
        textField.text += alphabet;
        clearBtn.SetActive(true);
    }

    public void SpaceFunction()
    {
        AlphabetFunction(" ");
    }

    public void BackSpace()
    {
        if(textField.text.Length>0) textField.text= textField.text.Remove(textField.text.Length-1);
        else clearBtn.SetActive(false);
    }

    public void ClearInput()
    {
        textField.text = "";
        clearBtn.SetActive(false);
    }

    public void Search()
    {
        uiController.CloseOSK();
    }

    public void CloseAllLayouts()
    {
        engLower.SetActive(false);
        engUpper.SetActive(false);
    }

    public void ShowLayout(GameObject SetLayout)
    {
        CloseAllLayouts();
        SetLayout.SetActive(true);
    }

    public void Show()
    {
        UIController.AnimateAlpha(canvasGroup,1f, duration,ease);
    }

    public void Hide()
    {
        UIController.AnimateAlpha(canvasGroup,0f, duration,ease);
    }
}