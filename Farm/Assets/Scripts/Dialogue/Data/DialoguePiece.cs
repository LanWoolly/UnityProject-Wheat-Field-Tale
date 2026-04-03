using System;
using UnityEngine;
// using UnityEngine.Events;

namespace Farm.Dialogue
{
    [Serializable]
    public class DialoguePiece
    {
        [Header("对话详情")]
        public Sprite faceImage;
        public bool onLeft;
        public string name;
        [TextArea]
        public string dialogueText;
        public bool hasToPause;
        [HideInInspector] public bool isDone;

        // public UnityEvent afterTalkEvent;
    }
}
