using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] public List<GameObject> playerInventory;
    [SerializeField] public GameObject playerHand;

    public bool pickupCooldown;


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
