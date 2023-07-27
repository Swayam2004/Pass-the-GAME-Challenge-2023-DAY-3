using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    /**
     * Item IDs:
     * 0 = air
     * 1 = hoe
     * 2 = watering can
     * 3 = corn seed
     **/
    public short ID, Amount;

    public Item(short i, short a)
    {
        ID = i;
        Amount = a;

        //If the amount is 0, the item automatically turns into air.
        if(Amount == 0)
        {
            ID = 0;
        }
    }
}
