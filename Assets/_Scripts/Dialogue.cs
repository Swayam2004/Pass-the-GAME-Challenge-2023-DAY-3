using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue
{
    /**
     * Speakers:
     * 0 = farmer
     * 1 = villain
     * Events:
     * 0 = nothing
     * 1 = camera zooms in from orthographic size 40 to 5
     * 2 = villain appears
     * 3 = game starts
     * 4 = move camera 6 units to the right
     * 5 = Move camera back to normal position;
     * 6 = Give the player 5 corn seeds.
     **/

    public string Line;
    public short Speaker, EventIndex;

    public Dialogue(string l, short s, short e = 0)
    {
        Line = l;
        Speaker = s;
        EventIndex = e;
    }
}
