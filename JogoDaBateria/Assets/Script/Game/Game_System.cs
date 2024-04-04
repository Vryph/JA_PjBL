using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Game_System : MonoBehaviour
{

    [SerializeField] private Musica musica = null;
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