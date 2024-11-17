using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitMotion;
using LitMotion.Adapters;
public class InputHandler : MonoBehaviour
{
    private Vector2 inputStart;
    private Vector2 currentInput;
    private float screenWidth;
    private Vector2 currentDelta;
    private float smoothDampRef;
    

    private bool isInputActive = false;
    private bool isGameStarted = false;

    private void Awake() 
    {
        MotionDispatcher.Clear();
        MotionDispatcher.EnsureStorageCapacity<Vector3, NoOptions, Vector3MotionAdapter>(500);
        MotionDispatcher.EnsureStorageCapacity<float, NoOptions, FloatMotionAdapter>(500);
        
    }

    void Start()
    {
        screenWidth = Screen.width;
        CacheEvents();
    }

    private void CacheEvents()
    {
        Managers.EventManager.Instance.OnMouseDown += StartInputTracking;
        Managers.EventManager.Instance.OnMouseUp += StopInputTracking;
    }

    private void Update()
    {
        TrackInput();
    }

    
    void StartInputTracking(Vector2 position) 
    {
        inputStart = position;
        isInputActive = true;
        if(!isGameStarted)
        {
            isGameStarted = true;
            Managers.EventManager.Instance.OnONLevelStart();
        }
    }

    void StopInputTracking(Vector2 position)
    {
        currentInput = position;
        isInputActive = false;
        Managers.EventManager.Instance.ONOnStopRotation();
    }

    //Sends the delta of the input for rotations and movements
    private void TrackInput()
    {
        if(!isInputActive) return;
        currentInput = Input.mousePosition;
        inputStart.x = Mathf.SmoothDamp(inputStart.x, currentInput.x, ref smoothDampRef, 2f);
        currentDelta = currentInput - inputStart;
        currentDelta.x = Mathf.Clamp(currentDelta.x,screenWidth * -0.5f, screenWidth * 0.5f);
        Managers.EventManager.Instance.ONOnSendCurrentDelta(currentDelta / screenWidth);
    }
}
