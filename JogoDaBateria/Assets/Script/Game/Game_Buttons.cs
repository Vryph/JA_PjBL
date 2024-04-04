using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Buttons : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void OnMouseDown(Note.music_note note)
    {
        Sinal.Note_Sinal = Note.music_note.Empty;
    }
}

public static class Sinal
{
    public static Note.music_note Note_Sinal;

}
