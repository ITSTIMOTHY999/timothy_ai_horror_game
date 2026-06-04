using UnityEngine;
using UnityEngine.Events;
 
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
    protected Coroutine _animatingDoorCoroutine;
    public bool IsAnimating => _isAnimating;
    public string Name => _name;

    [ContextMenu("Interact Door")]
    public void Interact(PlayerCharacter character)
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

    public virtual void Open()
    {
        _isOpen = true;
        OnDoorOpen?.Invoke();
    }

    public virtual void Close()
    {
        _isOpen = false;
        OnDoorClose?.Invoke();
    }
}