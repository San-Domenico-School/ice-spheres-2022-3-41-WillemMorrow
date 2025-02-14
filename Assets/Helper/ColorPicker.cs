using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**************************************************
 * cycles through the different colors available and returns one with the GetColor() method.
 * 
 * component of the player
 * 
 * Pacifica Morrow
 * 02.14.2025
 * ***********************************************/

public class ColorPicker : MonoBehaviour
{
    private Color[] colors; //array to hold the available colors
    private int colorIndex; //Index for the color array

    public Color GetColor()
    {
        // gets a local int of the colorIndex before colorIndex is changed
        int returnColorIndex = colorIndex;

        // changes the colorIndex to cycle to the next color, if the index is less than the length of color.
        if (colorIndex > colors.Length)
        {
            colorIndex++;
        }

        else
        {
            colorIndex = 0;
        }
        
        // returns the original colorIndex before it was modified, via the local variable
        return colors[returnColorIndex];
    }
}
