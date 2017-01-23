using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System;

public class ReadFromArduino : MonoBehaviour {

	SerialPort sp;
	void Start () {
		sp = new SerialPort("COM6", 9600);
		try {
			sp = new SerialPort("COM6", 9600);
		}
		catch (Exception ex){
			Debug.Log ("potato not working" + ex);
		}
		sp.Open ();
		sp.ReadTimeout = 1;
	}

	void Update () 
	{
		try{

			Debug.Log(sp.ReadLine());
			Debug.Log ("fharte"); 
		}
		catch(System.Exception){

		}
	}
}