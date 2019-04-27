 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_obj : MonoBehaviour {

    public float forceAmount = 500;

    Rigidbody selectedRigidbody;
    Camera targetCamera;
    Vector3 originalScreenTargetPosition;
    Vector3 originalRigidbodyPos;
    float selectionDistance;

    SkillCheck pickupSkill;

    Dunga character;

    // Start is called before the first frame update
    void Start() {
        targetCamera = GameObject.FindObjectOfType<Camera>();
        character = GameObject.Find("Character").GetComponent<Dunga>();

        pickupSkill = GameObject.Find("Pickup Skill").GetComponent<SkillCheck>();
    }

    void Update() { 

        if (pickupSkill.skillActive == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Check if we are hovering over Rigidbody, if so, select it
                selectedRigidbody = GetRigidbodyFromMouseClick();
            }

            if (Input.GetMouseButtonUp(0) && selectedRigidbody)
            {
                //Release selected Rigidbody if there any
                if (selectedRigidbody.gameObject.GetComponent<Rock_rock>() != null)
                {
                    selectedRigidbody.gameObject.GetComponent<Rock_rock>().isHeld = false;
                }

                selectedRigidbody = null;

            }
        }
        
    }

    void FixedUpdate() {
        
        
            if (selectedRigidbody)
            {
                Vector3 mousePositionOffset = targetCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, selectionDistance)) - originalScreenTargetPosition;
                selectedRigidbody.velocity = (originalRigidbodyPos + mousePositionOffset - selectedRigidbody.transform.position) * forceAmount * Time.deltaTime;
            }
        
    }

    Rigidbody GetRigidbodyFromMouseClick()
    {
        //Ray cast check
        RaycastHit hitInfo = new RaycastHit();
        Ray ray = targetCamera.ScreenPointToRay(Input.mousePosition);
        bool hit = Physics.Raycast(ray, out hitInfo);

        
        
            if (hit && character.combatActive == true)
            {
                if (hitInfo.collider.gameObject.GetComponent<Rigidbody>())
                {
                    selectionDistance = Vector3.Distance(ray.origin, hitInfo.point);
                    originalScreenTargetPosition = targetCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, selectionDistance));
                    originalRigidbodyPos = hitInfo.collider.transform.position;

                    // originalRigidbodyPos.x = 1;
                    // originalRigidbodyPos.y = 1;
                    originalRigidbodyPos.z = 0;


                    if (hitInfo.collider.gameObject.GetComponent<Rock_rock>() != null)
                    {
                        hitInfo.collider.gameObject.GetComponent<Rock_rock>().isHeld = true;
                    }

                    return hitInfo.collider.gameObject.GetComponent<Rigidbody>();
                }
            }
        

        return null;
    }
}
