using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MusicasManager : MonoBehaviour
{
    [SerializeField] private List<Musica> music_list = new List<Musica>();

    [SerializeField] private Button Opcao_01;

    [SerializeField] private Button Opcao_02;

    [SerializeField] private Button Opcao_03;

    [SerializeField] private Button Opcao_04;

    [SerializeField] private Button Opcao_05;

    void Start()
    {

        music_list[0].stars_music = MenuManager.musica_01;
        music_list[1].stars_music = MenuManager.musica_02;
        music_list[2].stars_music = MenuManager.musica_03;
        music_list[3].stars_music = MenuManager.musica_04;
        music_list[4].stars_music = MenuManager.musica_05;

        Opcao_01.music = music_list[0];
        Opcao_02.music = music_list[1];
        Opcao_03.music = music_list[2];
        Opcao_04.music = music_list[3];
        Opcao_05.music = music_list[4];

        for(int i = 0; i < music_list.Count; i++)
        {
            music_list[i].number = i;
            UnityEngine.Debug.Log(i);
            try
            {
                if (music_list[i].tarefa)
                {
                    music_list[i].stars_music = MenuManager.Stars_Get(i + 1, true);
                }
                else
                {
                    music_list[i].stars_music = MenuManager.Stars_Get(i + 1, false);
                }
                
                if(music_list[i].stars_required <= MenuManager.Stars_All())
                {
                    music_list[i].bloqueada = false;
                }
            }
            catch (StarsException)
            {
                music_list[i].stars_music = 0;
                music_list[i].bloqueada = true;
                UnityEngine.Debug.Log("Execption in music/task: Music/Task " + music_list[i].number);
                UnityEngine.Debug.Log("list number " + i);
            }
        }
    }

    void Update()
    {
        if (Opcao_01.image.sprite != Opcao_01.music.image)
        {
            Opcao_01.image.sprite = Opcao_01.music.image;
            Opcao_02.image.sprite = Opcao_02.music.image;
            Opcao_03.image.sprite = Opcao_03.music.image;
            Opcao_04.image.sprite = Opcao_04.music.image;
            Opcao_05.image.sprite = Opcao_05.music.image;
        }
    }
    public void Up()
    {
        if (Opcao_01.music.number - 1 >= 0)
        {
            Opcao_05.music = Opcao_04.music;
            Opcao_04.music = Opcao_03.music;
            Opcao_03.music = Opcao_02.music;
            Opcao_02.music = Opcao_01.music;

            Opcao_01.music = music_list[Opcao_01.music.number - 1];
        }
    }

    public void Down()
    {
        if (Opcao_05.music.number < music_list.Count - 1)
        {
            Opcao_01.music = Opcao_02.music;
            Opcao_02.music = Opcao_03.music;
            Opcao_03.music = Opcao_04.music;
            Opcao_04.music = Opcao_05.music;

            Opcao_05.music = music_list[Opcao_05.music.number + 1];
        }
    }

    public void Choose_Music(int button)
    {
        UnityEngine.Debug.Log("Button Press:" + button + ".");

        switch(button)
        {
            case 0:
                UnityEngine.Debug.Log("Op��o 0 open"); Game(Opcao_01.music); break;
            case 1:
                UnityEngine.Debug.Log("Op��o 1 open");  Game(Opcao_02.music); break;
            case 2:
                UnityEngine.Debug.Log("Op��o 2 open");  Game(Opcao_03.music); break;
            case 3:
                UnityEngine.Debug.Log("Op��o 3 open");  Game(Opcao_04.music); break;
            case 4:
                UnityEngine.Debug.Log("Op��o 4 open"); Game(Opcao_05.music); break;
        }
    }

    public void Game(Musica music)
    {
        if(music.bloqueada==false)
        {
            UnityEngine.Debug.Log("Entrando no Jogo");
            Game_Music.Set_Musica(music);
            MenuManager._MenuAtual = "Game";
            SceneManager.LoadScene("Game");
        }
        else { UnityEngine.Debug.Log("Voc� n�o liberou esta li��o."); }
    }

    [System.Serializable]
    public class Button
    {
        public Musica music;
        public GameObject button;
        public UnityEngine.UI.Image image;
    }
}

[System.Serializable]
public class Musica
{
    public string name = "Sem Nome";
    public float metronomo = 0;
    public float start_music = 0;
    public float note_velocity = 0;
    public AudioClip soundTrack;
    public int number = 0;
    public int stars_music = 0;
    public float stars_required = 0;
    public List<Times> times = new List<Times>();
    public bool tarefa = false;
    public bool bloqueada = true;
    public UnityEngine.Sprite image;
}

[System.Serializable]
public class Times
{
    public Game_System game_system;
    public enum metronomo_type { time , conter_time }

    public metronomo_type type;
    public Note[] notes = new Note[7];
    public float time = 0;
    public bool spawned = false;
    public bool complet = false;
    public bool fail = false;

    public void Check()
    {

        if (fail)
        {
            complet = true;
        }
        else
        {
            int check = notes.Length;
            int value = 0;

            for (int i = 0; i < notes.Length; i++)
            {
                if (notes[i].active == false) { time++; }
            }

            if (value == check) { complet = true; }

            game_system.sinal.Score_Sinal = true;
        }
    }
}

[System.Serializable]
public class Note
{
    public music_note note = music_note.Empty;
    public music_beat beat = music_beat.Average;
    public music_special_note special = music_special_note.None;
    private Times _time = null; public Times time { get; set; }
    public bool active = true;

    public enum music_note
    {
        Empty,Chimbal,Caixa, TomUm, TomDois, Surdo,Bumbo,Prato
    }

    public enum music_beat
    {
        Weak,Average,Strong
    }
    public enum music_special_note
    {
        None,Continuous,Ghost
    }
}


