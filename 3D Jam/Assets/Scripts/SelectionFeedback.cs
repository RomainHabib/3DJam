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

    [SerializeField] private float rayDistance;

    [SerializeField] private Text interactText;

    private Transform selectionTransform;

    [SerializeField] GameObject player;

    private void Start()
    {
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
            if (selection.CompareTag(selectableTag))
            {
                Debug.Log("Facing : " + hit.transform.name);

                var selectionRenderer = selection.GetComponent<Renderer>();
                if (selectionRenderer != null)
                {
                    selectionRenderer.material = selectMaterial;
                }

                interactText.text = "Interact : " + hit.transform.name;
                selectionTransform = selection;

                if (Input.GetKeyDown(KeyCode.E) && !player.GetComponent<Inventory>().pickupCooldown)
                {
                    if(player.GetComponent<Inventory>().playerInventory.Count < 1)
                    {
                    StartCoroutine(player.GetComponent<Inventory>().PickupCooldown());
                    Collect(selectionTransform);
                    }
                }

            }
            Debug.DrawLine(ray.origin, hit.point, Color.red);
        }
        else
        {
            interactText.text = "";
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * rayDistance, Color.green);
        }
    }

    void Collect(Transform collectable)
    {
        player.GetComponent<Inventory>().playerInventory.Add(collectable.gameObject);
    }
}

