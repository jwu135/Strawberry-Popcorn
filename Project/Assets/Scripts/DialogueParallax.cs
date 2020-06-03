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
    private string defaultAction;
    private float textspeed = 0.025f;
    private float baseTextSpeed;
    private int tempSwapper;
    private string finalSentence;
    private string currSentence;
    public int rand;
    public SP[] Strawberries = new SP[8];
    // Start is called before the first frame update

    public class SP
    {
        Vector3 position;
        bool flipped;
        int dialogueOption;

        public SP(Vector3 pos, bool flipped, int dialogueOption)
        {
            this.position = pos;
            this.flipped = flipped;
            this.dialogueOption = dialogueOption;
        }
    }
    void StrawberryPlacer()
    {

    }
    private void Awake()
    {

    }

    void Start()
    {
        baseTextSpeed = textspeed;
        defaultDirection = armature.transform.localScale; // default is to the left
        defaultAction = armature.GetComponent<UnityArmatureComponent>().animation.lastAnimationName;
        for (int i = 0; i < dialogue.Length; i++) {
            dialogue[i] = new Dialogue();
        }

        dialogue[0].sentences = "";
        dialogue[1].sentences = "Wow, your eyes’ a different color! That’s new.";
        dialogue[2].sentences = "Hello There!";
        dialogue[3].sentences = "Sorry to say this, but you smell kinda weird.";
        dialogue[4].sentences = "Mother’s right this way, if you wanna meet up with her.";
        dialogue[5].sentences = "Isn’t it awesome how Mother gave us a purpose?";
        dialogue[6].sentences = "I like your eyes! That's new.";
        //rand = Random.Range(0, dialogue.Length);

        // after first death
        if (UpgradeValues.deathCounter == 1) {
            dialogue[0].sentences = "";
            dialogue[1].sentences = "Hey look, your eyes’ also have a different color! Super weird!";
            dialogue[2].sentences = "Mother seemed pretty upset with the last one that got in.";
            dialogue[3].sentences = "I wonder what’s happening.";
            dialogue[4].sentences = "That’s weird. Mother didn’t eat the last one! She never does that...";
            dialogue[5].sentences = "I’ve been alive for two weeks now and Mother still hasn’t called upon me to be eaten."; // here
            dialogue[6].sentences = "Can't wait to meet Mother!";
        }
        if (UpgradeValues.deathCounter == 2) {
            dialogue[0].sentences = "";
            dialogue[1].sentences = "Mother doesn’t eat those with purple eyes, right? I hope my eyes never turn purple.";
            dialogue[2].sentences = "How many purple-eyes went in now?";
            dialogue[3].sentences = "You smell a little bit like Mother now.";
            dialogue[4].sentences = "What’s happening in there?";
            dialogue[5].sentences = "Why aren’t you letting Mother eat you? It’s what we were made for.";
            dialogue[6].sentences = "Can't wait to meet Mother!"; // here
        }
        if (UpgradeValues.deathCounter == 3) {
            dialogue[0].sentences = "";
            dialogue[1].sentences = "Why does Mother keep doing this? I heard her laughing while fighting other purple-eyes. Does she enjoy it?"; // too much text
            dialogue[2].sentences = "I hope I can get purple-eyes so that I can fight Mother. It seems fun.";
            dialogue[3].sentences = "Why doesn’t she want to eat us anymore?";
            dialogue[4].sentences = "Mother keeps getting restless. She doesn’t even eat anymore."; // here
            dialogue[5].sentences = "I asked Mother to change the sky color to pink. She said I would have to wait my turn.";
            dialogue[6].sentences = "Can't wait to meet Mother!";
        }
        if (UpgradeValues.deathCounter == 4) {
            dialogue[0].sentences = "";
            dialogue[1].sentences = "Hello Moth-. Wait. You’re not Mother.";
            dialogue[2].sentences = "I feel that Mother is getting weak inside… Either that or more and more Strawberry Popcorns are getting stronger for some reason."; // too much text
            dialogue[3].sentences = "I wonder if Mother’s Ok.";
            dialogue[4].sentences = "Can Mother even die? What’s my purpose if she does?";
            dialogue[5].sentences = "There’s something about you that feels… nostalgic?"; // here
            dialogue[6].sentences = "Can't wait to meet Mother!";

        }
        if (UpgradeValues.deathCounter == 20) { //random pool
            dialogue[0].sentences = "There’s something about you that feels… nostalgic?";
            dialogue[1].sentences = "Hello Moth-. Wait. You’re not Mother.";
            dialogue[2].sentences = "I feel that Mother is getting weak inside… Either that or more and more Strawberry Popcorns are getting stronger for some reason.";
            dialogue[3].sentences = "I wonder if Mother’s Ok.";
            dialogue[4].sentences = "Can Mother even die? What’s my purpose if she does?"; // here
            dialogue[5].sentences = "Mother’s right this way, if you wanna meet up with her.";
            dialogue[6].sentences = "Can't wait to meet Mother!";
        }
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
            dialogueBox.transform.Find("Sentence").GetComponent<Text>().text = currSentence;
            yield return new WaitForSeconds(textspeed);
            StartCoroutine("textScroll");
        }

    }
}
