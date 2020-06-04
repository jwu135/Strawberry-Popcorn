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

    public GameObject Strawberry; // just the prefab
    public GameObject EdgeStrawberry; // just the prefab
    //public GameObject[] StrawberryBodies = new GameObject[8]; // using this so I can access the scripts inside the prefab without changing the prefab itself
    public GameObject[] StrawberryBodies; // using this so I can access the scripts inside the prefab without changing the prefab itself
    public GameObject[] EdgeStrawberryBodies; // using this so I can access the scripts inside the prefab without changing the prefab itself
    public List<SP> Strawberries = new List<SP>(); // each SP object
    public List<EdgeSP> EdgeStrawberries = new List<EdgeSP>(); // each EdgeSP object
    public GameObject SPGroup; // the parent group that they are attached to

    private void Start()
    {
        SPHandler();
        EdgeSPHandler();
    }
    void SPHandler() // spawns interactable and non-interactable SPs
    {
        Strawberry = Resources.Load("Prefabs/Strawberry1") as GameObject;
        //Strawberries[0] = new SP(new Vector3(7.33f, -3.42f, 0f), false, 2, "Idle", -1);
        //Strawberries[1] = new SP(new Vector3(15.77f, -3.15f, 0f), false, 1, "Idle", -1);
        //Strawberries[2] = new SP(new Vector3(27.19f, -5.2f, 0f), true, 3, "Idletalking");
        //Strawberries[3] = new SP(new Vector3(29.7f, -5.2f, 0f), false, 5, "Idletalking");
        //Strawberries[4] = new SP(new Vector3(43.46f, -2.8f, 0f), false, 4, "Idle", -1);
        
        Strawberries.Add(new SP(new Vector3(7.33f, -3.42f, 0f), false, 2, "Idle", -1));
        Strawberries.Add(new SP(new Vector3(15.77f, -3.15f, 0f), false, 1, "Idle", -1));
        Strawberries.Add(new SP(new Vector3(27.19f, -5.2f, 0f), true, 3, "Idletalking",questionMarkLayer:1));
        Strawberries.Add(new SP(new Vector3(29.7f, -5.2f, 0f), false, 5, "Idletalking", questionMarkLayer: 1));
        Strawberries.Add(new SP(new Vector3(43.46f, -2.8f, 0f), false, 4, "Idle", -1));
        StrawberryBodies = new GameObject[Strawberries.Count];
        Debug.Log(Strawberries.Count);
        Debug.Log(Strawberries.Count);
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
        EdgeStrawberries.Add(new EdgeSP(new Vector3(22.53f, -0.08f), false));
        EdgeStrawberries.Add(new EdgeSP(new Vector3(48.56f, 0.06f), false));
        //EdgeStrawberries[2] = new EdgeSP(new Vector3(12.38f, 0f), false);
        EdgeStrawberryBodies = new GameObject[EdgeStrawberries.Count];
        for (int i = 0; i < EdgeStrawberryBodies.Length; i++) {
            EdgeStrawberryBodies[i] = Instantiate(EdgeStrawberry, EdgeStrawberries[i].position, EdgeStrawberry.transform.rotation);
        }
    }
}
