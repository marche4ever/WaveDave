using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System;
using UnityEngine.UI;

public class Read_From_Arduino : MonoBehaviour
{
    SerialPort sp;

    public GameObject background;

    public bool input_Read = false;

    private float input_Read_TimeOut = 1.0f;

    void Start()
    {
        sp = new SerialPort("COM6", 9600);
        try
        {
            sp = new SerialPort("COM6", 9600);
        }
        catch (Exception ex)
        {
            Debug.Log("potato not working" + ex);
        }
        sp.Open();
        sp.ReadTimeout = 1;
    }

    void Update()
    {
        input_Read_TimeOut -= Time.deltaTime;
        input_Read = false;

        try
        {
            //Debug.Log(input_Read_TimeOut);
            
                //  Debug.Log(sp.ReadLine());


                if (sp.ReadLine() == "0")
                {
                    background.GetComponent<Image>().color = Color.white;
                }
                else if(input_Read_TimeOut < 0)
                {
                    input_Read_TimeOut = 0.5f;
                    background.GetComponent<Image>().color = Color.red;
                    input_Read = true;
                }

                
         //   Debug.Log("fharte");
        }
        catch (System.Exception)
        {

        }
    }
}