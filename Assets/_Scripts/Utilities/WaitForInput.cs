using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Ink.Runtime;
using System.Reflection;

public class WaitForInput : CustomYieldInstruction {
    public override bool keepWaiting { get { return flag; } }
    private bool flag;

    public WaitForInput(InputAction inputAction) {
        flag = true;
        inputAction.performed += context => flag = false;
    }
}
