using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    //filter out objects while shooting using a layer mask
    public LayerMask movementMask;
    PlayerMotor motor;
    Camera cam;
    public Interactable focus;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        if(EventSystem.current.IsPointerOverGameObject())
            return;
        //check if left mouse button is clicked, 0 is left
        if(Input.GetMouseButtonDown(0)){ 
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 100, movementMask)){
                //move our player to what we hit
                //Debug.Log("We hit" + hit.collider.name + " " + hit.point);
                motor.MoveToPoint(hit.point);
                
                //stop focusing any object
                RemoveFocus();
            }
        }

        if(Input.GetMouseButtonDown(1)){ 
            //we create a ray
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //if the ray hits
            if(Physics.Raycast(ray, out hit, 100)){
                //check if we hit an interactable, if we did set it as our focus
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null){
                    SetFocus(interactable);
                }
            }
        }
    }

    void SetFocus(Interactable newFocus){
        if(newFocus != focus){
            if(focus != null){
                focus.OnDefocus();
            }            
            focus = newFocus;
            motor.FollowTarget(newFocus);
        }
        newFocus.OnFocus(transform);        
    }

    void RemoveFocus(){
        if(focus != null){
            focus.OnDefocus();
        }        
        focus = null;
        motor.StopFollowingTarget();
    }
}
