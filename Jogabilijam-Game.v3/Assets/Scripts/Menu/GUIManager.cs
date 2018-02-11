using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;


public class GUIManager : MonoBehaviour
{
    public Button BotaoJogar, BotaoOpcoes, BotaoSair;
    private string nomeDaCena;
    string CharSelect;//carreg a próxima cena, no caso seleção de carros
    public string Creditos;// Use this for initialization
    public GameObject currentButton;
    public AxisEventData currentAxis;
    private float timer = 0;
    private float timeBetweenInputs = 0.15f;
    private Vector2 posicao;

    void Start()
    {
        nomeDaCena = SceneManager.GetActiveScene().name; //instancia a tela inicial.
        posicao = new Vector3();
      
        // =========SETAR BOTOES (Instancia os objetos de acordo com as opções desejadas)==========//
        BotaoJogar.onClick = new Button.ButtonClickedEvent();
        BotaoOpcoes.onClick = new Button.ButtonClickedEvent();
        BotaoSair.onClick = new Button.ButtonClickedEvent();
        //funciona no ato do click sobre o botão, executando a ação desejada. (usado quando tem o mouse)
        //BotaoJogar.onClick.AddListener(() => SceneManager.LoadScene("CharSelect"));
       // BotaoOpcoes.onClick.AddListener(() => SceneManager.LoadScene("Creditos"));
        //BotaoSair.onClick.AddListener(() => Application.Quit());
    }

    // Update is called once per frame
    void Update()
    {
        //Selecao();
    }

    void Selecao()
    {
        if (timer == 0)
        {
            currentAxis = new AxisEventData(EventSystem.current);
            currentButton = EventSystem.current.currentSelectedGameObject;

            if (Input.GetAxis("Horizontal") > 0)//move right
            {
                currentAxis.moveDir = MoveDirection.Right;
                ExecuteEvents.Execute(currentButton, currentAxis, ExecuteEvents.moveHandler);
                timer = timeBetweenInputs;
            }
            else if (Input.GetAxis("Horizontal") < 0)//move left
            {
                currentAxis.moveDir = MoveDirection.Left;
                ExecuteEvents.Execute(currentButton, currentAxis, ExecuteEvents.moveHandler);
                timer = timeBetweenInputs;
            }

        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;

        }
        else
        {
            timer = 0;
        }
    }//não utilizado (deixei para aprendizado)

    public void Jogar()
    {
        SceneManager.LoadScene("CharSelect");
    }

    public void Staff()
    {
        SceneManager.LoadScene("Creditos");
    }

    public void Sair()
    {

    }
}

