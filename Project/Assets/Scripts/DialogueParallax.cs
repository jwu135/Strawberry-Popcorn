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
    public GameObject QuestionBubble;
    private bool spokenTo = false;
    private Dialogue[] dialogue = new Dialogue[7];
    private bool over = false;
    private bool activatedDialogue = false;
    private Vector3 defaultDirection;
    public string defaultAction;
    private float textspeed = 0.025f;
    private float baseTextSpeed;
    private int tempSwapper;
    private string finalSentence;
    private string currSentence;
    [HideInInspector]
    public int rand;
    
    // Start is called before the first frame update

    

    void Start()
    {
        //GameObject.Find("EventSystem").GetComponent<SPPlacer>().Activate();
        dialogueBox.transform.Find("Sentence").GetComponent<Text>().font = Resources.Load("Font/Nitw") as Font;
        baseTextSpeed = textspeed;
        defaultDirection = armature.transform.localScale; // default is to the left
        defaultAction = armature.GetComponent<UnityArmatureComponent>().animation.lastAnimationName;
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player") {
            //interactSign.SetActive(true);
            interactSign = GameObject.FindGameObjectWithTag("InteractSign");
            interactSign.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            //if(spokenTo)
            //  QuestionBubble.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            over = true;
        }
    }


    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player") {
            //interactSign.SetActive(false);
            interactSign.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);

            over = false;
            if (activatedDialogue) {
                dialogueBox.transform.parent.gameObject.SetActive(false);
                //dialogueBox.transform.parent.gameObject.SetActive(false);
                activatedDialogue = false;
                armature.GetComponent<UnityArmatureComponent>().animation.Play(defaultAction);
                lookAtPlayer(false);
                StopCoroutine("textScroll");
            }
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
            armature.transform.localScale = Vector3.Scale(absDirection, new Vector3(mult, 1, 1));
        } else {
            armature.transform.localScale = defaultDirection;
        }
    }
    void talk()
    {
        if (!activatedDialogue) {
            spokenTo = true;
            QuestionBubble.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            dialogueBox.transform.parent.gameObject.SetActive(true);
            interactSign.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            finalSentence = GameObject.Find("EventSystem").GetComponent<SPPlacer>().DialogueReturn(rand);//dialogue[rand].sentences;
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
            dialogueBox.transform.Find("Sentence").GetComponent<Text>().text = currSentence;
            yield return new WaitForSeconds(textspeed);
            StartCoroutine("textScroll");
        }

    }
}
