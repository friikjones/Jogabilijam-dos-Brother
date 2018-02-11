using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class Menu : MonoBehaviour
{
    //copiado do código do Alexandre
    public RawImage mask;
    //private Vector3 positMask;
   // private Canvas canvas;
   // private CharSelectDTOScript DTO;
    private int selecao;//seleciona a caixa que sera usada
   // private int currentCharIndex;


    //variáveis nativas que peguei do tutorial
    public Button BotaoJogar, BotaoOpcoes, BotaoSair;
    [Space(20)]
    //public Slider BarraVolume;
    public Toggle CaixaModoJanela;
    //public Dropdown Resolucoes, Qualidades;
    public Button BotaoVoltar, BotaoSalvarPref, BotaoCreditos;
    [Space(20)]
    //public Text textoVol;
    public string nomeCenaJogo = "CharSelect";
    private string nomeDaCena;
    //private float VOLUME;
    private int modoJanelaAtivo;
    //qualidadeGrafica,, resolucaoSalveIndex; (retirado da linha de cima)
    private bool telaCheiaAtivada;
   // private Resolution[] resolucoesSuportadas;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        // resolucoesSuportadas = Screen.resolutions;
    }

    void Start()
    {
        Opcoes(false);
        // ChecarResolucoes();
        // AjustarQualidades();
        //
        // if (PlayerPrefs.HasKey("Resolucao"))
        // {
        // int numResoluc = PlayerPrefs.GetInt("Resolucao");
        // if (resolucoesSuportadas.Length <= numResoluc)
        // {
        //PlayerPrefs.DeleteKey("Resolucao");
        //}
        //}
        //
        nomeDaCena = SceneManager.GetActiveScene().name;
        Cursor.visible = true;
        Time.timeScale = 1;
        //
        //BarraVolume.minValue = 0;
        //BarraVolume.maxValue = 1;

        //=============== SAVES===========//
        // if (PlayerPrefs.HasKey("Volume"))
        //{
        //VOLUME = PlayerPrefs.GetFloat("Volume");
        //BarraVolume.value = VOLUME;
        // }
        //else
        //{
        //PlayerPrefs.SetFloat("Volume", 1);
        // BarraVolume.value = 1;
        //}
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
        //if (PlayerPrefs.HasKey("Resolucao"))
        //{
        //resolucaoSalveIndex = PlayerPrefs.GetInt("Resolucao");
        // Screen.SetResolution(resolucoesSuportadas[resolucaoSalveIndex].width, resolucoesSuportadas[resolucaoSalveIndex].height, telaCheiaAtivada);
        //Resolucoes.value = resolucaoSalveIndex;
        //}
        //       else
        //{
        //     resolucaoSalveIndex = (resolucoesSuportadas.Length - 1);
        //Screen.SetResolution(resolucoesSuportadas[resolucaoSalveIndex].width, resolucoesSuportadas[resolucaoSalveIndex].height, telaCheiaAtivada);
        //PlayerPrefs.SetInt("Resolucao", resolucaoSalveIndex);
        //  Resolucoes.value = resolucaoSalveIndex;
        // }
        //=========QUALIDADES=========//
        //if (PlayerPrefs.HasKey("qualidadeGrafica"))
        // {
        //qualidadeGrafica = PlayerPrefs.GetInt("qualidadeGrafica");
        // QualitySettings.SetQualityLevel(qualidadeGrafica);
        // Qualidades.value = qualidadeGrafica;
        // }
        //else
        // {
        // QualitySettings.SetQualityLevel((QualitySettings.names.Length - 1));
        //qualidadeGrafica = (QualitySettings.names.Length - 1);
        //PlayerPrefs.SetInt("qualidadeGrafica", qualidadeGrafica);
        //Qualidades.value = qualidadeGrafica;
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
    // private void ChecarResolucoes()
    //{
    // Resolution[] resolucoesSuportadas = Screen.resolutions;
    // Resolucoes.options.Clear();
    //    for (int y = 0; y < resolucoesSuportadas.Length; y++)
    //    {
    // Resolucoes.options.Add(new Dropdown.OptionData() { text = resolucoesSuportadas[y].width + "x" + resolucoesSuportadas[y].height });
    //}
  //  Resolucoes.captionText.text = "Resolucao";
   // }
    //private void AjustarQualidades()
    // {
    //string[] nomes = QualitySettings.names;
    //  Qualidades.options.Clear();
    //  for (int y = 0; y < nomes.Length; y++)
    //  {
    //Qualidades.options.Add(new Dropdown.OptionData() { text = nomes[y] });
    //}
    //Qualidades.captionText.text = "Qualidade";
    //}
    private void Opcoes(bool ativarOP)
    {
        BotaoJogar.gameObject.SetActive(!ativarOP);
        BotaoOpcoes.gameObject.SetActive(!ativarOP);
        BotaoSair.gameObject.SetActive(!ativarOP);
        //
        //textoVol.gameObject.SetActive(ativarOP);
        //BarraVolume.gameObject.SetActive(ativarOP);
        CaixaModoJanela.gameObject.SetActive(ativarOP);
        //Resolucoes.gameObject.SetActive(ativarOP);
//Qualidades.gameObject.SetActive(ativarOP);
        BotaoVoltar.gameObject.SetActive(ativarOP);
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
        //PlayerPrefs.SetFloat("Volume", BarraVolume.value);
        //PlayerPrefs.SetInt("qualidadeGrafica", Qualidades.value);
        PlayerPrefs.SetInt("modoJanela", modoJanelaAtivo);
        //PlayerPrefs.SetInt("Resolucao", Resolucoes.value);
        //resolucaoSalveIndex = Resolucoes.value;
        //AplicarPreferencias();
    }
    // private void AplicarPreferencias()
    // {
    //VOLUME = PlayerPrefs.GetFloat("Volume");
    // QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("qualidadeGrafica"));
    //Screen.SetResolution(resolucoesSuportadas[resolucaoSalveIndex].width, resolucoesSuportadas[resolucaoSalveIndex].height, telaCheiaAtivada);
    //}
    //===========VOIDS NORMAIS=========//
    void Update()
    {
        if (SceneManager.GetActiveScene().name != nomeDaCena)
        {
            //AudioListener.volume = VOLUME;
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

  
        if (Input.GetButtonDown("Confirma") || selecao == 0)
            BotaoJogar.Select();
        if (Input.GetButtonDown("Confirma") || selecao == 1)
        {
            
            //rotina para a tela de opções.
            BotaoOpcoes.Select();
            //os if's são para a tela de opção

            if (Input.GetButtonDown("Direita"))
                selecao += 1;
            if (Input.GetButtonDown("Esquerda"))
                selecao -= 1;
            if (Input.GetButtonDown("Cima"))
                selecao -= 2;
            if (Input.GetButtonDown("Baixo"))
                selecao += 2;
            while (selecao < 0 || selecao > 3)
            {
                if (selecao < 0)
                    selecao += 4;
                if (selecao > 3)
                    selecao -= 4;
            }

            moveMask1();

            if (Input.GetButtonDown("Confirma") || selecao == 0)
                BotaoCreditos.Select();
            if (Input.GetButtonDown("Confirma") || selecao == 1)
                CaixaModoJanela.Select();
            if (Input.GetButtonDown("Confirma") || selecao == 2)
                BotaoSalvarPref.Select();
            if (Input.GetButtonDown("Confirma") || selecao == 3)
                BotaoVoltar.Select();
        }



        if (Input.GetButtonDown("Confirma")||selecao==2)
            BotaoSair.Select();
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
        Vector3 aux = new Vector3();
        switch (selecao)
        {
            case 0:
                aux = BotaoCreditos.transform.position; ;
                break;
            case 1:
                aux = CaixaModoJanela.transform.position;
                break;
            case 2:
                aux = BotaoSalvarPref.transform.position; ;
                break;
            case 3:
                aux = BotaoVoltar.transform.position; ;
                break;
            

        }
        mask.transform.position = aux;

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