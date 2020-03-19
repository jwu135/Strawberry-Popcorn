using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public Sprite[] box = new Sprite[3];
    public GameObject dialogueBox;
    public new Text name;
    public Text sentence;
    public float textspeed;
    [HideInInspector]
    public bool done = false;
    private int index = 0;
    private string currSentence;
    private string finalSentence;
    public Dialogue[] dialogue;
    public int[] stops;
    private int stopsIndex;
    bool continuing = false;
    bool continuing2 = false;
    public void Start()
    {
        finalSentence = dialogue[index].sentences;
        currSentence = "";  

        name.text = dialogue[index].name;
        sentence.text = currSentence;
        StartCoroutine("textScroll");
    }
    void Update()
    {
        if (Time.timeScale != 0) {
            lookAround();
        }
    }
    public void restart()
    {
        finalSentence = dialogue[index].sentences;
        currSentence = "";
        continuing = true;
        name.text = dialogue[index].name;
        sentence.text = currSentence;
        StartCoroutine("textScroll");
    }
    public void restart2()
    {
        index++;
        finalSentence = dialogue[index].sentences;
        currSentence = "";
        continuing = false;
        continuing2 = true;
        name.text = dialogue[index].name;
        sentence.text = currSentence;
        StartCoroutine("textScroll");

    }
    public void lookAround()
    {
        if (Input.GetButtonDown("Jump")){
            if (currSentence.Length < finalSentence.Length) {
                sentence.text = currSentence = finalSentence;
            } else {
                if (stops[stopsIndex] > index + 1) {
                    index++;
                } else {
                    stopsIndex++;
                    dialogueBox.SetActive(false);
                    if (continuing) {
                        GetComponent<CutsceneSystem>().StartCoroutine("buffer");
                    }else if(continuing2)
                        GetComponent<CutsceneSystem>().DialogueDone(true);
                    else
                        GetComponent<CutsceneSystem>().DialogueDone();
                    
                }
                name.text = dialogue[index].name;
                if (dialogue[index].name == "Mother:") {
                    dialogueBox.GetComponent<Image>().sprite = box[0];
                } else if (dialogue[index].name == "Strawberry Popcorn:") {
                    dialogueBox.GetComponent<Image>().sprite = box[1];
                } else {
                    dialogueBox.GetComponent<Image>().sprite = box[2];
                }
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
