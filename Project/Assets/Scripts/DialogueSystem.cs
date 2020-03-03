using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public GameObject dialogueBox;
    //public GameObject cutsceneSystem;
    public Text name;
    public Text sentence;
    [HideInInspector]
    public bool done = false;
    private int index = 0;
    public Dialogue[] dialogue;
    public void Start()
    {
        name.text = dialogue[index].name;
        sentence.text = dialogue[index].sentences;  
    }
    public void Update()
    {
        if (Input.GetButtonDown("Jump")){
            if (dialogue.Length > index + 1) {
                index++;
            } else {
                dialogueBox.SetActive(false);
                GetComponent<CutsceneSystem>().DialogueDone();
                Destroy(this);
            }
            name.text = dialogue[index].name;
            sentence.text = dialogue[index].sentences;
        }
    }
}
