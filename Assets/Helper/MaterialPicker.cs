
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**************************************************
 * Cycles through the different materials available 
 * and provides them via GetMaterial() or GetNextMaterial().
 * 
 * Component for the player
 * 
 * Gleb
 * 05.18.2025
 **************************************************/

public class MaterialPicker : MonoBehaviour
{
    [SerializeField] private Material[] materials;
    // private static int materialIndex;

    public Material GetMaterial(int desiredIndex)
    {
        if (desiredIndex <= materials.Length)
        {
            return materials[desiredIndex];
        }

        else
        {
            Debug.LogError("color index outside bounds. please use a inside the length of the materials list of the MaterialPicker class.");
            return null;
        }
    }

   /** ??????????????????
    public Material GetNextMaterial()
    {
        int returnIndex = materialIndex;
        materialIndex = (materialIndex + 1) % materials.Length;
        return materials[returnIndex];
    }
   */
}
