using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class SetupScript : MonoBehaviour
{
    private bool _isSetup = true;
    private bool _isLocked = false;
    private bool _bounce = true;
    private MLInputController _controller;
    public enum ButtonState
    {
        InActive,
        Active
    }
    public enum GameState
    {
        Setup,
        Game
    }

    public GameState Mode;
    public GameObject Terrain;
    public Transform RayCastObject;
    public Quaternion RotationDebug;
    // Use this for initialization
    void Start()
    {
        Mode = GameState.Setup;
        if (!(MLInput.Start().IsOk))
        {
            Debug.LogError("Problem with MLInput");
            enabled = false;
            return;
        }
        _controller = MLInput.GetController(MLInput.Hand.Right);
    }

    // Update is called once per frame
    void Update()
    {
        if (_controller.State.ButtonState[(int)MLInputControllerButton.HomeTap] == (int)ButtonState.Active && _bounce)
        {
            Mode = Mode == GameState.Setup ? GameState.Game : GameState.Setup;
            Terrain.transform.position = RayCastObject.transform.position;
            Debug.Log(Mode);
            _bounce = false;
        }
        else {
            _bounce = true;
        }
        switch (Mode)
        {

            case GameState.Setup:
                if (_controller.TriggerValue > .2 && _bounce)
                {
                    _isLocked = !_isLocked;
                    
                    //Terrain.transform.parent = (_isLocked) ? null : RayCastObject;
                    _bounce = false;
                }
                else if (_controller.TriggerValue < .2 && !_bounce)
                {
                    _bounce = true;
                }
                break;
            case GameState.Game:
                //TODO
                break;
        }

    }


    void OnDestroy()
    {
        MLInput.Stop();
    }
}

