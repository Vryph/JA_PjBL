
using System.Drawing;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(AudioSource))]
public class Game_Buttons : MonoBehaviour
{
    [SerializeField] private Game_System game_system;
    [SerializeField] private bool emite_sinal = false;

    [SerializeField] private Note.music_note note;
    [SerializeField] private AudioClip[] note_sound;
    [SerializeField] private KeyCode key;
    [SerializeField] private Sprite note_sprite; public Sprite Get_Sprite() { return note_sprite; }

    private AudioSource audio_source;
    private Animator animator;
    [SerializeField] private GameObject spawn; public GameObject getSpawn() { return spawn; }
    void Start()
    {
        animator = GetComponent<Animator>();
        audio_source = GetComponent<AudioSource>();
    }
    void Update()
    {
        if(FadeIn.color_a.a == 0)
        {
            if (Input.GetKeyDown(key))
            {
                CLick();
            }
        }
    }

    public void PlayAudio()
    {
        audio_source.PlayOneShot(note_sound[0]);
    }

    public void CLick()
    {
        PlayAudio();
        animator.SetTrigger("Clicou");
        
        if(!emite_sinal)
        {
            /*switch (note)
            {
                case Note.music_note.Chimbal:
                    Sinal.Chimbal = true; break;
                case Note.music_note.Caixa:
                    Sinal.Caixa = true; break;
                case Note.music_note.TomUm:
                    Sinal.TomUm = true; break;
                case Note.music_note.TomDois:
                    Sinal.TomDois = true; break;
                case Note.music_note.Bumbo:
                    Sinal.Bumbo = true; break;
                case Note.music_note.Surdo:
                    Sinal.Surdo = true; break;
                case Note.music_note.Prato:
                    Sinal.Prato = true; break;
            }*/
        }
        else
        {
            Sinal sinal = game_system.sinal;

            switch (note)
            {
                case Note.music_note.Chimbal:
                    sinal.Chimbal = true; break;
                case Note.music_note.Caixa:
                    sinal.Caixa = true; break;
                case Note.music_note.TomUm:
                    sinal.TomUm = true; break;
                case Note.music_note.TomDois:
                    sinal.TomDois = true; break;
                case Note.music_note.Bumbo:
                    sinal.Bumbo = true; break;
                case Note.music_note.Surdo:
                    sinal.Surdo = true; break;
                case Note.music_note.Prato:
                    sinal.Prato = true; break;
            }
            
        }
        
       
    }
}