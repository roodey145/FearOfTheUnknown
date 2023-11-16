using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabKey : MonoBehaviour
{
    [SerializeField] private GameObject _keyToGrab;
    [SerializeField] private GameObject _keyToActivate;
    // Start is called before the first frame update
    void Start()
    {
        if (_keyToGrab == null || _keyToActivate == null) throw new System.Exception("Please assign the varaibles _keyToGrab and _KeyToActivate");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == _keyToGrab)
        {
            _keyToGrab.SetActive(false);
            _keyToActivate.SetActive(true);
        }
    }
}
