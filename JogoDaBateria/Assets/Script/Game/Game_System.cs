using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Game_System : MonoBehaviour
{
    [SerializeField] private int score = 0;
    [SerializeField] private int[] score_Stars = new int[3];
    [SerializeField] private Musica musica = null;
    [SerializeField] private Note note_in = null;

    void Start()
    {
        musica = Game_Music.Get_Musica();
    }

    void Update()
    {
        for(int i = 0;  i < score_Stars.Length; i++)
        {
            if(score_Stars[i] <= score)
            {
                if(musica.stars_music < i++)
                {
                    musica.stars_music++;
                    MenuManager.Stars_Set(musica, i++);
                }
            }
        }
    }
}

public static class Game_Music
{
    private static Musica musica = null;
    public static void Set_Musica(Musica musica) { Game_Music.musica = musica; }
    public static Musica Get_Musica() {  return musica; }
}

public static class Sinal
{
    public static Note.music_note Note_Sinal;
    public static bool Score_Sinal = false;
}