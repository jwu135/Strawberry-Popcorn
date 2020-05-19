using DragonBones;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueParallax : MonoBehaviour
{
    public GameObject armature;
    public GameObject dialogueBox;
    public GameObject interactSign;
    private Dialogue[] dialogue = new Dialogue[7];
    private bool over = false;
    private bool activatedDialogue = false;
    private Vector3 defaultDirection;
    private string defaultAction;
    private float textspeed = 0.025f;
    private float baseTextSpeed;
    private int tempSwapper;
    private string finalSentence;
    private string currSentence;
    public int rand;
    // Start is called before the first frame update
    void Start()
    {
        baseTextSpeed = textspeed;
        defaultDirection = armature.transform.localScale; // default is to the left
        defaultAction = armature.GetComponent<UnityArmatureComponent>().animation.lastAnimationName;
        for (int i = 0; i < dialogue.Length; i++) {
            dialogue[i] = new Dialogue();
        }
        dialogue[0].sentences = "Praise be to Mother for the life she gave us.";
        dialogue[1].sentences = "Praise be to Mother for the purpose she gave us.";
        dialogue[2].sentences = "Hello There!";
        dialogue[3].sentences = "You smell kinda weird. Are you Ok?";
        dialogue[4].sentences = "I like your eyes! That's new.";
        dialogue[5].sentences = "Mother is in the next door, just waiting for us to become ready.";
        dialogue[6].sentences = "Can't wait to meet Mother!";
        //rand = Random.Range(0, dialogue.Length);
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
            armature.GetComponent<UnityArmatureComponent>().animation.Play(defaultAction);
            lookAtPlayer(false);
            StopCoroutine("textScroll");
        }
    }
    void lookAtPlayer(bool t)
    {
        float direction = GameObject.FindGameObjectWithTag("Player").transform.position.x - transform.position.x; // below 0 is left, above 0 is right
        int mult = 1;
        if (direction > 0)
            mult *= -1;
        Vector3 absDirection = new Vector3(Mathf.Abs(defaultDirection.x), Mathf.Abs(defaultDirection.y), Mathf.Abs(defaultDirection.z)); // gettin the default scale

        if (t) {
            armature.transform.localScale = Vector3.Scale(absDirection,new Vector3(mult,1,1));
        } else {
            armature.transform.localScale = defaultDirection;
        }
    }
    void talk()
    {
        if (!activatedDialogue) {
            
            dialogueBox.transform.parent.gameObject.SetActive(true);
            interactSign.SetActive(false);
            finalSentence = dialogue[rand].sentences;
            currSentence = "";
            StartCoroutine("textScroll");
            //dialogueBox.transform.FindChild("Sentence").GetComponent<Text>().text = dialogue[rand].sentences;
            activatedDialogue = true;
            armature.GetComponent<UnityArmatureComponent>().animation.Play("Idletalking");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("interact") && over) {
            talk();
        }
        if (activatedDialogue) {
            lookAtPlayer(true);
        }
    }
    IEnumerator textScroll()
    {
        if (currSentence.Length < finalSentence.Length) {
           if (tempSwapper % 2 == 0) {
                if (finalSentence[currSentence.Length] != ' ') {
                    //SoundManager.PlaySound("playerTalk2");
                    int swapper = Random.Range(0, 4);
                    if (swapper == 0) {
                        SoundManager.PlaySound("playerTalk1");
                    } else if (swapper == 1) {
                        SoundManager.PlaySound("playerTalk2");
                    } else if (swapper == 2) {
                        SoundManager.PlaySound("playerTalk3");
                    } else if (swapper == 3) {
                        SoundManager.PlaySound("playerTalk4");
                    }
                    textspeed = baseTextSpeed;
                }
            } else {
                textspeed = baseTextSpeed;
            }
            if (finalSentence[currSentence.Length] == '!' || finalSentence[currSentence.Length] == '.' || finalSentence[currSentence.Length] == '?')
                textspeed = baseTextSpeed * 10f;
            if (finalSentence[currSentence.Length] == ',')
                textspeed = baseTextSpeed * 3f;
            tempSwapper++;

            currSentence += finalSentence[currSentence.Length];
            dialogueBox.transform.FindChild("Sentence").GetComponent<Text>().text = currSentence;
            yield return new WaitForSeconds(textspeed);
            StartCoroutine("textScroll");
        }

    }
}
