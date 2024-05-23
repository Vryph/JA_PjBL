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
    [SerializeField] private UnityEngine.UI.Text text_score;

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
        configuration.musica.start_music = configuration.musica.start_music + Time.time;
        configuration.musica.times[0].time = configuration.musica.times[0].time+Time.time;

        for (int o = 0; o < configuration.musica.times[0].notes.Length; o++)
        {
            configuration.musica.times[0].notes[o].time = configuration.musica.times[0];
        }

        float last_time = configuration.musica.times[0].time;
        Times thisTime = default(Times);

        Note.music_note ContinuousNoteName = default(Note.music_note);
        bool Continuous = false;

        for (int i = 1; i < configuration.musica.times.Count; i++)
        {
            thisTime = configuration.musica.times[i];

            if (thisTime.type == Times.metronomo_type.time)
            {
                last_time = last_time + configuration.musica.metronomo; thisTime.time = last_time;
            }
            else if(configuration.musica.times[i].type == Times.metronomo_type.conter_time)
            {
                configuration.musica.times[i].time = last_time + (configuration.musica.metronomo/2);
            }

            for(int o = 0; o < thisTime.notes.Length; o++)
            {
                thisTime.notes[o].time = thisTime;
            }

            ContinuousNote(ref thisTime, ref Continuous, ref ContinuousNoteName, i);
        }

        configuration.audio_source.clip = configuration.musica.soundTrack;
        time_in = configuration.musica.times[0];
    }

    void ContinuousNote(ref Times thisTime, ref bool Continuous, ref Note.music_note ContinuousNoteName, int i)
    {
        Note.music_note thisNote = default(Note.music_note);
        int ContinuousNoteNumber = 0;

        for (int o = 0; o < thisTime.notes.Length; o++)
        {
            thisNote = thisTime.notes[o].note;
            switch (thisTime.notes[o].special)
            {
            case Note.music_special_note.None:
                    if (Continuous & thisNote == ContinuousNoteName)
                    { thisTime.notes[o].special = Note.music_special_note.Ghost; ContinuousNoteNumber+=1; /*Debug.Log(i);*/ }
                    break;
                case Note.music_special_note.Continuous:
                    Continuous = true; ContinuousNoteName = thisNote; ContinuousNoteNumber += 1; break;
            }
        }

        if(Continuous)
        {
            if (ContinuousNoteNumber == 0)
            {
                //Debug.Log("Continuous note OFF in time:" + i);
                Continuous = false; ContinuousNoteName = default(Note.music_note);
            }
        }
        else
        {
            if (ContinuousNoteNumber != 0)
            {
                //Debug.Log("Continuous Note ON in time:" + i);
                Continuous = true; ContinuousNoteName = default(Note.music_note);
            }
        }
    }

    private void LateUpdate()
    {
        Spawn_Check();
        Sinal.Reset();
    }
    void Update()
    {
        configuration.game_Time = Time.time;
        CheckTime();

        if (configuration.musica.start_music < configuration.game_Time && !music_start) { configuration.audio_source.Play(); ; music_start = true; }

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

        if (Input.GetKeyDown(KeyCode.Escape))
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

        text_score.text = score.ToString();
    }

    public bool CheckGhost(ref Times time)
    {
        for(int i = 0; i < time.notes.Length; i++)
        {
            if (time.notes[i].special == Note.music_special_note.Ghost)
            {
                string spawnName = "";

                switch (time_in.notes[0].note)
                {
                    case Note.music_note.Chimbal:
                        spawnName = "Chimbal"; break;
                    case Note.music_note.Caixa:
                        spawnName = "Caixa"; break;
                    case Note.music_note.TomUm:
                        spawnName = "Tom_01"; break;
                    case Note.music_note.TomDois:
                        spawnName = "Tom_02"; break;
                    case Note.music_note.Surdo:
                        spawnName = "Surdo"; break;
                    case Note.music_note.Bumbo:
                        spawnName = "Bumbo"; break;
                    case Note.music_note.Prato:
                        spawnName = "Prato"; break;
                }

                if (!time_in.notes[i].active)
                {
                    time_in.notes[i].active = true;
                    for (int o = 0; o < configuration.spawns.Length; o++)
                    {
                        if (configuration.spawns[o].name == spawnName)
                        {
                            configuration.spawns[o].GetComponent<Game_Buttons>().PlayAudio();
                        }
                    }
                    return true;
                }
            }
        }
        return false;
    }//Verifica se tem ghosts em um tempo e toca o audio correspondente.

    public void CheckTime()
    {
        if (!time_in.complet && !time_in.fail && time_in.time - configuration.margem < configuration.game_Time)
        {
            if (time_in.time + configuration.margem < configuration.game_Time)
            {
                time_in.fail = true;
            }//Verifica se a nota passou da margem maxima.
            else
            {
                int correct = 0;

                if(time_in.time <= configuration.game_Time)
                {
                    if (time_in.notes.Length == 1)
                    {
                        if(CheckGhost(ref time_in))
                        {
                            correct++;
                        }
                    }
                    else
                    {
                        CheckGhost(ref time_in);
                    }
                }//Verifica se a ghost passou/chegou no ponto perfeito para tocar o som.

                for (int i = 0; i < time_in.notes.Length; i++)
                {
                    /*try
                    {
                        if (Sinal.Check_Note(time_in.notes[i].note))
                        {
                            if (correct == 0)
                            {
                                new WaitForSeconds(0.5f);
                                correct++;
                            }
                            else
                            {
                                correct++;
                            }
                        }
                    }
                    catch { Debug.Log("Note não encontrada."); }*/

                    try
                    {
                        if (Sinal.Check_Note(time_in.notes[i].note))
                        {
                            if (!time_in.notes[i].active) { time_in.notes[i].active = true; }
                        }
                    }
                    catch { Debug.Log("Note não encontrada."); }
                }

                for(int i = 0; i < time_in.notes.Length;i++)
                {
                    if (time_in.notes[i].active) { correct++; }
                }

                if(correct == time_in.notes.Length)
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

                /*if (correct != 0)
                {
                    int Ghost = 0;

                    for (int i = 0;i < time_in.notes.Length;i++)
                    {
                        if(time_in.notes[i].special == Note.music_special_note.Ghost){ Ghost++; }
                    }

                    if (correct >= time_in.notes.Length - Ghost)
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
                }*/
            }

            if (time_in.complet == true || time_in.fail == true)
            {
                
                

                if (time_in.fail == true) { combo = 0; }

                CheckGhost(ref time_in);
                this_time++;

                if (this_time < configuration.musica.times.Count)
                { 
                    time_in = configuration.musica.times[this_time];
                }
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
    }//Verifica o quao antes a nota tem que spawnar para chegar no tempo perfeito.

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
                        Spawn(music_time.notes[o]);
                    }
                }
            }
        }
    }//Verifica se tem que spawnar alguma nota no momento que é checado.
    
    public void Spawn(Note spawnNote)
    {
        string spawnName = "";

        switch (spawnNote.note)
        {
            case Note.music_note.Chimbal:
                spawnName = "Chimbal"; break;
            case Note.music_note.Caixa:
                spawnName = "Caixa"; break;
            case Note.music_note.TomUm:
                spawnName = "Tom_01"; break;
            case Note.music_note.TomDois:
                spawnName = "Tom_02"; break;
            case Note.music_note.Surdo:
                spawnName = "Surdo"; break;
            case Note.music_note.Bumbo:
                spawnName = "Bumbo"; break;
            case Note.music_note.Prato:
                spawnName = "Prato"; break;
        }

        for (int i = 0; i < configuration.spawns.Length; i++)
        {
            if (configuration.spawns[i].name == spawnName)
            {
                GameObject spawn_object = configuration.spawns[i].GetComponent<Game_Buttons>().getSpawn();
                GameObject note = null;

                if (spawnNote.special == Note.music_special_note.None)
                {
                     note = Instantiate(configuration.note_Empety, spawn_object.transform);
                }
                else if(spawnNote.special == Note.music_special_note.Continuous)
                {
                    note = Instantiate(configuration.note_Empety, spawn_object.transform);
                }
                else
                {
                    note = Instantiate(configuration.note_Ghost, spawn_object.transform);
                }

                SpriteRenderer noteSprite = note.GetComponent<SpriteRenderer>();

                note.GetComponent<Game_Note>().SetVelocity(configuration.musica.note_velocity);
                note.GetComponent<Game_Note>().SetTime(spawnNote.time);
                noteSprite.sprite = configuration.spawns[i].GetComponent<Game_Buttons>().Get_Sprite();
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
    public GameObject note_Empety = null;
    public GameObject note_Ghost = null;
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

    public static void Reset()
    {
        Chimbal = false; Caixa = false; TomUm = false; TomDois = false; Surdo = false; Bumbo = false; Prato = false;
    }
    public static bool Check_Note(Note.music_note note)
    {
        if (note == Note.music_note.Empty) { return true; }

        bool note_check = false;
        bool value = false;

        switch(note)
        {
            case Note.music_note.Chimbal:
                value = Chimbal; Chimbal = false; note_check = true; break;
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

        if (note_check) { return value; }

        throw new System.Exception();
    }
}