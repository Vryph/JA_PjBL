using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Game_System : MonoBehaviour
{
    [SerializeField] private int score = 0;
    [SerializeField] private Musica musica = null;
    [SerializeField] private Note note_in = null;

    void Start()
    {
        musica = Game_Music.Get_Musica();
    }

    void Update()
    {
        
    }
}

public static class Game_Music
{
    private static Musica musica = null;
    public static void Set_Musica(Musica musica) { Game_Music.musica = musica; }
    public static Musica Get_Musica() {  return musica; }
}