using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]
public class Chave : MonoBehaviour {
	
	public int IDDaChave = 0;
	[Range(0.1f,10.0f)]
	public float distanciaMinima = 3;
	public KeyCode teclaParaPegar = KeyCode.E;
	public AudioClip somDePegar;
	public GameObject player;

	AudioSource audSource;
	bool caught = false;

	void Start(){
		audSource = GetComponent<AudioSource> ();
		audSource.playOnAwake = false;
		audSource.loop = false;
	}

	void Update(){
		if (!caught && player) {
			float distance = Vector3.Distance (player.transform.position, transform.position);
			if (distance < distanciaMinima) {
				if (Input.GetKeyDown (teclaParaPegar)) {
					ControladorDasChaves.chavesDoJogador.Add (IDDaChave);
					caught = true;
					StartCoroutine ("DestroyObject");
				}
			}
		}
	}

	IEnumerator DestroyObject(){
		MeshRenderer renderer = GetComponentInChildren <MeshRenderer> ();
		if (renderer) {
			renderer.enabled = false;
		}
		if (somDePegar) {
			audSource.clip = somDePegar;
			audSource.PlayOneShot (audSource.clip);
		}
		yield return new WaitForSeconds (3);
		Destroy (gameObject);
	}
}