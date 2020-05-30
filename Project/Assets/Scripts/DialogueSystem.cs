using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public Sprite[] cursors;
    public Sprite[] box = new Sprite[3];
    public GameObject dialogueBox;
    public new Text name;
    public Text sentence;
    public float textspeed;
    public GameObject buttonIcon;
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
    private float baseTextSpeed;
    public bool dialogueGoing = true;
    public bool startTalking = false;
    public void Start()
    {
        baseTextSpeed = textspeed;
        finalSentence = dialogue[index].sentences;
        currSentence = "";
        GameObject.Find("crosshairAttack").GetComponent<SpriteRenderer>().sprite = cursors[1];
        name.text = dialogue[index].name;
        sentence.text = currSentence;
        boxChange();
        //StartCoroutine("textScroll");
        /*if (GlobalVariable.deathCounter > 0) {
            startTalking = true;
            StartCoroutine("textScroll");
        }*/
    }
    void Update()
    {
        if (Time.timeScale != 0) {
            lookAround();
        }
    }
    public void restart()
    {
        GameObject.Find("crosshairAttack").GetComponent<SpriteRenderer>().sprite = cursors[1];
        index++;
        dialogueGoing = true;
        finalSentence = dialogue[index].sentences;
        currSentence = "";
        continuing = true;
        name.text = dialogue[index].name;
        sentence.text = currSentence;
        boxChange();
        StartCoroutine("textScroll");
    }
    public void restart2()
    {
        GameObject.Find("crosshairAttack").GetComponent<SpriteRenderer>().sprite = cursors[1];
        index++;
        dialogueGoing = true;
        finalSentence = dialogue[index].sentences;
        currSentence = "";
        continuing = false;
        continuing2 = true;
        name.text = dialogue[index].name;
        sentence.text = currSentence;
        boxChange();
        StartCoroutine("textScroll");

    }
    private void boxChange()
    {
        //GameObject.Find("crosshairAttack").GetComponent<SpriteRenderer>().sprite = cursors[1];
        if (dialogue[index].name == "Mother:") {
            dialogueBox.GetComponent<Image>().sprite = box[0];
            dialogueBox.GetComponent<RectTransform>().localPosition = new Vector2(0f, 87.65f);
        } else if (dialogue[index].name == "Strawberry Popcorn:") {
            dialogueBox.GetComponent<Image>().sprite = box[1];
            dialogueBox.GetComponent<RectTransform>().localPosition = new Vector2(0f, 2.87f);
        } else {
            dialogueBox.GetComponent<Image>().sprite = box[2];
            //dialogueBox.transform.position = new Vector2(2f, 2f);
        }
    }
    public void lookAround()
    {
        if (startTalking) {
            if (Input.GetButtonDown("interact") && dialogueGoing) {
                index = stops[stopsIndex] - 1; // use this to make dialogue only show first dialogue box of each thing
                if (currSentence.Length < finalSentence.Length) {
                    StopCoroutine("textScroll");
                    sentence.text = currSentence = finalSentence;
                } else {
                    if (stops[stopsIndex] > index + 1 && stopsIndex < 8) {
                        index++;
                        name.text = dialogue[index].name;
                        boxChange();
                        finalSentence = dialogue[index].sentences;
                        currSentence = "";
                        StartCoroutine("textScroll");
                    } else {
                        stopsIndex++;
                        dialogueBox.SetActive(false);
                        dialogueGoing = false;
                        if (continuing) {
                            GetComponent<CutsceneSystem>().StartCoroutine("buffer");
                        } else if (continuing2) {
                            GetComponent<CutsceneSystem>().DialogueDone(true);
                            GameObject.Find("crosshairAttack").GetComponent<SpriteRenderer>().sprite = cursors[0];
                            GameObject.Find("Border").GetComponent<Animator>().SetTrigger("Up");
                            Debug.Log("Went up");
                        } else {
                            GetComponent<CutsceneSystem>().DialogueDone();
                            GameObject.Find("crosshairAttack").GetComponent<SpriteRenderer>().sprite = cursors[0];
                            if (UpgradeValues.deathCounter>0) {
                                if(UpgradeValues.deathCounter==1)
                                    buttonIcon.SetActive(true);
                                GameObject.Find("Border").GetComponent<Animator>().SetTrigger("Up");
                                Debug.Log("Went up");
                            }
                        }
                    }

                }
            }
        }
        
    }

    IEnumerator eatDelay()
    {
        if (UpgradeValues.deathCounter == 0) {
            //SoundManager.PlaySound("crunch");
            yield return new WaitForSeconds(0f);
        }
        StartCoroutine("textScroll");
        startTalking = true;
    }

    private int tempSwapper = 0;
    IEnumerator textScroll()
    {
        if (currSentence.Length < finalSentence.Length) {
            if (dialogue[index].name == "Mother:") {
                if (finalSentence[currSentence.Length] != ' '&&tempSwapper%2==0) {
                    //SoundManager.PlaySound("playerTalk2");
                    int swapper = Random.Range(0, 4);
                    if (swapper == 0) {
                        SoundManager.PlaySound("motherTalk1");
                    } else if (swapper == 1) {
                        SoundManager.PlaySound("motherTalk2");
                    } else if (swapper == 2) {
                        SoundManager.PlaySound("motherTalk3");
                    } else if (swapper == 3) {
                        SoundManager.PlaySound("motherTalk4");
                    }
                    textspeed = baseTextSpeed*1.8f;
                }
            } else if (dialogue[index].name == "Strawberry Popcorn:"&& tempSwapper % 2 == 0) {
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
            if(finalSentence[currSentence.Length] == '!'|| finalSentence[currSentence.Length] == '.' || finalSentence[currSentence.Length] == '?')
                textspeed = baseTextSpeed*10f;
            if(finalSentence[currSentence.Length] == ',')
                textspeed = baseTextSpeed*3f;
            tempSwapper++;

            currSentence += finalSentence[currSentence.Length];
            sentence.text = currSentence;
            yield return new WaitForSeconds(textspeed);
            StartCoroutine("textScroll");
        }

    }
}
