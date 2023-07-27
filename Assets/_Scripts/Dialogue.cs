using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
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

    public string line;
    public short speaker, eventIndex;
    public Dialogue(string l, short s, short e = 0)
    {
        line = l;
        speaker = s;
        eventIndex = e;
    }
}
