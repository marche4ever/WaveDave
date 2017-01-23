using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public List<GameObject> path_Positions = new List<GameObject>();
    private List<GameObject> persons = new List<GameObject>();
    public GameObject person_Prefab;
    public float walking_Speed;

    public float person_Rate_Per_Second = 1;

    private float spawn_Timer = 0;

    public Sprite[] body_Sprites;
    public Sprite[] arm_Sprites_Up;
    public Sprite[] arm_Sprites_Down;

    public Sprite[] hat_Sprites;
    public Sprite[] head_Sprites;
    public Sprite[] pants_Sprites;

    public Sprite[] face_Sprites;

    public AudioClip[] sound_Effects;

    public int Points = 0;

    // Use this for initialization
    void Start()
    {

    }

    public void HeyDave()
    {
        GetComponent<AudioSource>().clip = sound_Effects[10];
        GetComponent<AudioSource>().Play();

    }
    void Spawn()
    {
        bool gender = Random.Range(0, 2) == 1;
        bool waving = Random.Range(0, 2) == 1;
        int body_index = Random.Range(0, body_Sprites.Length);
        int hat_index = Random.Range(0, hat_Sprites.Length);
        int head_index = Random.Range(0, head_Sprites.Length);
        int pants_index = Random.Range(0, pants_Sprites.Length);

        GameObject temp = (GameObject)Instantiate(person_Prefab, path_Positions[body_index].transform.position, Quaternion.identity);
        temp.GetComponent<Person>().Init(body_Sprites[body_index], arm_Sprites_Down[body_index], hat_Sprites[hat_index], head_Sprites[head_index], pants_Sprites[pants_index], waving, body_index);

        persons.Add(temp);

    }


    public void restart_game()
    {
        SceneManager.LoadScene(0);
    }
    // Update is called once per frame
    void Update()
    {

        spawn_Timer += Time.deltaTime;

        if (spawn_Timer > (1 / person_Rate_Per_Second))
        {
            //person_Rate_Per_Second = Random.Range (0.3f, 0.7f);
            person_Rate_Per_Second += Time.deltaTime * 0.1f;
            walking_Speed += Time.deltaTime;
            spawn_Timer = 0;
            Spawn();
        }

        bool waved_success = false;
        bool wave_input = false;

        for (int i = persons.Count - 1; i >= 0; i--)
        {

            int temp_Path_Index = persons[i].GetComponent<Person>().path_Index;

            if (temp_Path_Index == 5)
                if (persons[i].GetComponent<Person>().waving)
                    persons[i].GetComponent<Person>().Set_Waving(arm_Sprites_Up[persons[i].GetComponent<Person>().body_Index]);

            Vector3 path_Position_1 = path_Positions[temp_Path_Index].transform.position;
            Vector3 path_Position_2 = path_Positions[temp_Path_Index + 1].transform.position;


            float line_Magnitude = Vector3.Distance(path_Position_1, path_Position_2);

            float speed = 1 / line_Magnitude * walking_Speed;

            persons[i].GetComponent<Person>().lerp_Time += Time.deltaTime * speed;
            persons[i].transform.position = Vector3.Lerp(path_Positions[temp_Path_Index].transform.position, path_Positions[temp_Path_Index + 1].transform.position, persons[i].GetComponent<Person>().lerp_Time);

            if (persons[i].GetComponent<Person>().lerp_Time > 1.0f)
            {
                persons[i].GetComponent<Person>().lerp_Time = 0;
                persons[i].GetComponent<Person>().path_Index++;

                if (persons[i].GetComponent<Person>().path_Index + 1 > path_Positions.Count - 1)
                {

                    if(persons[i].GetComponent<Person>().waving)
                    {
                        Debug.Log("Missed");
                        Points--;

                        GetComponent<AudioSource>().clip = sound_Effects[3];
                        GetComponent<AudioSource>().Play();
                    }
                    GameObject temp = persons[i];
                    persons.RemoveAt(i);

                    Destroy(temp);


                }
            }

            if(Input.GetKeyDown(KeyCode.Space) || GetComponent<Read_From_Arduino>().input_Read)
            {
               if( persons[i].GetComponent<Person>().waving && temp_Path_Index > 7 )
                {
                    persons[i].GetComponent<Person>().waving = false;
                    persons[i].GetComponent<Person>().waved_Successfully = true;
                    persons[i].GetComponent<Person>().Set_Waving(arm_Sprites_Down[persons[i].GetComponent<Person>().body_Index]);
                    persons[i].GetComponent<Person>().Set_Face_Expression(face_Sprites[1]);
                    waved_success = true;
                    Points++;

                    Debug.Log("Success! Points = " + Points);

                    GetComponent<AudioSource>().clip = sound_Effects[0];
                    GetComponent<AudioSource>().Play();
                    Debug.Log(GetComponent<Read_From_Arduino>().input_Read);
                }
           
            }
           

            //if (temp_Path_Index > 7 && temp_Path_Index <9)
            //{
            //    // || GetComponent<Read_From_Arduino>().input_Read) 
            //    if ((Input.GetKeyDown(KeyCode.Space) && persons[i].GetComponent<Person>().waving))
            //    {
            //        persons[i].GetComponent<Person>().waving = false;
            //        persons[i].GetComponent<Person>().Set_Waving(arm_Sprites_Down[persons[i].GetComponent<Person>().body_Index]);
            //        persons[i].GetComponent<Person>().Set_Face_Expression(face_Sprites[1]);
            //        waved_success = true;
            //        Points++;

            //        Debug.Log("Success! Points = " + Points);

            //        Debug.Log(GetComponent<Read_From_Arduino>().input_Read);
            //    }
            //   else if((!Input.GetKeyDown(KeyCode.Space) && persons[i].GetComponent<Person>().waving))
            //    {
            //        persons[i].GetComponent<Person>().waving = false;
            //        persons[i].GetComponent<Person>().Set_Waving(arm_Sprites_Down[persons[i].GetComponent<Person>().body_Index]);
            //        persons[i].GetComponent<Person>().Set_Face_Expression(face_Sprites[0]);
            //        Debug.Log("Missed " + Points);
            //        Points--;
            //    }
                
            //}
            //else if(temp_Path_Index>9)
            //{

            //}

        }

        
        if (Input.GetKeyDown(KeyCode.Space) || GetComponent<Read_From_Arduino>().input_Read)
        {
            if (!waved_success)
            {
                for (int i = persons.Count - 1; i >= 0; i--)
                {
                    if (persons[i].GetComponent<Person>().path_Index > 7 && !persons[i].GetComponent<Person>().waved_Successfully)
                        persons[i].GetComponent<Person>().Set_Face_Expression(face_Sprites[0]);

                    GetComponent<AudioSource>().clip = sound_Effects[1];
                    GetComponent<AudioSource>().Play();
                }
                Debug.Log("Missed " + Points);
                Points--;
                if (Points < 0)
                    Application.LoadLevel(Application.loadedLevel);
            }
        }

    }
}
