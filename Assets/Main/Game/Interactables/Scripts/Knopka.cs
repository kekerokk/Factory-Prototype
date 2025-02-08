using System;
using UnityEngine;
using UnityEngine.Events;

public class Knopka : MonoBehaviour, IButton {
    [SerializeField] UIPointer _pointer;
    
    public UnityEvent OnPressEvent;
    public event Action OnPress = delegate { };
    
    public void Designate() {
        _pointer.Activate();
    }
    
    void OnMouseDown() {
        OnPress.Invoke();
        OnPressEvent.Invoke();
    }
}