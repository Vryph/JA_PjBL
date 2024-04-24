using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Game_Buttons : MonoBehaviour
{
    [SerializeField] private Note.music_note note;
    [SerializeField] private KeyCode key;
    [SerializeField] private GameObject spawn; public GameObject getSpawn() { return spawn; }
    [SerializeField] private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if(Input.GetKeyDown(key))
        {
            Debug.Log("Signal Bateria = " + note.HumanName());

            switch (note)
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
            }
        }
        animator.SetTrigger("Clicou");
    }

    public void OnMouseDown()
    {
        Debug.Log("Signal Bateria = " + note.HumanName());

        switch(note)
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
        }

    }
}