using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class MenuInicial : MonoBehaviour
{
    //copiado do código do Alexandre
    public RawImage mask;
    // private Canvas canvas;
    // private CharSelectDTOScript DTO;
    private int selecao;//seleciona a caixa que sera usada
                   
    public Button BotaoJogar, BotaoOpcoes, BotaoSair;
    public string nomeCenaJogo = "CharSelect";
    public string nomeCenaOpcoes = "Opcoes";//carregar a cena das opções
    private string nomeDaCena;
    //private int modoJanelaAtivo;
    //private bool telaCheiaAtivada;

    void Awake()//não destroi o objeto após o carregamento da próxima cena
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    void Start()
    {
        nomeDaCena = SceneManager.GetActiveScene().name;
        Cursor.visible = true;
        Time.timeScale = 1;

        //=============MODO JANELA===========//
       

        // =========SETAR BOTOES (Instancia os objetos de acordo com as opções desejadas==========//
        BotaoJogar.onClick = new Button.ButtonClickedEvent();
        BotaoOpcoes.onClick = new Button.ButtonClickedEvent();
        BotaoSair.onClick = new Button.ButtonClickedEvent();
        BotaoJogar.onClick.AddListener(() => Jogar());
        BotaoOpcoes.onClick.AddListener(() => Opcoes());
        BotaoSair.onClick.AddListener(() => Sair());
       // mask.transform.position = BotaoJogar.transform.position; //posiciona a máscara sobre a posição do botão jogar
    }
    //=========VOIDS DE CHECAGEM==========//
    
    //=========VOIDS DE SALVAMENTO==========//
   
    //===========VOIDS NORMAIS=========//
    void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name != nomeDaCena)
        {

            Destroy(gameObject);
        }

        //código copiado do Alexandre

        //os if's são para a primeira tela de seleção

        if (Input.GetButtonDown("Cima"))
            selecao -= 1;
        if (Input.GetButtonDown("Baixo"))
            selecao += 1;
        while (selecao < 0 || selecao > 2)
        {
            if (selecao < 0)
                selecao += 3;
            if (selecao > 2)
                selecao -= 3;
        }

        moveMask();

        if (Input.GetButtonDown("Submit"))
            selecionaOpcao();

    }


    public void selecionaOpcao()
    {

        switch (selecao) //o critério de defnição é o mesmo da caixa de seleção (Switch/case não aceita vetor para comparação)
        {
            case 0:
                BotaoJogar.Select();
                break;

            case 1:
                BotaoOpcoes.Select();
                break;

            case 2:
                BotaoSair.Select();
                break;

        }


    }
    

    public void moveMask()//copiado do Alexandre e adaptado, esta funcional. Move a caixa de seleção na primeira tela.
    {
        Vector3 aux = new Vector3();
        switch (selecao)
        {
            case 0:
                aux = BotaoJogar.transform.position; ;
                break;
            case 1:
                aux = BotaoOpcoes.transform.position; ;
                break;
            case 2:
                aux = BotaoSair.transform.position; ;
                break;

        }
        mask.transform.position = aux;

    }

    


    private void Jogar()//chama a tela de seleção de personagem, esta funcional
    {
        SceneManager.LoadScene(nomeCenaJogo);
    }

    private void Opcoes() //chama a cena de opções.
    {
        SceneManager.LoadScene(nomeCenaOpcoes);
    }


    private void Sair()//deve encerrar a aplicação, ainda nada acontece
    {
        Application.Quit();
    }
}