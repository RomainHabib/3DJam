using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] public List<GameObject> playerInventory;
    [SerializeField] public GameObject playerHand;

    public bool pickupCooldown;

    [SerializeField] private GameObject img1;
    [SerializeField] private GameObject img2;


    // Start is called before the first frame update
    void Start()
    {
        pickupCooldown = false;
    }

    // Update is called once per frame
    void Update()
    {
        RefreshInventory();
    }

    void RefreshInventory()
    {
        if(playerInventory.Count == 1)
        {
            playerInventory[0].transform.position = playerHand.transform.position;
            playerInventory[0].transform.parent = playerHand.transform;
            playerInventory[0].transform.rotation = Quaternion.identity;
        }

        if(playerInventory.Count == 1)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                playerInventory[0].transform.position = playerHand.transform.position;
                playerInventory[0].transform.parent = playerInventory[0].transform.parent;

                RaycastHit hit = new RaycastHit();
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out hit))
                {
                    playerInventory[0].transform.parent = GameObject.FindGameObjectWithTag("PropContainer").transform;
                   // playerInventory[0].transform.position = new Vector3(transform.position.x + 0.5f, 2.0f, transform.position.z + 0.5f);
                    playerInventory[0].transform.position = new Vector3(transform.position.x, 2, transform.forward.z + 1);
                }

                playerInventory.Remove(playerInventory[0]);
            }
        }
    }
    
   public IEnumerator PickupCooldown()
    {
        pickupCooldown = true;
        yield return new WaitForSeconds(1.0f);
        pickupCooldown = false;
    }
}
