using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;
using System.Linq;

public class SP
{
    public Vector3 position;
    public bool flipped;
    public int dialogueOption;
    public string defaultAction;
    public int orderInLayer;
    public bool ableToTalk;
    public int questionMarkLayer;
    public SP(Vector3 pos, bool flipped, int dialogueOption, string defaultAction, int orderInLayer = 0, bool ableToTalk = true,int questionMarkLayer = 0)
    {
        this.position = pos;
        this.flipped = flipped;
        this.dialogueOption = dialogueOption;
        this.defaultAction = defaultAction;
        this.orderInLayer = orderInLayer;
        this.ableToTalk = ableToTalk;
        this.questionMarkLayer = questionMarkLayer;
    }
    
}
public class EdgeSP
{
    public Vector3 position;
    public bool flipped;

    public EdgeSP(Vector3 pos,bool flipped)
    {
        this.position = pos;
        this.flipped = flipped;
    }
}
public class SPPlacer : MonoBehaviour
{
    public void Awake()
    {
        SPHandler();
        EdgeSPHandler();
        DialogueHandler();
    }


    // Moved dialogue stuff here so it only runs once;
    private Dialogue[] dialogue = new Dialogue[7];

    public string DialogueReturn(int rand)
    {
        return dialogue[rand].sentences;
    }

