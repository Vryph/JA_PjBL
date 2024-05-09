using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bateria : MonoBehaviour
{
    public int[] touch_trigger;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Touch();
        MouseDown();
    }

    public void Touch()
    {
        if (Input.touchCount > 0)
        {
            Touch[] touch = Input.touches;

            int[] nullID = new int[touch_trigger.Length];

            for (int i = 0; i < touch_trigger.Length; i++)
            {
                if (touch_trigger[i] != touch[i].GetHashCode())
                {
                    nullID[i] = i;
                }
            }

            ////////////////////////////////////////////////////////////////////////

            if(true)
            {
                int[] save = new int[touch_trigger.Length - nullID.Length];

                int o = 0;

                for (int i = 0; i < touch_trigger.Length; i++)
                {
                    if (nullID[i] == i)
                    {
                        save[o] = touch_trigger[i];
                    }
                    o++;
                }

                touch_trigger = save;
            }
            

            if (touch.Length != touch_trigger.Length)
            {
                int[] save = new int[touch_trigger.Length];
                touch_trigger = new int[touch.Length];

                if (save != null)
                {
                    for (int i = 0; i < save.Length; i++)
                    {
                        touch_trigger[i] = save[i];
                    }
                }
            }

            if(true)
            {
                int[] save = new int[touch.Length];

                for (int i = 0; i < touch.Length; i++)
                {
                    int tentativas = 0;
                    for (int o = 0; o < touch_trigger.Length; o++)
                    {
                        if (touch[i].GetHashCode() == touch_trigger[o])
                        {
                            save[i] = touch_trigger[o];
                            tentativas++;
                        }
                    }
                }

                touch_trigger = save;
            }
            
            ////////////////////////////////////////////////////////////////////////

            Ray ray;

            for (int i = 0; i < touch.Length; i++)
            {
                if (touch.GetHashCode() != touch_trigger[i])
                {
                    ray = Camera.main.ScreenPointToRay(touch[i].position);

                    if (Physics.Raycast(ray.origin, ray.direction * 100, out RaycastHit hit))
                    {
                        if (hit.transform.GetComponent<Game_Buttons>() != null)
                        {
                            hit.transform.gameObject.GetComponent<Game_Buttons>().CLick();
                        }
                    }
                }
            }
        }
    }
    public void MouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction * 100, out RaycastHit hit))
            {
                Debug.Log("Chegou em algo.");

                DrawCube(hit.transform.position, 5, UnityEngine.Color.red);

                if (hit.transform.GetComponent<Game_Buttons>() != null)
                {
                    Debug.Log("Algo tem Game Buttons.");
                    hit.transform.gameObject.GetComponent<Game_Buttons>().CLick();
                }
            }
        }
    }
    public void DrawCube(Vector3 point, float size, UnityEngine.Color color)
    {
        Vector3[] points = new Vector3[4];

        points[0] = new Vector3(point.x - size / 2, point.y - size / 2, point.z);
        points[1] = new Vector3(point.x + size / 2, point.y - size / 2, point.z);
        points[2] = new Vector3(point.x + size / 2, point.y + size / 2, point.z);
        points[3] = new Vector3(point.x - size / 2, point.y + size / 2, point.z);

        Debug.DrawLine(points[0], points[1], color, 5f);
        Debug.DrawLine(points[0], points[3], color, 5f);
        Debug.DrawLine(points[2], points[1], color, 5f);
        Debug.DrawLine(points[2], points[3], color, 5f);
    }
}
