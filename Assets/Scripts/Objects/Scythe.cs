using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Scythe : MonoBehaviour, ICutting
{
    [SerializeField] private LayerMask _mask;
    private Collider _collider;

    // Start is called before the first frame update
    void Awake()
    {
        
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
        ToggleCollider(false); 
        GetComponent<Rigidbody>().useGravity = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & _mask) != 0)
        {
            if (other.TryGetComponent<WheatObject>(out var wheat)) wheat.Cut();
        }
    }

    public void ToggleCollider(bool value)
    {
        _collider.enabled = value;
    }

}
