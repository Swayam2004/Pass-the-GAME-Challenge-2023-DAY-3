using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    /**
     * Item IDs:
     * 0 = air
     * 1 = hoe
     * 2 = watering can
     * 3 = corn seed
     **/
    public short ID, amount;
    public Item(short i, short a)
    {
        ID = i;
        amount = a;

        //If the amount is 0, the item automatically turns into air.
        if(amount == 0)
        {
            ID = 0;
        }
    }
}
