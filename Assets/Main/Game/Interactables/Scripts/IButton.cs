using System;
public interface IButton {
    public event Action OnPress;   
    public void Designate();
}
