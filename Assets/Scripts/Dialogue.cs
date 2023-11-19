using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour {

    [SerializeField] private TextAsset dialogueText;

    private void StartDialogue() => DialogueManager.Instance.ProcessDialogue(dialogueText);

}
