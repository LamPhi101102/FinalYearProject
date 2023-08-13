using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    // ***** Definitely Delete *****
    public static SelectionManager Instance { get; set; }
    public bool onTarget;
    public GameObject selectedObject;

    public GameObject Interaction_info_UI;
    Text interaction_text;
    // Start is called before the first frame update
    void Start()
    {
        // ***** Definitely Delete *****
        onTarget = false;
        // Get the text from gameobject
        interaction_text = Interaction_info_UI.GetComponent<Text>();
    }

    // ***** Definitely Delete *****
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    // Update is called once per frame
    void Update()
    {
        // get the position from the main camera
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // the function to display and turn off the text to the main camera
        if (Physics.Raycast(ray, out hit))
        {
            // the selectionTransform will get the transfrom of the position that the main camera hit 
            var selectionTransform = hit.transform;
            // it means call a fruntion from another script and then Getcomponent from this script
            InteractableObject interactable = selectionTransform.GetComponent<InteractableObject>();
            // Check if when there is a component
            if (interactable && interactable.playerInRange)
            {
                // ***** Definitely Delete *****
                onTarget = true;
                // use to select a item that we need without select a lot of item in the sime location
                selectedObject = interactable.gameObject;
                // turn on the Text component and display Text to main screen
                Interaction_info_UI.SetActive(true);
                interaction_text.text = interactable.GetItemName();
            }
            else
            {
                // ***** Definitely Delete *****
                onTarget = false;
                // turn off the Text component (there is a hit without an Interactable script)
                Interaction_info_UI.SetActive(false);
            }
        }
        else
        {
            // ***** Definitely Delete *****
            onTarget = false;
            // if there is no hit at all
            Interaction_info_UI.SetActive(false);
        }
    }
}
