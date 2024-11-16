using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private Vector2 inputStart;
    private Vector2 currentInput;
    private float screenWidth;
    private Vector2 currentDelta;
    private float smoothDampRef;
    

    private bool isInputActive = false;


    void Start()
    {
        screenWidth = Screen.width;
        Cursor.lockState = CursorLockMode.Confined;
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
    }

    void StopInputTracking(Vector2 position)
    {
        currentInput = position;
        isInputActive = false;
        Managers.EventManager.Instance.ONOnStopRotation();
    }

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
