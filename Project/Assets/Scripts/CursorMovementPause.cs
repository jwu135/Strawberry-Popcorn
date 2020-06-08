//Author: Ethan Rafael 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// duplicated from CursorMovement to prevent bugs and get functionality for this specific case
public class CursorMovementPause: MonoBehaviour
{
    public float moveSpeed; //the maximum movement speed
    public float acceleration; //the multiplier for speeding up
    public float deceleration; //the coefficient of drag

    Rigidbody2D cursorBody;

    public bool paused = false;
    public float deadzone; //joystick deadzone
    Vector2 stickInput;
    Vector2 mouseDelta;
    bool controlFrozen;
    bool usingController = true;

    private Camera m_camera;

    PhysicsScene2D scene1Physics;
    Scene scene1;
    Vector2 velocityVector;

    //start is called upon the creation of any instance this script is attached to.
    private void Start()
    {
        Cursor.visible = false;
        controlFrozen = false;
        cursorBody = GetComponent<Rigidbody2D>();
        velocityVector = new Vector2(0, 0);
        m_camera = Camera.main;
        cursorBody.isKinematic = false;
        CreateSceneParameters csp = new CreateSceneParameters(LocalPhysicsMode.Physics2D);
        if (!SceneManager.GetSceneByName("Cursor").IsValid())
            scene1 = SceneManager.CreateScene("Cursor", csp);
        else
            scene1 = SceneManager.GetSceneByName("Cursor");
        scene1Physics = scene1.GetPhysicsScene2D();

        SceneManager.MoveGameObjectToScene(gameObject,scene1);

        Physics2D.autoSimulation = false;
        
        
        
    }

    // Fixed update is called 50 times per second, regardless of framerate (this can be changed in the project settings)
    private void Update()
    {
        if(!paused)
            doMovement();
    }

    private void OnDestroy()
    {
        Physics2D.autoSimulation = true;
        //_ = SceneManager.UnloadSceneAsync(scene1.buildIndex);
    }

    void doMovement()
    {
        if(scene1Physics!=null)
            scene1Physics.Simulate(Time.fixedDeltaTime);

        mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        stickInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); //gets the stick input and puts it in a vector
        if (stickInput.magnitude < deadzone)
        {
            stickInput = Vector2.zero;
        }

        if (controlFrozen == false) //as long as the player isn't rolling
        {
            velocityVector = Vector2.ClampMagnitude(cursorBody.velocity + (stickInput * acceleration), moveSpeed);
        }

        if (mouseDelta.magnitude > 0.02)
        {
            usingController = false;
        }
        else if (stickInput.magnitude > deadzone && usingController == false)
        {
            usingController = true;
        }

        if(usingController == true)
        {
            if (stickInput.magnitude == Vector2.zero.magnitude && controlFrozen == false)//if player is not pressing anything horizontal and the player isn't rolling

            {
                if (velocityVector.x > 0)
                {
                    if (velocityVector.x - deceleration < 0)
                    {
                        velocityVector.x = 0;
                    }
                    else
                    {
                        velocityVector.x -= deceleration;
                    }
                }
                if (velocityVector.x < 0)
                {
                    if (velocityVector.x + deceleration > 0)
                    {
                        velocityVector.x = 0;
                    }
                    else
                    {
                        velocityVector.x += deceleration;
                    }
                }

                if (velocityVector.y > 0)
                {
                    if (velocityVector.y - deceleration < 0)
                    {
                        velocityVector.y = 0;
                    }
                    else
                    {
                        velocityVector.y -= deceleration;
                    }
                }
                if (velocityVector.y < 0)
                {
                    if (velocityVector.y + deceleration > 0)
                    {
                        velocityVector.y = 0;
                    }
                    else
                    {
                        velocityVector.y += deceleration;
                    }
                }
            }
            cursorBody.velocity = velocityVector;
        }
        if (usingController == false)
        {
            Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dir2 = new Vector2(dir.x, dir.y);
            cursorBody.position = dir2;
        }
    }
}

