using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;
using System;


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
public class EdgeSP : ICloneable
{
    public Vector3 position;
    public bool flipped;

    public EdgeSP(Vector3 pos,bool flipped)
    {
        this.position = pos;
        this.flipped = flipped;
    }
    public object Clone() // don't really need this anymore, but gonna leave it so I remember it for later
    {
        return new EdgeSP(this.position, this.flipped);
    }
}
public class SPPlacer : MonoBehaviour
{
    public void Awake()
    {
        DialogueHandler();
        SPHandler();
        EdgeSPHandler();
    }


    // Moved dialogue stuff here so it only runs once;
    private Dialogue[] dialogue = new Dialogue[7];
    bool specialScene = false;
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
        specialScene = false;
        // after first death
        if (UpgradeValues.deathCounter == 0) {
            specialScene = true;
            dialogue[0].sentences = "";
            dialogue[1].sentences = "Wow, your eyes’ a different color! That’s new.";
            dialogue[2].sentences = "Hello There!";
            //dialogue[2].sentences = "I asked Mother to teach me how to read. She said only she’s supposed to read and write.";
            dialogue[3].sentences = "Sorry to say this, but you smell kinda weird.";
            dialogue[4].sentences = "Mother’s right this way, if you wanna meet up with her.";
            dialogue[5].sentences = "Isn’t it awesome how Mother gave us a purpose?";
            dialogue[6].sentences = "I like your eyes! That's new.";
        } else if (UpgradeValues.deathCounter == 1) {
            specialScene = true;
            dialogue[0].sentences = "";
            dialogue[1].sentences = "Hey look, your eyes’ also have a different color! Super weird!";
            dialogue[2].sentences = "Mother seemed pretty upset with the last one that got in.";
            dialogue[3].sentences = "I wonder what’s happening.";
            dialogue[4].sentences = "That’s weird. Mother didn’t eat the last one! She never does that...";
            dialogue[5].sentences = "I’ve been alive for two weeks now and Mother still hasn’t called upon me to be eaten."; // here
            dialogue[6].sentences = "Can't wait to meet Mother!";
        } else if (UpgradeValues.highestPhaseEncountered > UpgradeValues.highestPhaseDiscussed) { // for the remaining special cases
            specialScene = true;
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
            List<string> sentences = new List<string>();
            sentences.Add("I had tea the other day. I like it");
            sentences.Add("I asked Mother to change the sky color to pink. She said I would have to wait my turn.");
            sentences.Add("I’ve been alive for two weeks now and Mother still hasn’t called upon me to be eaten.");
            sentences.Add("Mother loves all of us. She dotes on us so much. She said we taste better when we’re filled with love.");
            sentences.Add("Noyce Day, innit? Us SPs gutta stand oot, roight? Yoo gut the purple eyes, I made a new way oof speekin’."); // here
            sentences.Add("I wanted to build a house for me and my favorite. Pity Mother said we’re not allowed to create anything.");
            sentences.Add("Ever thought of running away with your favorite? Mine got eaten the other day. I still miss her sometimes.");
            sentences.Add("I think that the longest Strawberry Popcorn to not be eaten was a full 542 days. I hope that doesn’t happen to me.");
            sentences.Add("I heard that long ago, Mother made daughters that were made of different ‘fruits’. I wonder what that word means.");
            sentences.Add("I asked Mother to teach me how to read. She said only she’s supposed to read and write.");
            sentences.Add("Are you having fun out here? Try relaxing for a bit.");
            sentences.Add("I’m gonna ask Mother to eat me and my favorite at the same time. The thought of any of us two living without the other is too much to bear.");
            sentences.Add(":)");
            sentences.Add("Isn’t it funny to think that there’s more Strawberry Popcorns living at the other branches in the horizon? How many of us are there?");
            for(int i = 0; i < dialogue.Length; i++) {
                int index = UnityEngine.Random.Range(0, sentences.Count);
                dialogue[i].sentences = sentences[index];
                sentences.RemoveAt(index);
            }
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


        /* to-do:
         * Add several sets of strawberry groups to use for special scene, maybe one or two
         * For non-special, create about 5 random set-ups. 
         * These, combo'd with the EdgeSP's should make it diverse-ish enough hopefully
         * Also, if it is not a special scene, make only a few have dialogue options
         * Possibly look into expanding the Collider when the SP is talking, that way the player can walk further away and still hear them
         */
        specialScene = false;
        if (specialScene) {
            Strawberries.Add(new SP(new Vector3(7.33f, -3.42f, 0f), false, 2, "Idle", -1));
            Strawberries.Add(new SP(new Vector3(15.77f, -3.15f, 0f), false, 1, "Idle", -1));
            Strawberries.Add(new SP(new Vector3(27.19f, -5.2f, 0f), true, 3, "Idletalking", questionMarkLayer: 1));
            Strawberries.Add(new SP(new Vector3(29.7f, -5.2f, 0f), false, 5, "Idletalking", questionMarkLayer: 1));
            Strawberries.Add(new SP(new Vector3(43.46f, -2.8f, 0f), false, 4, "Idle", -1));
        } else {
            int set = UnityEngine.Random.Range(0, 1); // this is a thing for tomorrow


            /*
             * 
            else if (set == 1) {
                Strawberries.Add(new SP(new Vector3(16.19f, -3.42f, 0f), true, 3, "Idletalking", questionMarkLayer: 1));
                Strawberries.Add(new SP(new Vector3(18.7f, -3.42f, 0f), false, 5, "Idletalking", questionMarkLayer: 1));
            }
             * 
             */



            if (set == 0) { 
                Strawberries.Add(new SP(new Vector3(4.52f, -3.42f, 0f), true, 2, "Idle", -1));
                Strawberries.Add(new SP(new Vector3(16.66f, -5.2f, 0f), true, 1, "Idle", 1, questionMarkLayer: 1));
                Strawberries.Add(new SP(new Vector3(22.54f, -2.7f, 0f), false, 3, "Idle", -1));
                Strawberries.Add(new SP(new Vector3(39.65f, -5.2f, 0f), false, 3, "Idle", 1, questionMarkLayer:1));
            } else{
                Strawberries.Add(new SP(new Vector3(7.33f, -3.42f, 0f), false, 2, "Idle", -1));
                Strawberries.Add(new SP(new Vector3(15.77f, -3.15f, 0f), false, 1, "Idle", -1));
                Strawberries.Add(new SP(new Vector3(27.19f, -5.2f, 0f), true, 3, "Idletalking", questionMarkLayer: 1));
                Strawberries.Add(new SP(new Vector3(29.7f, -5.2f, 0f), false, 5, "Idletalking", questionMarkLayer: 1));
                Strawberries.Add(new SP(new Vector3(43.46f, -2.8f, 0f), false, 4, "Idle", -1));
            }
        }
        
        
        
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
            if (!specialScene) {
                float prob = UnityEngine.Random.Range(0f, 1f);
                //Debug.Log(prob);
                if (prob > .3f)
                    Strawberries[i].ableToTalk = false;
            }
            if (Strawberries[i].ableToTalk == false) {
                StrawberryBodies[i].GetComponent<DialogueParallax>().enabled = false;
                StrawberryBodies[i].transform.Find("QuestionBubble").gameObject.SetActive(false);
                StrawberryBodies[i].GetComponent<BoxCollider2D>().enabled = false;
            }
            //StrawberryBodies[i].GetComponent<BoxCollider2D>().size = new Vector2(5.05f, 1.95f);
            StrawberryBodies[i].GetComponent<BoxCollider2D>().size = new Vector2(2.29f, 1.95f); // might do something like increasing the collider size if you're talking to a SP
            
        }
    }
    void EdgeSPHandler() // spawns non-interactable edge SPs
    {
        EdgeStrawberry = Resources.Load("Prefabs/edgeSP") as GameObject;
        
        // All possible positions
        List<EdgeSP> possibleSPs = new List<EdgeSP>();
        possibleSPs.Add(new EdgeSP(new Vector3(3.03f, 0.07f), false));
        possibleSPs.Add(new EdgeSP(new Vector3(12.38f, 0f), false));
        possibleSPs.Add(new EdgeSP(new Vector3(19.22f, 0.19f), false));
        possibleSPs.Add(new EdgeSP(new Vector3(22.53f, -0.08f), false));
        possibleSPs.Add(new EdgeSP(new Vector3(48.56f, 0.06f), false));

        
        // Using this to make some less common than other
        float numSPProb = UnityEngine.Random.Range(0f, 1f);
        int numSPs = 0;
        if (numSPProb < 0.1f) {
            numSPs = 0;
        }else if (numSPProb>=.1f&&numSPProb<.35f) {
            numSPs = 1;
        }else if (numSPProb>=.35f&&numSPProb<.7f) {
            numSPs = 2;
        }else if (numSPProb>=.7f&&numSPProb<1f) {
            numSPs = 3;
        }

        
        for(int i = 0; i < numSPs; i++) {
            EdgeStrawberries.Add(possibleSPs[UnityEngine.Random.Range(0,possibleSPs.Count)]);
            possibleSPs.RemoveAt(i);
        }
        EdgeStrawberryBodies = new GameObject[EdgeStrawberries.Count];

        for (int i = 0; i < EdgeStrawberryBodies.Length; i++) {
            EdgeStrawberryBodies[i] = Instantiate(EdgeStrawberry, EdgeStrawberries[i].position, EdgeStrawberry.transform.rotation);
        }
    }
}
