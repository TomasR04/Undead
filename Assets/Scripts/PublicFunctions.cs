using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicFunctions : MonoBehaviour
{
    public static int gameSpeed = 40;

    public static string GetOriginalName(string inputName)
    {
        //Debug.Log("a");
        List<char> objectName = new List<char>();
        char[] a = inputName.ToCharArray();
        foreach (var item in a)
        {
            objectName.Add(item);
        }
        //Debug.Log("b");
        bool removeing = false;
        List<int> charsToRemove = new List<int>();
        int b = 0;
        foreach (var item in objectName)
        {
            if (removeing)
            {
                charsToRemove.Add(b);
            }
            else
            {
                if (item == '(')
                {
                    removeing = true;
                    if (objectName[b - 1] == ' ')
                    {
                        charsToRemove.Add(b - 1);
                    }

                    charsToRemove.Add(b);
                }
            }
            b++;

        }
        //Debug.Log(charsToRemove.Count);
        /*foreach (var item in charsToRemove)
        {
            Debug.Log(item);
        }*/
        //Debug.Log("--------------");
        //int cislo = 0;
        /*foreach (var item in objectName)
        {
            Debug.Log(item + " " + cislo);
            cislo++;
        }*/
        //Debug.Log("c");
        int odecti = 0;
        foreach (var item in charsToRemove)
        {

            objectName.RemoveAt(item - odecti);
            odecti++;
        }
        //Debug.Log("d");
        char[] c = objectName.ToArray();
        /*for (int i = 0; i < c.Length; i++)
        {
            Debug.Log(c[i]);
        }*/
        //Debug.Log("----------");
        string finalName = new string(c);

        return finalName;
    }
}
