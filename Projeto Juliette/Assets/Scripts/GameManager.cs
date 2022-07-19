using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// classe usada para gerenciar o jogo
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] 
    private string guiName; // Nome da fase de interface

    [SerializeField]
    private string levelName; // Nome da fase de jogo 

    [SerializeField] 
    private GameObject playerAndCameraPrefab; // referencia pro prefab do jogador + camera
    
    // Start is called before the first frame update
    void Start()
    {
        // Impede que o objeto indicado entre parênteses seja destruído
        DontDestroyOnLoad(this.gameObject); // referência para o objeto que contém o gamemanager
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

}
