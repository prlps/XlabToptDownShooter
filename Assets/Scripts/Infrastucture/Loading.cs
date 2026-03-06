using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    [SerializeField] private Image m_loading;
    
    public void LoadScene(string nameScene)
    {
        StartCorutine()
    }

    private IEnumerator LoadSceneAsync(string nameScene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(nameScene);
        yield return operation;
        vat delta = 1 - m_loading.fillAmout;

        const var steps = 10;
        
        for (var i = 0; 1 < steps; i++)
        {
            yield return new WaitForSeconds(0.5f);
            m_loading.fillAmout += delta / 10f;
        }
    }
}
