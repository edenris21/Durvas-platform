using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// classe usada para gerenciar o jogo
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Instancia do Singleton
    
    [SerializeField] 
    private string guiName; // Nome da fase de interface

    [SerializeField]
    private string levelName; // Nome da fase de jogo 

    [SerializeField] 
    private GameObject playerAndCameraPrefab; // referencia pro prefab do jogador + camera

    private void Awake()
    {
        // Condição de criação do singleton
        if (Instance == null)
        {
            Instance = this;
            
            // Impede que o objeto indicado entre parênteses seja destruído
            DontDestroyOnLoad(this.gameObject); // referência para o objeto que contém o gamemanager
        }
        else Destroy(this.gameObject);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // Se estiver iniciando a cena a partir de Initialization, carregue o jogo do jeito de antes
        if (SceneManager.GetActiveScene().name == "Inicialization")
            StartGameFromInitialization();
        else // Caso contrario, esta iniciando a partir do level, carregue o jogo do modo apropriado
            StartGameFromLevel();
    }

    private void StartGameFromLevel()
    {
        //1 - Carrega a cena de interface de modo aditivo para nao apagar a cena do level da memoria RAM
        SceneManager.LoadScene(guiName, LoadSceneMode.Additive);
        
        // 2 - Precisa instanciar o jogador na cena
        // Começa procurando o objeto PlayerStart na cena do level
        Vector3 playerStartPosition = GameObject.Find("PlayerStart").transform.position;

        // Instancia o prefab do jogador na posição do player start com rotação zerada
        Instantiate(playerAndCameraPrefab, playerStartPosition, Quaternion.identity);
        
    }
    public void StartGame()
    {
        // 1 - Carregar a cena de interface e do jogo
        SceneManager.LoadScene(guiName);
        //SceneManager.LoadScene(levelName, LoadSceneMode.Additive);

        SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive).completed += operation =>
        {
            Scene levelScene = default;
            // encontrar a cena de level que está carregada
            // for que intera no array de cenas abertas
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                // se o nome da cena na posição i do array for igual ao nome do level
                if (SceneManager.GetSceneAt(i).name == levelName)
                {
                    // associe a cena na posição i do array à variável
                    levelScene = SceneManager.GetSceneAt(i);
                    break;
                }
            }

            // se a variável tiver um valor diferente do padrão, significa que ela foi alterada 
            // e a cena do level atual foi encontrada no array, entao faça ela ser a
            // nova cena ativa
            if (levelScene != default) SceneManager.SetActiveScene(levelScene);

            // 2 - Precisa instanciar o jogador na cena
            // Começa procurando o objeto PlayerStart na cena do level
            Vector3 playerStartPosition = GameObject.Find("PlayerStart").transform.position;

            // Instancia o prefab do jogador na posição do player start com rotação zerada
            Instantiate(playerAndCameraPrefab, playerStartPosition, Quaternion.identity);
        };
    }

    private void StartGameFromInitialization()
    {
        SceneManager.LoadScene("Splash");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