    private void DialogueHandler()
    {
        for (int i = 0; i < dialogue.Length; i++) {
            dialogue[i] = new Dialogue();
        }


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
        } else if (UpgradeValues.highestPhaseEncountered > UpgradeValues.highestPhaseDiscussed) {
            Debug.Log(UpgradeValues.highestPhaseEncountered);
            UpgradeValues.highestPhaseDiscussed = UpgradeValues.highestPhaseEncountered;
            if (UpgradeValues.highestPhaseDiscussed >= 1 && UpgradeValues.highestPhaseDiscussed < 2) { // if highest phase is 1
                dialogue[0].sentences = "";
                dialogue[1].sentences = "Mother doesn’t eat those with purple eyes, right? I hope my eyes never turn purple.";
                dialogue[2].sentences = "How many purple-eyes went in now?";
                dialogue[3].sentences = "You smell a little bit like Mother now.";
                dialogue[4].sentences = "What’s happening in there?";
                dialogue[5].sentences = "Why aren’t you letting Mother eat you? It’s what we were made for.";
                dialogue[6].sentences = "Can't wait to meet Mother!"; // here
            }
            if (UpgradeValues.highestPhaseDiscussed >= 2 && UpgradeValues.highestPhaseDiscussed < 3) { // if highest phase is 2
                dialogue[0].sentences = "";
                dialogue[1].sentences = "Why does Mother keep doing this? I heard her laughing while fighting other purple-eyes. Does she enjoy it?"; // too much text
                dialogue[2].sentences = "I hope I can get purple-eyes so that I can fight Mother. It seems fun.";
                dialogue[3].sentences = "Why doesn’t she want to eat us anymore?";
                dialogue[4].sentences = "Mother keeps getting restless. She doesn’t even eat anymore."; // here
                dialogue[5].sentences = "I asked Mother to change the sky color to pink. She said I would have to wait my turn.";
                dialogue[6].sentences = "Can't wait to meet Mother!";
            }
            if (UpgradeValues.highestPhaseDiscussed >= 3 && UpgradeValues.highestPhaseDiscussed < 4) { // if highest phase is 3
                dialogue[0].sentences = "";
                dialogue[1].sentences = "Hello Moth-. Wait. You’re not Mother.";
                dialogue[2].sentences = "I feel that Mother is getting weak inside… Either that or more and more Strawberry Popcorns are getting stronger for some reason."; // too much text
                dialogue[3].sentences = "I wonder if Mother’s Ok.";
                dialogue[4].sentences = "Can Mother even die? What’s my purpose if she does?";
                dialogue[5].sentences = "There’s something about you that feels… nostalgic?"; // here
                dialogue[6].sentences = "Can't wait to meet Mother!";

            }
        } else {
            dialogue[0].sentences = "";
            dialogue[1].sentences = "Wow, your eyes’ a different color! That’s new.";
            dialogue[2].sentences = "Hello There!";
            dialogue[3].sentences = "Sorry to say this, but you smell kinda weird.";
            dialogue[4].sentences = "Mother’s right this way, if you wanna meet up with her.";
            dialogue[5].sentences = "Isn’t it awesome how Mother gave us a purpose?";
            dialogue[6].sentences = "I like your eyes! That's new.";
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


    // Strawberry Placement Stuff

    public GameObject Strawberry; // just the prefab
    public GameObject EdgeStrawberry; // just the prefab
    //public GameObject[] StrawberryBodies = new GameObject[8]; // using this so I can access the scripts inside the prefab without changing the prefab itself
    public GameObject[] StrawberryBodies; // using this so I can access the scripts inside the prefab without changing the prefab itself
    public GameObject[] EdgeStrawberryBodies; // using this so I can access the scripts inside the prefab without changing the prefab itself
    public List<SP> Strawberries = new List<SP>(); // each SP object
    public List<EdgeSP> EdgeStrawberries = new List<EdgeSP>(); // each EdgeSP object
    public GameObject SPGroup; // the parent group that they are attached to

    
    void SPHandler() // spawns interactable and non-interactable SPs
    {
        Strawberry = Resources.Load("Prefabs/Strawberry1") as GameObject;
   
        // set 1
        Strawberries.Add(new SP(new Vector3(7.33f, -3.42f, 0f), false, 2, "Idle", -1));
        Strawberries.Add(new SP(new Vector3(15.77f, -3.15f, 0f), false, 1, "Idle", -1));
        Strawberries.Add(new SP(new Vector3(27.19f, -5.2f, 0f), true, 3, "Idletalking",questionMarkLayer:1));
        Strawberries.Add(new SP(new Vector3(29.7f, -5.2f, 0f), false, 5, "Idletalking", questionMarkLayer: 1));
        Strawberries.Add(new SP(new Vector3(43.46f, -2.8f, 0f), false, 4, "Idle", -1));
        
        
        
        
        StrawberryBodies = new GameObject[Strawberries.Count];

        for (int i = 0; i < StrawberryBodies.Length; i++) {
            StrawberryBodies[i] = Instantiate(Strawberry, Strawberries[i].position, Strawberry.transform.rotation, SPGroup.transform);
            StrawberryBodies[i].transform.localPosition = Strawberries[i].position;
            DialogueParallax Dialogue = StrawberryBodies[i].GetComponent<DialogueParallax>();
            Dialogue.rand = Strawberries[i].dialogueOption;
            Dialogue.defaultAction = Strawberries[i].defaultAction;
            GameObject armature = StrawberryBodies[i].transform.Find("Armature").gameObject;
            if (Strawberries[i].flipped) {
                armature.transform.localScale = Vector3.Scale(StrawberryBodies[i].transform.Find("Armature").localScale, new Vector3(-1, 1, 1));
            }
            armature.GetComponent<UnityArmatureComponent>().animation.Play(Strawberries[i].defaultAction);
            armature.GetComponent<UnityArmatureComponent>().sortingOrder = Strawberries[i].orderInLayer;
            StrawberryBodies[i].transform.Find("QuestionBubble").GetComponent<SpriteRenderer>().sortingOrder = Strawberries[i].questionMarkLayer;
            if (Strawberries[i].ableToTalk == false) {
                StrawberryBodies[i].GetComponent<DialogueParallax>().enabled = false;
                StrawberryBodies[i].transform.Find("QuestionBubble").gameObject.SetActive(false);
                StrawberryBodies[i].GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
    void EdgeSPHandler() // spawns non-interactable edge SPs
    {
        EdgeStrawberry = Resources.Load("Prefabs/edgeSP") as GameObject;

        // set 1
        EdgeStrawberries.Add(new EdgeSP(new Vector3(22.53f, -0.08f), false));
        EdgeStrawberries.Add(new EdgeSP(new Vector3(48.56f, 0.06f), false));

        // set 2
        //EdgeStrawberries.Add(new EdgeSP(new Vector3(22.53f, -0.08f), false));
        //EdgeStrawberries.Add(new EdgeSP(new Vector3(12.38f, 0f), false));
        
        EdgeStrawberryBodies = new GameObject[EdgeStrawberries.Count];

        for (int i = 0; i < EdgeStrawberryBodies.Length; i++) {
            EdgeStrawberryBodies[i] = Instantiate(EdgeStrawberry, EdgeStrawberries[i].position, EdgeStrawberry.transform.rotation);
        }
    }
}
