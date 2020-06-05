using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Porta : MonoBehaviour {

	public GameObject player;
	public int IDDaPorta = 0;
	public enum _initialState {Aberta90, Fechada00, Trancada};
	public _initialState estadoInicial = _initialState.Fechada00;
	public enum _rotationType {GiraEmX, GiraEmY, GiraEmZ};
	public _rotationType rotationType = _rotationType.GiraEmY;
	public KeyCode teclaParaAbrir = KeyCode.E;
	public float grausDeGiro = 90.0f;
	[Range(0.1f,10.0f)]
	public float velocidadeDeRotacao = 5;
	[Range(0.1f,10.0f)]
	public float distanciaMinima = 4;
	public bool inverterRotacao = false;
	public bool abrirAutomaticamente = false;

	//txt
	[Space(10)][Header("TEXTOS")]
	public Text textoAperteE;
	public Text textoPortaTrancada;
	[Range(0.1f,4.0f)]
	public float tempoTextoPortaTrancada = 1;

	//sounds
	[Space(10)][Header("SONS")]
	public AudioClip somDeAbrir;
	[Range(0.5f,3.0f)]
	public float velocidadeSomDeAbrir = 1;
	[Space(3)]
	public AudioClip somDeFechar;
	[Range(0.5f,3.0f)]
	public float velocidadeSomDeFechar = 1;
	[Space(3)]
	public AudioClip somDeTrancado;
	public AudioClip somDeDestrancando;

	//OPCIONAL, DEBUG
	[Space(10)][Header("OPCIONAL")]
	public GameObject pontoDeProximidade;

	Vector3 initialRotation;
	float currentRotation;
	float targetRotation;
	bool isClosed;
	bool close;
	bool isLocked;
	AudioSource audSource;

	void Start () {
		initialRotation = transform.eulerAngles;
		audSource = GetComponent<AudioSource> ();
		audSource.playOnAwake = false;
		audSource.loop = false;
		if (textoPortaTrancada) {
			textoPortaTrancada.enabled = false;
		}
		if (textoAperteE) {
			textoAperteE.enabled = false;
		}
		switch (estadoInicial) {
		case _initialState.Fechada00:
			isClosed = true;
			isLocked = false;
			targetRotation = 0.0f;
			currentRotation = 0.0f;
			break;
		case _initialState.Aberta90:
			isClosed = false;
			isLocked = false;
			if (inverterRotacao) {
				currentRotation = grausDeGiro;
				targetRotation = grausDeGiro;
			} else {
				currentRotation = -grausDeGiro;
				targetRotation = -grausDeGiro;
			}
			break;
		case _initialState.Trancada:
			isClosed = true;
			isLocked = true;
			targetRotation = 0.0f;
			currentRotation = 0.0f;
			break;
		}
	}

	void Update () {
		if (player) {
			DoorController ();
			RotateDoor ();
		}
	}

	void CheckKey(){
		bool haveTheKey = false;
		for (int x = 0; x < ControladorDasChaves.chavesDoJogador.Count; x++) {
			if (ControladorDasChaves.chavesDoJogador [x] == IDDaPorta) {
				haveTheKey = true;
			}
		}
		if (haveTheKey) {
			isLocked = false;
			if (somDeDestrancando) {
				audSource.pitch = 1;
				audSource.clip = somDeDestrancando ;
				audSource.PlayOneShot (audSource.clip);
			}
		} else {
			if (somDeTrancado) {
				audSource.pitch = 1;
				audSource.clip = somDeTrancado;
				audSource.PlayOneShot (audSource.clip);
			}
			StartCoroutine ("MessageOnTheScreen");
		}
	}

	void DoorController(){
		Vector3 localDeChecagem;
		if (pontoDeProximidade) {
			localDeChecagem = pontoDeProximidade.transform.position;
		} else {
			localDeChecagem = transform.position;
		}
		if (Vector3.Distance (player.transform.position, localDeChecagem) < distanciaMinima) {
			if (abrirAutomaticamente) {
				if (!close && !isLocked) {
					close = true;
					isClosed = !isClosed;
					CheckParameters ();
				}
			} else {
				if (textoAperteE) {
					textoAperteE.enabled = true;
				}
				if (Input.GetKeyDown (teclaParaAbrir) && !isLocked) {
					isClosed = !isClosed;
					CheckParameters ();
				}
				if (Input.GetKeyDown (teclaParaAbrir) && isLocked) {
					CheckKey ();
				}
			}
		} else {
			if (textoAperteE) {
				textoAperteE.enabled = false;
			}
			if (abrirAutomaticamente) {
				if (close && !isLocked) {
					close = false;
					isClosed = !isClosed;
					CheckParameters ();
				}
			}
		}
		currentRotation = Mathf.Lerp (currentRotation, targetRotation, Time.deltaTime * velocidadeDeRotacao);
	}

	void CheckParameters(){
		if (!isClosed) {
			if (somDeAbrir) {
				audSource.pitch = velocidadeSomDeAbrir;
				audSource.clip = somDeAbrir;
				audSource.PlayOneShot (audSource.clip);
			}
			if (inverterRotacao) {
				targetRotation = grausDeGiro;
			} else {
				targetRotation = -grausDeGiro;
			}
		} else {
			if (somDeFechar) {
				audSource.pitch = velocidadeSomDeFechar;
				audSource.clip = somDeFechar;
				audSource.PlayOneShot (audSource.clip);
			}
			if (inverterRotacao) {
				targetRotation = 0.0f;
			} else {
				targetRotation = 0.0f;
			}
		}
	}

	void RotateDoor(){
		switch (rotationType) {
		case _rotationType.GiraEmX:
			transform.eulerAngles = new Vector3 (initialRotation.x + currentRotation, initialRotation.y, initialRotation.z);
			break;
		case _rotationType.GiraEmY:
			transform.eulerAngles = new Vector3 (initialRotation.x, initialRotation.y + currentRotation, initialRotation.z);
			break;
		case _rotationType.GiraEmZ:
			transform.eulerAngles = new Vector3 (initialRotation.x, initialRotation.y, initialRotation.z + currentRotation);
			break;
		}
	}

	IEnumerator MessageOnTheScreen(){
		if (textoPortaTrancada) {
			textoPortaTrancada.enabled = true;
			yield return new WaitForSeconds (tempoTextoPortaTrancada);
			textoPortaTrancada.enabled = false;
		}
	}
}