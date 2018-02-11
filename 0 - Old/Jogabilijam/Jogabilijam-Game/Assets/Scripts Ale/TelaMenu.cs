using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class TelaMenu : MonoBehaviour
{
    //copiado do código do Alexandre
    public RawImage mask;
    //private Vector3 positMask;
    // private Canvas canvas;
    // private CharSelectDTOScript DTO;
    private int selecao;//seleciona a caixa que sera usada
    private int SelecaoOpcao;//seleciona a opção no menu de opções.
                   


    //variáveis nativas que peguei do tutorial
    public Button BotaoJogar, BotaoOpcoes, BotaoSair;
    public Toggle CaixaModoJanela;
    public Button BotaoVoltar, BotaoSalvarPref, BotaoCreditos;
    public string nomeCenaJogo = "CharSelect";
    private string nomeDaCena;
    private int modoJanelaAtivo;
    private bool telaCheiaAtivada;

    void Awake()//não destrou o objeto após o carregamento da próxima cena
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    void Start()
    {
        Opcoes(false);
        nomeDaCena = SceneManager.GetActiveScene().name;
        Cursor.visible = true;
        Time.timeScale = 1;
      
        //=============MODO JANELA===========//
        if (PlayerPrefs.HasKey("modoJanela"))
        {
            modoJanelaAtivo = PlayerPrefs.GetInt("modoJanela");
            if (modoJanelaAtivo == 1)
            {
                Screen.fullScreen = false;
                CaixaModoJanela.isOn = true;
            }
            else
            {
                Screen.fullScreen = true;
                CaixaModoJanela.isOn = false;
            }
        }
        else
        {
            modoJanelaAtivo = 0;
            PlayerPrefs.SetInt("modoJanela", modoJanelaAtivo);
            CaixaModoJanela.isOn = false;
            Screen.fullScreen = true;
        }
        //========RESOLUCOES========//
        if (modoJanelaAtivo == 1)
        {
            telaCheiaAtivada = false;
        }
        else
        {
            telaCheiaAtivada = true;
        }
        //}

        // =========SETAR BOTOES (Instancia os objetos de acordo com as opções desejadas==========//
        BotaoJogar.onClick = new Button.ButtonClickedEvent();
        BotaoOpcoes.onClick = new Button.ButtonClickedEvent();
        BotaoSair.onClick = new Button.ButtonClickedEvent();
        BotaoVoltar.onClick = new Button.ButtonClickedEvent();
        BotaoCreditos.onClick = new Button.ButtonClickedEvent();
        BotaoSalvarPref.onClick = new Button.ButtonClickedEvent();
        BotaoJogar.onClick.AddListener(() => Jogar());
        BotaoOpcoes.onClick.AddListener(() => Opcoes(true));
        BotaoSair.onClick.AddListener(() => Sair());
        BotaoVoltar.onClick.AddListener(() => Opcoes(false));
        BotaoSalvarPref.onClick.AddListener(() => SalvarPreferencias());
        mask.transform.position = BotaoJogar.transform.position; //posiciona a máscara sobre a posição do botão jogar
    }
    //=========VOIDS DE CHECAGEM==========//
    private void Opcoes(bool ativarOP)
    {
        BotaoJogar.gameObject.SetActive(!ativarOP);
        BotaoOpcoes.gameObject.SetActive(!ativarOP);
        BotaoSair.gameObject.SetActive(!ativarOP);
        CaixaModoJanela.gameObject.SetActive(ativarOP);
        BotaoVoltar.gameObject.SetActive(ativarOP);
        BotaoCreditos.gameObject.SetActive(ativarOP);
        BotaoSalvarPref.gameObject.SetActive(ativarOP);
    }
    //=========VOIDS DE SALVAMENTO==========//
    private void SalvarPreferencias()
    {
        if (CaixaModoJanela.isOn == true)
        {
            modoJanelaAtivo = 1;
            telaCheiaAtivada = false;
        }
        else
        {
            modoJanelaAtivo = 0;
            telaCheiaAtivada = true;
        }
       
        PlayerPrefs.SetInt("modoJanela", modoJanelaAtivo);
       
    }
    
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

        //o if abaixo cria uma condição dupla de verificação:se a caixa de seleção esta no local correto e se o botão foi pressionado.
        if (Input.GetButtonDown("Submit") && mask.transform.position == BotaoJogar.transform.position)
            BotaoJogar.Select();
		if (Input.GetButtonDown ("Submit") && mask.transform.position == BotaoOpcoes.transform.position) {
			BotaoOpcoes.Select ();
			//rotina para a tela de opções.

			//os if's são para a tela de opção

			if (Input.GetButtonDown ("Direita"))
				SelecaoOpcao += 1;
			if (Input.GetButtonDown ("Esquerda"))
				SelecaoOpcao -= 1;
			if (Input.GetButtonDown ("Cima"))
				SelecaoOpcao -= 2;
			if (Input.GetButtonDown ("Baixo"))
				SelecaoOpcao += 2;
			while (SelecaoOpcao < 0 || SelecaoOpcao > 3) {
				if (SelecaoOpcao < 0)
					SelecaoOpcao += 4;
				if (SelecaoOpcao > 3)
					SelecaoOpcao -= 4;
			}

			moveMask1 ();

			if (Input.GetButtonDown ("Submit")) {
				if (mask.transform.position == BotaoCreditos.transform.position)
					BotaoCreditos.Select ();
				if (mask.transform.position == CaixaModoJanela.transform.position)
					CaixaModoJanela.Select ();
				if(mask.transform.position == BotaoSalvarPref.transform.position)
					BotaoSalvarPref.Select ();
				if(mask.transform.position == BotaoVoltar.transform.position)
					BotaoVoltar.Select ();
				if (selecao == 2)
					BotaoSair.Select ();
			}
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


    public void moveMask1()//copiado do Alexandre e adaptado, esta funcional. Move a caixa de seleção na tela de opções
    {
        Vector3 aux1 = new Vector3();
        switch (SelecaoOpcao)
        {
            case 0:
                aux1 = BotaoCreditos.transform.position; ;
                break;
            case 1:
                aux1 = CaixaModoJanela.transform.position;
                break;
            case 2:
                aux1 = BotaoSalvarPref.transform.position; ;
                break;
            case 3:
                aux1 = BotaoVoltar.transform.position; ;
                break;


        }
        mask.transform.position = aux1;

    }


    private void Jogar()//chama a tela de seleção de personagem, esta funcional
    {
        SceneManager.LoadScene(nomeCenaJogo);
    }
    private void Sair()//deve encerrar a aplicação, ainda nada acontece
    {
        Application.Quit();
    }
}