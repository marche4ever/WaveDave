using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class Message : MonoBehaviour {


    public GameObject Default_Message;

    public GameObject Message_Panel;

    List<GameObject> Messages = new List<GameObject>();


    public int message_count;
    int count=0;
	// Use this for initialization
	void Start ()
    {

        //for (int i = 0; i < message_count; i++)
        //{
        //    Messages.Add(Instantiate(Default_Message, Message_Panel.transform) as GameObject);
        //    Messages[i].transform.name = ("Message #"+i);
        //    Messages[i].transform.Find("Text").GetComponent<Text>().text = Messages[i].transform.name;
        //}

        //Default_Message.SetActive(false);
    }

    // Update is called once per frame
    void Update ()
    {
       // Debug.Log(Messages.Count);

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //  //  Debug.Log(Messages.Count);

        //    MakeNewMsg("myMsg"+count);
        //    count++;
        //}
	}

    void MakeNewMsg(string msg)
    {
        Messages[0].transform.Find("Text").GetComponent<Text>().text = msg;
        int last_index=Messages[Messages.Count-1].transform.GetSiblingIndex();

        Messages[0].transform.SetSiblingIndex(last_index);


        GameObject go = Messages[0];
        Messages.RemoveAt(0);
        Messages.Add(go);
        Messages[Messages.Count - 1].GetComponent<Animator>().Play("Pop_Up");

    }
}
