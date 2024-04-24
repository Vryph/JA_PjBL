using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if(Opcao_01.text.text != Opcao_01.music.name)
        {
            Opcao_01.text.text = Opcao_01.music.name;
            Opcao_02.text.text = Opcao_02.music.name;
            Opcao_03.text.text = Opcao_03.music.name;
            Opcao_04.text.text = Opcao_04.music.name;
            Opcao_05.text.text = Opcao_05.music.name;
        }
    }
    public void Up()
    {
        if (Opcao_01.music.number - 2 >= 0)
        {
            Opcao_05.music = Opcao_04.music; Opcao_04.music = Opcao_03.music; Opcao_03.music = Opcao_02.music; Opcao_02.music = Opcao_01.music;

            Opcao_01.music = music_list[Opcao_01.music.number-2];
        }
    }

    public void Down()
    {
        if(Opcao_05.music.number <= music_list.Count-1)
        {
            Opcao_01.music = Opcao_02.music; Opcao_02.music = Opcao_03.music; Opcao_03.music = Opcao_04.music; Opcao_04.music = Opcao_05.music;

            Opcao_05.music = music_list[Opcao_05.music.number];
        }
    }

    public void Choose_Music(int button)
    {
        UnityEngine.Debug.Log("Button Press:" + button + ".");

        switch(button)
        {
            case 0:
                UnityEngine.Debug.Log("Opção 0 open"); Game(Opcao_01.music); break;
            case 1:
                UnityEngine.Debug.Log("Opção 1 open");  Game(Opcao_02.music); break;
            case 2:
                UnityEngine.Debug.Log("Opção 2 open");  Game(Opcao_03.music); break;
            case 3:
                UnityEngine.Debug.Log("Opção 3 open");  Game(Opcao_04.music); break;
            case 4:
                UnityEngine.Debug.Log("Opção 4 open"); Game(Opcao_05.music); break;
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
        else { UnityEngine.Debug.Log("Você não liberou esta lição."); }
    }

    [System.Serializable]
    public class Button
    {
        public Musica music;
        public GameObject button;
        public TextMeshProUGUI text;
    }
}

[System.Serializable]
public class Musica
{
    public string name = "Sem Nome";
    public int number = 0;
    public int stars_music = 0;
    public float stars_required = 0;
    public List<Times> times = new List<Times>();
    public bool tarefa = false;
    public bool bloqueada = true;
}

[System.Serializable]
public class Times
{
    public Note[] notes = new Note[7];
    public float time = 0;
    public bool spawned = false;
    public bool complet = false;
    public bool fail = false;
    public int position_In_Music = 0;

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

            Sinal.Score_Sinal = true;
        }
    }
}

[System.Serializable]
public class Note
{
    public music_note note = music_note.Empty;
    public music_beat beat = music_beat.Average;
    public bool active = true;

 

    public enum music_note
    {
        Empty,Chimbal,Caixa, TomUm, TomDois, Surdo,Bumbo,Prato
    }

    public enum music_beat
    {
        Weak,Average,Strong
    }
}


