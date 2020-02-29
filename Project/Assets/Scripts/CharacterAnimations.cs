using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class CharacterAnimations : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        // from https://www.youtube.com/watch?reload=9&v=NoDR7iCnExw
        UnityFactory.factory.LoadDragonBonesData("CharacterAnimations/firstProg_ske"); // DragonBones file path (without suffix)
        UnityFactory.factory.LoadTextureAtlasData("CharacterAnimations/firstProg_tex"); //Texture atlas file path (without suffix) 

        // Create armature.
        
        var armatureComponent = UnityFactory.factory.BuildArmatureComponent("Armature");
        // Input armature name

        // Play animation.
        armatureComponent.animation.Play("animation0");

        // Change armatureposition.
        armatureComponent.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
    }
}
