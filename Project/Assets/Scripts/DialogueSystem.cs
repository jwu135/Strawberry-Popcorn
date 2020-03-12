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
    public float textspeed;
    [HideInInspector]
    public bool done = false;
    private int index = 0;
    private string currSentence;
    private string finalSentence;
    public Dialogue[] dialogue;
    public void Start()
    {
        finalSentence = dialogue[index].sentences;
        currSentence = "";  

        name.text = dialogue[index].name;
        sentence.text = currSentence;
        StartCoroutine("textScroll");
    }
    public void Update()
    {
        if (Input.GetButtonDown("Jump")){
            if (currSentence.Length < finalSentence.Length) {
                sentence.text = currSentence = finalSentence;
            } else {
                if (dialogue.Length > index + 1) {
                    index++;
                } else {
                    dialogueBox.SetActive(false);
                    GetComponent<CutsceneSystem>().DialogueDone();
                    Destroy(this);
                }
                name.text = dialogue[index].name;
                finalSentence = dialogue[index].sentences;
                currSentence = "";
                StartCoroutine("textScroll");
            }
        }
        
        
    }
    IEnumerator textScroll()
    {
        if (currSentence.Length < finalSentence.Length) {   
            currSentence += finalSentence[currSentence.Length];
            sentence.text = currSentence;
            yield return new WaitForSeconds(textspeed);
            StartCoroutine("textScroll");
        }

    }
}
