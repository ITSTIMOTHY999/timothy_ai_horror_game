using UnityEngine;
 
public class Door : MonoBehaviour, IInteractable
{
    public UnityEvent OnDoorOpen;
    public UnityEvent OnDoorClose;

    [SerializeField]
    private string _name;
    [SerializeField]
    protected Transform _doorTransform;
    [SerializeField]
    protected float _duration = 1f;
    [SerializeField]
    protected bool _isLocked;
    [SerializeField]
    protected string _keyID;
    protected bool _isAnimating;
    protected bool _isOpen;
    public bool IsAnimating => _isAnimating;
    public string Name => _name;

    public void Interact()
    {
        // Mengecek apakah pintu sedang terbuka
        if (_isOpen == true)
        {
            // Jika pintu terbuka maka tutup pintu
            Close();
        }
        else
        {
            // Jika pintu tertutup maka buka pintu
            Open();
        }
    }

    public void Open()
    {
        _isOpen = true;
        OnDoorOpen?.Invoke();
    }

    public void Close()
    {
        _isOpen = false;
        OnDoorClose?.Invoke();
    }
}