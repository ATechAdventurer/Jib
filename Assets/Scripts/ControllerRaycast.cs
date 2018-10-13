using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class ControllerRaycast : MonoBehaviour {
    private MLInputController _controller;
    private bool _holding = false;
    private GameObject _heldObject = null;
    public enum ButtonState
    {
        InActive,
        Active
    }
    // Use this for initialization
    void Start () {
        if (!(MLInput.Start().IsOk))
        {
            Debug.LogError("Problem with MLInput");
            enabled = false;
            return;
        }
        _controller = MLInput.GetController(MLInput.Hand.Right);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
       if(_holding && _controller.TriggerValue < .2)
        {
            _holding = false;
        }
        else if(_holding){
            _heldObject.transform.position = _controller.Position;
        }
    }
    void OnDestroy()
    {
        MLInput.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Person" && _controller.TriggerValue > .2)
        {
            _holding = true;
            _heldObject = other.gameObject;
        }
    }
}
