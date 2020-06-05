using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gerencia o controle de locomoção do player.
/// </summary>
[RequireComponent(typeof(Animator))] //Obriga o componente Animator
public class ControllerPlayer : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Rate per seconds holding down input")]
    private float rotationRate = 360; //Valor para velocidade da rotação do player

    private string turnInputAxis = "Horizontal";

    private Animator anim; //Declarando o animator como uma variável

    public AudioSource[] somPassos;

    private void Awake()
    {
        anim = GetComponent<Animator>(); //Atribuindo o componente a variável antes da inicialização
    }

    private void Update()
    {
        float turnAxis = Input.GetAxis(turnInputAxis);

        ApplyInput(turnAxis);

        anim.SetFloat("Horizontal", Input.GetAxis("Horizontal")); //Atribui o valor da variável inputX para o parâmetro Horizontal do controle de animação
        anim.SetFloat("Vertical", Input.GetAxis("Vertical")); //Atribui o valor da variável inputY para o parâmetro Vertical do controle de animação

        //Correndo ou não
        if (Input.GetKey(KeyCode.LeftShift))
            anim.SetBool("Run", true); //Segurando o shift esquerdo a bool Run do controle de animação recebe true
        else
            anim.SetBool("Run", false); //Soltando o shift esquerdo a bool Run do controle de animação recebe false
        

        if (Input.GetKey(KeyCode.Space))
            anim.SetBool("Jump", true); //Segurando o shift esquerdo a bool Run do controle de animação recebe true
        else
            anim.SetBool("Jump", false); //Soltando o shift esquerdo a bool Run do controle de animação recebe false
    }

    private void ApplyInput(float turnInput)
    {
        Turn(turnInput);
    }

    private void Turn(float input)
    {
        transform.Rotate(0, input * rotationRate * Time.deltaTime, 0);
    }

    public void Passos()
    {
        if(!somPassos[0].isPlaying) {
            somPassos[0].Play();
        } else {
            somPassos[1].Play();
        }
    }
}
