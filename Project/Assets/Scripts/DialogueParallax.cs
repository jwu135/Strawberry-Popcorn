using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueParallax : MonoBehaviour
{
    public GameObject interactSign;
    public GameObject dialogueBox;
    private Dialogue[] dialogue = new Dialogue[7];
    private bool over = false;
    private bool activatedDialogue = false;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < dialogue.Length; i++) {
            dialogue[i] = new Dialogue();
        }
        dialogue[0].sentences = "Praise be to Mother for the life she gave us.";
        dialogue[1].sentences = "Praise be to Mother for the purpose she gave us.";
        dialogue[2].sentences = "Hello There!";
        dialogue[3].sentences = "You smell kinda weird. Are you Ok?";
        dialogue[4].sentences = "I like your eyes! That's new.";
        dialogue[5].sentences = "Mother is in the next door, just waiting for us to become ready.";
        dialogue[6].sentences = "Can't wait to meet Mother!";
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player") {
            interactSign.SetActive(true);
            over = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        interactSign.SetActive(false);
        over = false;
        if (activatedDialogue) {
            dialogueBox.transform.parent.gameObject.SetActive(false);
            activatedDialogue = false;
        }
    }
    void talk()
    {
        if (!activatedDialogue) {
            int rand = Random.Range(0, dialogue.Length);
            dialogueBox.transform.parent.gameObject.SetActive(true);
            interactSign.SetActive(false);
            dialogueBox.transform.FindChild("Sentence").GetComponent<Text>().text = dialogue[rand].sentences;
            activatedDialogue = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("interact") && over) {
            talk();
        }
    }
}
