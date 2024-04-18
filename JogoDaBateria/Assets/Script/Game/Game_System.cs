using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Game_System : MonoBehaviour
{
    [SerializeField] private bool deBug = false;
    [SerializeField] private int score = 0;
    [SerializeField] private Times time_in = null;

    

    [SerializeField] private Game_Configuration configuration;

    private void Awake()
    {
        configuration.game_Time = Time.time;

        if(!deBug)
        {
            configuration.musica = Game_Music.Get_Musica();
        }
    }

    void Start()
    {

        for(int i = 0; i < configuration.musica.times.Count; i++)
        {
            configuration.musica.times[i].time = configuration.musica.times[i].time+configuration.game_Time;
        }
    }

    void Update()
    {
        configuration.game_Time = Time.time;
        Spawn_Check();

        for(int i = 0;  i < configuration.score_Stars.Length; i++)
        {
            if(configuration.score_Stars[i] <= score)
            {
                if(configuration.musica.stars_music < i++)
                {
                    configuration.musica.stars_music++;
                    MenuManager.Stars_Set(configuration.musica, i++);
                }
            }
        }
    }

    public void Spawn_Check()
    {
        for(int i = 0; i < configuration.musica.times.Count; i++)
        {
            Musica music = configuration.musica;

            if (!music.times[i].spawned) 
            {
                if (music.times[i].time <= configuration.game_Time + configuration.spawn_time)
                {
                    
                    Times music_time = music.times[i];
                    music_time.spawned = true;

                    for(int o = 0; o < music_time.notes.Length; o++)
                    {
                        

                        switch (music_time.notes[o].note.HumanName())
                        {
                            case "Chimbal":
                                Spawn("Chimbal"); break;
                            case "Caixa":
                                Spawn("Caixa"); break;
                            case "Tom Um":
                                Spawn("Tom_01"); UnityEngine.Debug.Log(music_time.notes[o].note.HumanName()); break;
                            case "Tom Dois":
                                Spawn("Tom_02"); UnityEngine.Debug.Log(music_time.notes[o].note.HumanName()); break;
                            case "Surdo":
                                Spawn("Surdo"); break;
                            case "Bumbo":
                                Spawn("Bumbo"); break;
                            case "Prato":
                                Spawn("Prato"); break;
                        }
                    }
                }
            }
        }
    }   
    
    public void Spawn(string spawn)
    {
        for(int i = 0; i < configuration.spawns.Length; i++)
        {
            if (configuration.spawns[i].name == spawn)
            { 
                GameObject spawn_object = configuration.spawns[i].GetComponent<Game_Buttons>().getSpawn();
                Instantiate(configuration.note_object,spawn_object.transform);
            }
        }
    }
}

[System.Serializable]
public class Game_Configuration
{
    public float game_Time = 0;
    public int[] score_Stars = new int[3];
    public int spawn_time = 0;
    public GameObject[] spawns;
    public Musica musica = null;
    public GameObject note_object = null;
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