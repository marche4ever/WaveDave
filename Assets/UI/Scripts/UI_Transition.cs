using UnityEngine;
using System.Collections;

public class UI_Transition : MonoBehaviour {
    public GameObject Menu_Panel;
    public GameObject Game_Panel;
    public GameObject Option_Panel;

     // Use this for initialization




    void Start () {
        
	}
	

	// Update is called once per frame
	void Update () {
	
	}

    public void To_GamePanel()
    {
   
        Menu_Panel.SetActive(false);
        Option_Panel.SetActive(false);
        Game_Panel.SetActive(true);
    
         }

    public void To_MenuPanel()
    {
  
        Game_Panel.SetActive(false);
        Option_Panel.SetActive(false);
        Menu_Panel.SetActive(true);
    }
    public void To_OptionPanel()
    {
        Game_Panel.SetActive(false);
      
        Option_Panel.SetActive(true);
    }
    
}
