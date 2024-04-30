using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bateria : MonoBehaviour
{
    // Start is called before the first frame update
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
            Vector2[] touch_position = new Vector2[touch.Length];

            RaycastHit hit;

            for (int i = 0; i < touch.Length; i++)
            {
                touch_position[i] = touch[i].position;

                if (Physics.Raycast(Camera.main.transform.position, Camera.main.ScreenToWorldPoint(touch_position[i]), out hit, 250))
                {
                    if (hit.transform.GetComponent<Game_Buttons>() != null)
                    {
                        hit.transform.gameObject.GetComponent<Game_Buttons>().CLick();
                    }
                }
            }
        }
    }
    public void MouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3[] touch_position = new Vector3[3];

            RaycastHit hit;

            for (int i = 0; i < touch_position.Length; i++)
            {
                touch_position[i] = Input.mousePosition;
                Debug.Log("Mouse Position: X " + touch_position[i].x + "Y" + touch_position[i].y);

                touch_position[i] = Camera.main.ScreenToWorldPoint(touch_position[i]);
                Debug.Log("Mouse Position To World: X " + touch_position[i].x + "Y" + touch_position[i].y);

                Vector3 direction = new Vector3(touch_position[i].x, touch_position[i].y, 20);

                Debug.DrawLine(touch_position[i], Camera.main.transform.forward, UnityEngine.Color.red, 5f);
                Debug.DrawLine(touch_position[i], direction, UnityEngine.Color.black, 5f);

                DrawCube(direction, 10, UnityEngine.Color.black);

                if (Physics.Raycast(touch_position[i], direction ,out hit))
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
