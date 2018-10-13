using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class MagicRigController : MonoBehaviour {
    private MLInputController _controller;
    private enum ButtonState
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
	void Update () {
        //Move the controller into postion
        this.gameObject.transform.position = _controller.Position;
        this.gameObject.transform.rotation = _controller.Orientation;

        //Check Button Input
        var buttonState = _controller.State.ButtonState[(int)MLInputControllerButton.Bumper];
        if (buttonState == (int)ButtonState.Active)
        {
            var a = 3;
        }
	}

    void OnDestroy()
    {
        MLInput.Stop();
    }
}
