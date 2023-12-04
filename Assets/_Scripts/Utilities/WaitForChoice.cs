using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

public class WaitForChoice : CustomYieldInstruction {

    public override bool keepWaiting { get { return flag; } }
    private bool flag;

    public WaitForChoice(Story story) {
        flag = true;
        story.onMakeChoice += choice => flag = false;
    }

}
