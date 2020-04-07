using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionFeedback : MonoBehaviour
{
    [Header("Materials Prefs")]
    [SerializeField] private string selectableTag = "Prop";
    [SerializeField] private Material normalMaterial;
    [SerializeField] private Material selectMaterial;
    public Sprite inventoryPreview;

    [SerializeField] private float rayDistance;

    [SerializeField] private Text interactText;
    [SerializeField] private GameObject interactHud;

    private Transform selectionTransform;

    [SerializeField] GameObject player;

    private void Start()
    {
        interactHud.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Selection();
    }

    void Selection()
    {
        if(selectionTransform != null)
        {
            var selectionRenderer = selectionTransform.GetComponent<Renderer>();
            selectionRenderer.material = normalMaterial;
            selectionTransform = null;
        }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, rayDistance))
        {
            var selection = hit.transform;

            if (selection.CompareTag(selectableTag) && selection.gameObject != player.GetComponent<Inventory>().inHand)
            {
                Debug.Log("Facing : " + hit.transform.name);

                var selectionRenderer = selection.GetComponent<Renderer>();
                if (selectionRenderer != null)
                {
                    selectionRenderer.material = selectMaterial;
                }

                interactHud.SetActive(true);
                interactText.text = "Interact : " + hit.transform.name;
                selectionTransform = selection;

                if (Input.GetKeyDown(KeyCode.F) && selection.gameObject != player.GetComponent<Inventory>().inHand)
                {
                    player.GetComponent<Inventory>().PickUp(selection.gameObject);
                }
            }
            Debug.DrawLine(ray.origin, hit.point, Color.red);
        }
        else
        {
            interactText.text = ""; 
            interactHud.SetActive(false);
            transform.GetComponent<Renderer>().material = normalMaterial;
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * rayDistance, Color.green);
        }
    }
}

