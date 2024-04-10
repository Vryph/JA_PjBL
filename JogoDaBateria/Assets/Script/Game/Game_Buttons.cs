using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Game_Buttons : MonoBehaviour
{
    [SerializeField] private Note.music_note note;
    void Start()
    {

    }


    void Update()
    {

    }

    public void OnMouseDown()
    {
        Debug.Log("Signal Bateria = "+note.HumanName());
        Sinal.Note_Sinal = note;

    }
}

public static class Sinal
{
    public static Note.music_note Note_Sinal;

}
