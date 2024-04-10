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
        Opcao_01.music = music_list[0];
        Opcao_02.music = music_list[1];
        Opcao_03.music = music_list[2];
        Opcao_04.music = music_list[3];
        Opcao_05.music = music_list[4];
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
        if (Opcao_01.music.number - 1 >= 0)
        {
            Opcao_05.music = Opcao_04.music; Opcao_04.music = Opcao_03.music; Opcao_03.music = Opcao_02.music; Opcao_02.music = Opcao_01.music;

            Opcao_01.music = music_list[Opcao_01.music.number - 1];
        }
    }

    public void Down()
    {
        if(Opcao_05.music.number+1 <= music_list.Count-1)
        {
            Opcao_01.music = Opcao_02.music; Opcao_02.music = Opcao_03.music; Opcao_03.music = Opcao_04.music; Opcao_04.music = Opcao_05.music;

            Opcao_05.music = music_list[Opcao_05.music.number + 1];
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
    public float metronomo = 1;
    public List<Note> notes = new List<Note>();
    public bool bloqueada = true;
}

[System.Serializable]
public class Note
{
    public music_note note = music_note.Empty;
    public music_beat beat = music_beat.Average;
    public bool active = true;

    public enum music_note
    {
        Empty,Chimbal,Caixa,Tom_1,Tom_2,Surdo,Bumbo,Prato
    }
    public enum music_beat
    {
        Weak,Average,Strong
    }
}


