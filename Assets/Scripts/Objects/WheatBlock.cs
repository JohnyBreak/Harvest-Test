using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WheatBlock : MonoBehaviour, ICollectable
{
    [SerializeField] private int _price;
    private Collider _collider;
    private Vector3 _initialScale;

    public Collider Collider => _collider;
    public Vector3 InitialScale => _initialScale;
    public int Price => _price;

    public void Collect()
    {
        _collider.enabled = false;
    }

    private void Awake()
    {
        _collider = GetComponent<Collider>();
           _initialScale = transform.localScale;
    }
}
