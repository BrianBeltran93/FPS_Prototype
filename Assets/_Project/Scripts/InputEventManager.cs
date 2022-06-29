using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputEventManager : MonoBehaviour
{
    public static event Action RightTriggerPressed;
    public static event Action RightTriggerHeld;

    private bool _isRightButtonStillHeldDown;
    private bool _isLeftButtonStillHeldDown;

    private static bool IsRightGripPressed => OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) >= 0.9f;
    private static bool IsRightTriggerPressed => OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) >= 0.8f;
    private static bool IsRightTriggerLetGo => OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) <= 0.2f;
    private static bool IsLeftTriggerLetGo => OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) <= 0.2f;
    private static bool IsLeftTriggerPressed => OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) >= 0.8f;
    private static bool IsLeftGripPressed => OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) >= 0.9f;

    
    private void Update()
    {
        RightTriggerPress();
        LeftTriggerPress();

        RightTriggerHold();
        LeftTriggerHold();
    }

    private void RightTriggerPress()
    {
        if (IsRightGripPressed && IsRightTriggerPressed && !_isRightButtonStillHeldDown)
        {
            if (RightTriggerPressed == null) 
                return;
            
            RightTriggerPressed.Invoke();
            _isRightButtonStillHeldDown = true;
        }
        else if (IsRightTriggerLetGo && _isRightButtonStillHeldDown)
        {
            _isRightButtonStillHeldDown = false;
        }
    }

    private void LeftTriggerPress()
    {
        if (IsLeftGripPressed && IsLeftTriggerPressed && !_isLeftButtonStillHeldDown)
        {
            if (RightTriggerPressed == null) 
                return;
            
            RightTriggerPressed.Invoke();
            _isLeftButtonStillHeldDown = true;
        }
        else if (IsLeftTriggerLetGo && _isLeftButtonStillHeldDown)
        {
            _isLeftButtonStillHeldDown = false;
        }
    }

    private static void RightTriggerHold()
    {
        if (!IsRightGripPressed || !IsRightTriggerPressed) return;
        
        if (RightTriggerHeld != null)
        {
            RightTriggerHeld.Invoke();
        }
    }
    
    private static void LeftTriggerHold()
    {
        if (!IsLeftGripPressed || !IsLeftTriggerPressed) 
            return;
        
        if (RightTriggerHeld != null)
        {
            RightTriggerHeld.Invoke();
        }
    }
}
