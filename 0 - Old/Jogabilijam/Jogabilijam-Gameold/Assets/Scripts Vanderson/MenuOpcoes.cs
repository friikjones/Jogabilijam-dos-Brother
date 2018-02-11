using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class MenuOpcoes : MonoBehaviour
{
    //copiado do código do Alexandre
    public RawImage mask2;
    //private Vector3 positMask;
    // private Canvas canvas;
    // private CharSelectDTOScript DTO;
    private int selecao2;//seleciona a caixa que sera usada
                         //private int SelecaoOpcao;//seleciona a opção no menu de opções.



    //variáveis nativas que peguei do tutorial
    public Button BotaoControles;
    //public Toggle CaixaModoJanela;
    public Button BotaoVoltar, BotaoCreditos;
    public string nomeCenaControle = "Controle";
    public string nomeCenaCreditos = "Creditos";//carregar a cena das opções
    public string nomeCenaMenu= "Menu";
    private string nomeDaCena;
    //private int modoJanelaAtivo;
   // private bool telaCheiaAtivada;

    void Awake()//não destroi o objeto após o carregamento da próxima cena
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    void Start()
    {
        //Opcoes(false);
        nomeDaCena = SceneManager.GetActiveScene().name;
        Cursor.visible = true;
        Time.timeScale = 1;

        //=============MODO JANELA===========//
       

        // =========SETAR BOTOES (Instancia os objetos de acordo com as opções desejadas==========//
        
        BotaoVoltar.onClick = new Button.ButtonClickedEvent();
        BotaoCreditos.onClick = new Button.ButtonClickedEvent();
        //BotaoSalvar.onClick = new Button.ButtonClickedEvent();
        BotaoControles.onClick = new Button.ButtonClickedEvent();
        BotaoVoltar.onClick.AddListener(() => Voltar());
        BotaoCreditos.onClick.AddListener(() => Creditos());
        BotaoControles.onClick.AddListener(() => Controle());
       // BotaoSalvar.onClick.AddListener(() => SalvarPreferencias());
        mask2.transform.position = BotaoControles.transform.position; //posiciona a máscara sobre a posição do botão créditos
    }
    //=========VOIDS DE CHECAGEM==========//

    //=========VOIDS DE SALVAMENTO==========//
    //private void SalvarPreferencias()
    // {
    //if (CaixaModoJanela.isOn == true)
    //{
    //modoJanelaAtivo = 1;
    //telaCheiaAtivada = false;
    //}
    //     else
    //        {
    //modoJanelaAtivo = 0;
    //telaCheiaAtivada = true;
    //}

    //    PlayerPrefs.SetInt("modoJanela", modoJanelaAtivo);

    //}

    //===========VOIDS NORMAIS=========//
    void Update()
    {
        if (SceneManager.GetActiveScene().name != nomeDaCena)
        {

            Destroy(gameObject);
        }

        //código copiado do Alexandre

        //os if's são para a primeira tela de seleção


        if (Input.GetButtonDown("Cima"))
            selecao2 -= 1;
        if (Input.GetButtonDown("Baixo"))
            selecao2 += 1;
        while (selecao2 < 0 || selecao2 > 2)
        {
            if (selecao2 < 0)
                selecao2 += 3;
            if (selecao2 > 2)
                selecao2 -= 3;
        }

        moveMask();

        if (Input.GetButtonDown("Submit"))
            escolheOpcao();

    }


    public void escolheOpcao()
    {

        switch (selecao2) //o critério de defnição é o mesmo da caixa de seleção (Switch/case não aceita vetor para comparação)
        {
            case 0:
                BotaoControles.Select();
                break;

            case 1:
                BotaoCreditos.Select();
                break;

            case 2:
                BotaoVoltar.Select();
                break;

        }


    }


    public void moveMask()//copiado do Alexandre e adaptado, esta funcional. Move a caixa de seleção na primeira tela.
    {
        Vector3 aux = new Vector3();
        switch (selecao2)
        {
            case 0:
                aux = BotaoControles.transform.position; ;
                break;
            case 1:
                aux = BotaoCreditos.transform.position; ;
                break;
            case 2:
                aux = BotaoVoltar.transform.position; ;
                break;

        }
        mask2.transform.position = aux;

    }




    private void Creditos()//chama a tela de seleção de personagem, esta funcional
    {
        SceneManager.LoadScene(nomeCenaCreditos);
    }

    private void Controle()
    {
        SceneManager.LoadScene(nomeCenaControle);
    }


    private void Voltar()//deve encerrar a aplicação, ainda nada acontece
    {
        SceneManager.LoadScene(nomeCenaMenu);
    }
}