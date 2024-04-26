
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Game_Buttons : MonoBehaviour
{
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
        if(Input.GetKeyDown(key))
        {
            CLick();
        }
    }

    public void Touch()
    {
        if(Input.touchCount > 0)
        {
            Touch[] touch = Input.touches;
            Vector3[] touch_position = new Vector3[touch.Length];

            RaycastHit[] hit = new RaycastHit[touch.Length];

            for(int i = 0; i < touch.Length; i++)
            {
                touch_position[i] = touch[i].position;
            }

            for(int i = 0; i < touch.Length; i++)
            {
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.ScreenToWorldPoint(touch_position[i]), out hit[i]))
                {
                    if (hit[i].transform.tag == "Bateria")
                    {
                        hit[i].transform.gameObject.GetComponent<Game_Buttons>().CLick();
                    }
                }
            }
        }
    }

    public void CLick()
    {
        audio_source.PlayOneShot(note_sound[0]);
        animator.SetTrigger("Clicou");

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

    public void OnMouseDown()
    {
        CLick();
    }
}