using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class MenuCreditos : MonoBehaviour
{

    public RawImage mask3;
    public Button BotaoVoltar2;
    public string nomeCenaOpcoes = "Opcoes";
    private string nomeDaCena3;

    void Awake()//não destroi o objeto após o carregamento da próxima cena
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    void Start()
    {
        //Opcoes(false);
        nomeDaCena3 = SceneManager.GetActiveScene().name;
        Cursor.visible = true;
        Time.timeScale = 1;

        //=============MODO JANELA===========//


        // =========SETAR BOTOES (Instancia os objetos de acordo com as opções desejadas==========//

        BotaoVoltar2.onClick = new Button.ButtonClickedEvent();
        BotaoVoltar2.onClick.AddListener(() => VoltarOpcoes());
        mask3.transform.position = BotaoVoltar2.transform.position; //posiciona a máscara sobre a posição do botão créditos
    }

    //===========VOIDS NORMAIS=========//
    void Update()
    {
        if (SceneManager.GetActiveScene().name != nomeDaCena3)
        {

            Destroy(gameObject);
        }


        if (Input.GetButtonDown("Submit"))
            BotaoVoltar2.Select();

    }

    private void VoltarOpcoes()
    {
        SceneManager.LoadScene(nomeCenaOpcoes);
    }
}