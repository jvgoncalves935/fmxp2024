using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuPrincipalManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private ScenesData scenesData;
    [SerializeField] private AudioManager audioManager;

    [Header("Botões Menu")]
    [SerializeField] private GameObject menuPrincipal;
    [SerializeField] private GameObject menuOpcoes;
    [SerializeField] private Button botaoJogar;
    [SerializeField] private Button botaoCreditos;
    [SerializeField] private Button botaoOpcoes;
    [SerializeField] private Button botaoSair;
    [SerializeField] private Button botaoVoltar;
    [SerializeField] private Button botaoMenor;
    [SerializeField] private Button botaoMaior;
    [SerializeField] private TMP_Text textoVersao;
    [SerializeField] private TMP_Text textLinguagens;
    [SerializeField] private TMP_Text textLinguagensSelect;

    private string[] linguagensSiglas;
    private string linguagemAtual;
    private int linguagemAtualIndice;
    private int numLinguagens;
    private Dictionary<int, string> linguagens;
    private SaveData saveData;

    private Dictionary<string, string> stringsOpcoes;
    // Start is called before the first frame update
    void Start()
    {
        IniciarListenersBotoes();
        ToggleMenuOpcoes(false);
        ToggleMenuPrincipal(true);
        DestravarCursor();
        VerificarSceneLoaderInstanciado();
        IniciarStrings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DesfocarMouse() {
    #if UNITY_EDITOR || UNITY_STANDALONE_WIN
        MouseOperations.DestravarCursorMultiPlat();
    #endif
    }

    public void MusicaInicio() {
        AudioManager.InstanciaAudioManager.Play("R.E. (full)");
    }

    public void VerificarSceneLoaderInstanciado() {
        if(FindObjectOfType<SceneLoader>() == null) {
            Instantiate(sceneLoader);
            Instantiate(audioManager);
            Instantiate(scenesData);
            //MusicaInicio();
            //DontDestroyOnLoad(sceneLoader);
            //Debug.Log("SceneData criado em EventHorizon");
        } else {
            //Debug.Log("SceneData anteriormente criado");
        }
    }

    private void OnButtonJogarClick()
    {
        CarregarCena();
    }

    private void OnButtonCreditosClick()
    {
        CarregarCreditos();
    }

    private void OnButtonOpcoesClick() {
        ToggleMenuOpcoes(true);
        ToggleMenuPrincipal(false);
    }

    private void OnButtonSairClick()
    {
        FecharJogo();
    }

    private void OnButtonVoltarClick() {
        FecharMenuOpcoes();
        ToggleMenuOpcoes(false);
        ToggleMenuPrincipal(true);
    }

    private void OnButtonMaiorOpcoesClick() {
        AtualizarLinguagem(1);
    }

    private void OnButtonMenorOpcoesClick() {
        AtualizarLinguagem(-1);
    }

    private void CarregarCreditos() {
        SceneManager.LoadScene("Scenes/Creditos", LoadSceneMode.Single);
    }

    private void CarregarCena() {
        //TravarCursor();
        SceneLoader.InstanciaSceneLoader.SetStopMusicOnLoading(false);
        SceneLoader.InstanciaSceneLoader.SetProximaCena("Petsfarm");
        //Debug.Log(SceneLoader.InstanciaSceneLoader.GetProximaCena());
        GerenciadorCena.CarregarCena("Loading");
    }

    private void FecharJogo()
    {
        Application.Quit();
    }

    private void IniciarListenersBotoes()
    {
        botaoJogar.onClick.AddListener(OnButtonJogarClick);
        botaoOpcoes.onClick.AddListener(OnButtonOpcoesClick);
        botaoCreditos.onClick.AddListener(OnButtonCreditosClick);
        botaoSair.onClick.AddListener(OnButtonSairClick);
        botaoVoltar.onClick.AddListener(OnButtonVoltarClick);

        botaoMaior.onClick.AddListener(OnButtonMaiorOpcoesClick);
        botaoMenor.onClick.AddListener(OnButtonMenorOpcoesClick);
    }

    private void CarregarStrings() {
        stringsOpcoes = LocalizationSystem.GetDicionarioStringsCena(GerenciadorCena.NomeCenaAtual());
    }

    private void CarregarStringsLinguagens() {
        Dictionary<string, string> stringsLinguagens = LocalizationSystem.GetDicionarioStringsCenaCommon(GerenciadorCena.NomeCenaAtual() + "Common");
        foreach(KeyValuePair<string, string> entrada in stringsLinguagens) {
            //Debug.Log(entrada.Key + " " + entrada.Value);
            stringsOpcoes.Add(entrada.Key, entrada.Value);
        }
    }

    private void IniciarStringsLinguagens() {
        textLinguagens.text = stringsOpcoes["OPCOES_LINGUAGEM"];
        textLinguagensSelect.text = stringsOpcoes["OPCOES_LINGUAGEM_" + linguagensSiglas[linguagemAtualIndice].ToUpper()];
        //Debug.Log(linguagensSiglas[linguagemAtualIndice].ToUpper());

        botaoVoltar.transform.GetChild(0).GetComponent<TMP_Text>().text = stringsOpcoes["MENU_VOLTAR"];
        botaoJogar.transform.GetChild(0).GetComponent<TMP_Text>().text = stringsOpcoes["MENU_INICIAR"];
        botaoOpcoes.transform.GetChild(0).GetComponent<TMP_Text>().text = stringsOpcoes["MENU_OPCOES"];
        botaoCreditos.transform.GetChild(0).GetComponent<TMP_Text>().text = stringsOpcoes["MENU_CREDITOS"];
        botaoSair.transform.GetChild(0).GetComponent<TMP_Text>().text = stringsOpcoes["MENU_SAIR"];
    }

    private void ToggleMenuPrincipal(bool flag) {
        menuPrincipal.SetActive(flag);
        ResetarBotaoSelecionado();
    }

    private void ToggleMenuOpcoes(bool flag) {
        menuOpcoes.SetActive(flag);
        ResetarBotaoSelecionado();
    }

    private void AtivarMenuPrincipal() {
        ToggleMenuPrincipal(true);
        ToggleMenuOpcoes(false);
    }

    private void AtivarMenuOpcoes() {
        ToggleMenuPrincipal(false);
        ToggleMenuOpcoes(true);
    }

    public void AtualizarLinguagem(int add) {
        int novaPosicao = AtualizarPosicaoLinguagem(add);
        TrocarLinguagem(novaPosicao);
        AlterarLinguagemSave();
    }

    private int AtualizarPosicaoLinguagem(int add) {
        int novaPosicao = linguagemAtualIndice + add;
        int posicaoFinal;
        if(novaPosicao == -1) {
            posicaoFinal = numLinguagens - 1;
        } else {
            if(novaPosicao == numLinguagens) {
                posicaoFinal = 0;
            } else {
                posicaoFinal = novaPosicao;
            }
        }

        return posicaoFinal;
    }

    private void CarregarLinguagemAtual() {
        linguagens = LocalizationSystem.DicionarioLinguagens();
        linguagemAtual = LocalizationSystem.LinguagemAtual();
        numLinguagens = linguagens.Count;

        linguagensSiglas = new string[numLinguagens];
        foreach(KeyValuePair<int, string> kvp in linguagens) {
            if(kvp.Value == linguagemAtual) {
                linguagemAtualIndice = kvp.Key;
            }
            linguagensSiglas[kvp.Key] = kvp.Value;
        }
    }

    private void TrocarLinguagem(int pos) {
        linguagemAtualIndice = pos;
        
        textLinguagensSelect.text = stringsOpcoes["OPCOES_LINGUAGEM_" + linguagensSiglas[linguagemAtualIndice].ToUpper()];
        linguagemAtual = linguagensSiglas[linguagemAtualIndice];
    }

    public void CarregarSaveOpcoes() {
        saveData = SaveSystem.CarregarData();
        //Debug.Log(saveData.Linguagem);
    }

    private void AlterarLinguagemSave() {
        //saveData.Linguagem = linguagemAtual;
        AlterarLinguagemSaveWeb();
    }

    private void AlterarLinguagemSaveWeb() {
        scenesData.AddScenesData("language", linguagemAtual);
    }

    private void SalvarSaveOpcoes() {
        LocalizationSystem.SetLinguagem(linguagemAtual);
        //SaveUIController.InstanciaSaveUIController.ReiniciarDicionario(linguagemAtual);
        SaveSystem.SalvarAlteraçõesSave(saveData, false);
    }

    public void FecharMenuOpcoes() {
        //SalvarSaveOpcoes();
        IniciarStrings();

        //Debug.Log(linguagemAtual);
    }

    private void ResetarBotaoSelecionado() {
        BotaoController.InstanciaBotaoController.DeselecionarBotaoMouseEnter();
    }

    private void IniciarStrings() {
        //CarregarSaveOpcoes();
        CarregarStrings();
        CarregarStringsLinguagens();
        CarregarLinguagemAtual();
        IniciarStringsLinguagens();
        textoVersao.text = "v" + Application.version;
    }


    public static void TravarCursor() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public static void DestravarCursor() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


}
