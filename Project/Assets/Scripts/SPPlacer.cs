using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;
public class SP
{
    public Vector3 position;
    public bool flipped;
    public int dialogueOption;
    public string defaultAction;
    public int orderInLayer;
    public SP(Vector3 pos, bool flipped, int dialogueOption,string defaultAction,int orderInLayer = 0)
    {
        this.position = pos;
        this.flipped = flipped;
        this.dialogueOption = dialogueOption;
        this.defaultAction = defaultAction;
        this.orderInLayer = orderInLayer;
    }
    
}
public class SPPlacer : MonoBehaviour
{

    public GameObject Strawberry; // just the prefab
    public GameObject[] StrawberryBodies = new GameObject[8]; // using this so I can access the scripts inside the prefab without changing the prefab itself
    public SP[] Strawberries = new SP[8]; // each SP object
    public GameObject SPGroup; // the parent group that they are attached to
    void StrawberryPlacer()
    {

    }
    private void Start()
    {
        Strawberry = Resources.Load("Prefabs/Strawberry1") as GameObject;
        Strawberries[0] = new SP(new Vector3(7.33f, -3.42f, 0f), false, 2,"Idle",-1);
        Strawberries[1] = new SP(new Vector3(15.77f, -3.15f, 0f), false, 1, "Idle",-1);
        Strawberries[2] = new SP(new Vector3(27.19f, -5.2f, 0f), true, 3,"Idletalking");
        Strawberries[3] = new SP(new Vector3(29.7f, -5.2f, 0f), false, 5,"Idletalking");
        Strawberries[4] = new SP(new Vector3(43.46f, -2.8f, 0f), false, 4,"Idle",-1);
        for (int i = 0; i < 5; i++) {
            StrawberryBodies[i] = Instantiate(Strawberry, Strawberries[i].position, Strawberry.transform.rotation,SPGroup.transform);
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
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
