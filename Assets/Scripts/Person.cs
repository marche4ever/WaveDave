using UnityEngine;
using System.Collections;

public class Person : MonoBehaviour {

	//the current point (position) on the path the person is walking on
	public int path_Index = 0;
	public float lerp_Time = 0;
	public int body_Index = 0;

	public bool death = false;
    public bool waved_Successfully = false;

	public GameObject torso;
	public GameObject arm;

	public GameObject hat;
	public GameObject head;
	public GameObject pants;
    public GameObject face;


	public bool waving = false;

    private Color rand_color;
    // Use this for initialization
    void Start () {
	}

	public void Init(Sprite _body, Sprite _arm, Sprite _hat, Sprite _head, Sprite _pants, bool _waving, int _body_Index) {

		rand_color = new Color( 0.5f + Random.value, 0.5f + Random.value, 0.5f + Random.value, 1.0f );

		torso.GetComponent<Renderer> ().material.color = rand_color;
		arm.GetComponent<Renderer> ().material.color = rand_color;

		torso.GetComponent<SpriteRenderer> ().sprite = _body;
		arm.GetComponent<SpriteRenderer> ().sprite = _arm;
		hat.GetComponent<SpriteRenderer> ().sprite = _hat;
		head.GetComponent<SpriteRenderer> ().sprite = _head;
		pants.GetComponent<SpriteRenderer> ().sprite = _pants;
		waving = _waving;
		body_Index = _body_Index;
	}

	public void Set_Waving(Sprite _arm)
	{
		arm.GetComponent<SpriteRenderer> ().sprite = _arm;
	}

    public void Set_Face_Expression(Sprite _face)
    {
        face.GetComponent<SpriteRenderer>().sprite = _face;
        face.GetComponent<Renderer>().material.color = rand_color;

    }

    // Update is called once per frame
    void Update () {
	
	}
}
