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
        // Mengecek apakah pintu dikunci
        if (_isLocked == true)
        {
            // Jika pintu dikunci
            // Mengecek apakah player memiliki kuncinya di inventory 
            // dengan menggunakan ID nya
            bool hasKey = character.Inventory.CheckItem(_keyID);
            if (hasKey == true)
            {
                // Jika punya maka mengubah status pintu menajdi tidak terkunci
                _isLocked = false;
                // Kemudian buka pintu
                Open();
            }
        }
        else
        {
            // Jika tidak terkunci atau kunci telah dibuka
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