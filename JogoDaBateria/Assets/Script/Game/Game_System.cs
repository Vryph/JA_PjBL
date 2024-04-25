using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Game_System : MonoBehaviour
{
    [SerializeField] private bool deBug = false;
    [SerializeField] private bool music_start = false;
    [SerializeField] private bool music_paused = false;
    [SerializeField] private int score = 0;
    [SerializeField] private int combo = 0;
    [SerializeField] private int this_time = 0;
    [SerializeField] private Times time_in = null;
    [SerializeField] private Game_Configuration configuration;

    private void Awake()
    {
        configuration.game_Time = Time.time;
        configuration.audio_source = GetComponent<AudioSource>();

        if(!deBug)
        {
            configuration.musica = Game_Music.Get_Musica();
        }
    }

    void Start()
    {
        Sinal.Check_Note(Note.music_note.Chimbal);

        configuration.musica.times[0].time = configuration.musica.times[0].time+Time.time;
        float last_time = configuration.musica.times[0].time;

        for (int i = 1; i < configuration.musica.times.Count; i++)
        {
            if (configuration.musica.times[i].type == Times.metronomo_type.time)
            {
                last_time = last_time + configuration.musica.metronomo;
                configuration.musica.times[i].time = last_time;
            }
            else if (configuration.musica.times[i].type == Times.metronomo_type.conter_time)
            {
                configuration.musica.times[i].time = last_time + (configuration.musica.metronomo/2);
            }
        }

        configuration.audio_source.clip = configuration.musica.soundTrack;
        time_in = configuration.musica.times[0];
    }

    void Update()
    {
        configuration.game_Time = Time.time;
        Spawn_Check();
        CheckTime();

        if(configuration.musica.start_music < configuration.game_Time && !music_start) { configuration.audio_source.Play(); ; music_start = true; }

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (music_start && !music_paused)
            {
                configuration.audio_source.Pause(); music_paused = true;
            }
            else
            {
                configuration.audio_source.Play(); music_paused = false;
            }
        }
    }

    public void CheckTime()
    {
        if (!time_in.complet && !time_in.fail && time_in.time - configuration.margem < configuration.game_Time)
        {
            if (time_in.time + configuration.margem < configuration.game_Time)
            {
                time_in.fail = true;
            }
            else
            {
                int correct = 0;

                for (int i = 0; i < time_in.notes.Length; i++)
                {
                    try
                    {
                        if (Sinal.Check_Note(time_in.notes[i].note))
                        {
                            if (correct == 0)
                            {
                                correct++;
                                new WaitForSeconds(0.2f);
                            }
                            else
                            {
                                correct++;
                            }
                        }
                    }
                    catch { Debug.Log("Note não encontrada."); }
                    
                }

                if (correct != 0)
                {
                    if (correct == time_in.notes.Length)
                    {
                        if(time_in.notes[0].note != Note.music_note.Empty)
                        {
                            time_in.complet = true;
                            combo++;

                            if (combo < 20)
                            {
                                score += configuration.get_Points * combo / 2;
                            }
                            else
                            {
                                score += configuration.get_Points * 10;
                            }
                        }
                        else
                        {
                            time_in.complet = true;
                        }
                        
                    }
                    else { time_in.fail = true; }
                }
            }

            if (time_in.complet == true || time_in.fail == true)
            {
                if (time_in.fail == true) { combo = 0; }

                this_time++;

                if (this_time < configuration.musica.times.Count)
                { time_in = configuration.musica.times[this_time]; }
                else
                {
                    new WaitForSeconds(2f);

                    MenuManager.ChangeMenu("JogoLivre");
                }
            }
        }
    }

    public float Spawn_Time()
    {
        GameObject spawn = configuration.spawns[0];

        float distance = spawn.transform.position.y - spawn.GetComponent<Game_Buttons>().getSpawn().transform.position.y;
        float value = distance / configuration.musica.note_velocity;

        return value;
    }

    public void Spawn_Check()
    {
        for (int i = 0; i < configuration.musica.times.Count; i++)
        {
            Musica music = configuration.musica;

            if (!music.times[i].spawned)
            {
                if (music.times[i].time <= configuration.game_Time - Spawn_Time())
                {

                    Times music_time = music.times[i];
                    music_time.spawned = true;

                    for (int o = 0; o < music_time.notes.Length; o++)
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
    }//Verifica se tem que spawnar alguma nota no momento que é checado.
    
    public void Spawn(string spawn)
    {
        for(int i = 0; i < configuration.spawns.Length; i++)
        {
            if (configuration.spawns[i].name == spawn)
            { 
                GameObject spawn_object = configuration.spawns[i].GetComponent<Game_Buttons>().getSpawn();
                GameObject note = Instantiate(configuration.note_object,spawn_object.transform);
                note.GetComponent<SpriteRenderer>().sprite = configuration.spawns[i].GetComponent<Game_Buttons>().Get_Sprite();
                note.GetComponent<Game_Note>().Set_Velocity(configuration.musica.note_velocity);
            }
        }
    }//Spawna as notas prevendo sua posição perfeita.
}

[System.Serializable]
public class Game_Configuration
{
    public float game_Time = 0;
    public float margem = 0;
    public AudioSource audio_source = null;
    public int get_Points = 0;
    public int[] score_Stars = new int[3];
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
    public static bool Chimbal, Caixa, TomUm, TomDois, Surdo, Bumbo, Prato;
    public static bool Score_Sinal = false;

    public static bool Check_Note(Note.music_note note)
    {
        if (note == Note.music_note.Empty) { return true; }

        bool note_check = false;
        bool value = false;

        switch(note)
        {
            case Note.music_note.Chimbal:
                value = Chimbal; Chimbal = false; note_check = true; UnityEngine.Debug.Log("Chimbal"); break;
            case Note.music_note.Caixa:
                value = Caixa; Caixa = false; note_check = true; break;
            case Note.music_note.TomUm:
                value = TomUm; TomUm = false; note_check = true; break;
            case Note.music_note.TomDois:
                value = TomDois; TomDois = false; note_check = true; break;
            case Note.music_note.Bumbo:
                value = Bumbo; Bumbo = false; note_check = true; break;
            case Note.music_note.Surdo:
                value = Surdo; Surdo = false; note_check = true; break;
            case Note.music_note.Prato:
                value = Prato; Prato = false; note_check = true; break;
        }

        if(note_check) { return value; }

        throw new System.Exception();
    }
}