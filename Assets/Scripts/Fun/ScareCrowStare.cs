using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareCrowStare : MonoBehaviour
{
    [SerializeField] private Transform _player;

    // Update is called once per frame
    void Update()
    {
        if (_player == null) return;

        transform.LookAt(_player, Vector3.up);
    }
}
