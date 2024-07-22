using TMPro;

using Unity;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CardUI : MonoBehaviour {
    [SerializeField] public Image cardArt;
    [SerializeField] public TMP_Text costTMP;
    [SerializeField] public TMP_Text titleTMP;
    [SerializeField] public TMP_Text descriptionTMP;

    public UnityEvent MouseEntered;
    public void InvokeMouseEntered() {
        MouseEntered.Invoke();
    }

    public UnityEvent MouseExited;
    public void InvokeMouseExited() {
        MouseExited.Invoke();
    }

    public UnityEvent OnClick;
    public void InvokeOnClick() {
        OnClick.Invoke();
    }
}