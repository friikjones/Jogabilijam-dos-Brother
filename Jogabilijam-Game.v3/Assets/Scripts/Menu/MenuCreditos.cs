using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class MenuCreditos : MonoBehaviour
{
    //ATUALMENTE O CÓDIGO NÃO TEM FUNÇÃO. A IDÉIA É IMPLEMENTAR A OPÇÃO DE RETORNAR A TELA INICIAL.
    //public RawImage mask3;
    public Button BotaoVoltar;
    public string MenuInicial;
    private string nomeDaCena3;

    void Awake()//não destroi o objeto após o carregamento da próxima cena
    {
       // DontDestroyOnLoad(transform.gameObject);
    }

    void Start()
    {
        nomeDaCena3 = SceneManager.GetActiveScene().name;
        //Cursor.visible = true;
        Time.timeScale = 1;

        // =========SETAR BOTOES (Instancia os objetos de acordo com as opções desejadas==========//

        BotaoVoltar.onClick = new Button.ButtonClickedEvent();
        //BotaoVoltar2.onClick.AddListener(() => VoltarOpcoes());
       // mask3.transform.position = BotaoVoltar2.transform.position; //posiciona a máscara sobre a posição do botão créditos
    }

    void Update()
    {
       // if (SceneManager.GetActiveScene().name != nomeDaCena3)
       // {

       //     Destroy(gameObject);
       // }


       // if (Input.GetButtonDown("Submit"))
         //   BotaoVoltar2.Select();

    }

   public void Voltar()
    {
        SceneManager.LoadScene("MenuInicial");
    }
}