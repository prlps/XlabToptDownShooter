using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField] private Image m_loading;

    private static Loading m_instance;

    private void Awake()
    {
        if (m_instance != null)
        {
            Destroy(m_instance.gameObject);
            m_instance = null;
        }

        m_instance = this;
        gameObject.SetActive(false);
        DontDestroyOnLoad(this);
        Infrastucture.ServiceLocator.Register(this);
    }

    public void LoadScene(string nameScene)
    {
        if (string.IsNullOrWhiteSpace(nameScene))
        {
            return;
        }

        gameObject.SetActive(true);
        StartCoroutine(LoadSceneAsync(nameScene));
    }

    private IEnumerator LoadSceneAsync(string nameScene)
    {
        m_loading.fillAmount = 0f;
        var operation = SceneManager.LoadSceneAsync(nameScene, LoadSceneMode.Single);
        if (operation == null)
        {
            gameObject.SetActive(false);
            yield break;
        }

        operation.allowSceneActivation = false;

        while (operation.progress < 0.9f)
        {
            m_loading.fillAmount = Mathf.Clamp01(operation.progress / 0.9f);
            yield return null;
        }

        m_loading.fillAmount = 1f;
        operation.allowSceneActivation = true;
        yield return operation;
        yield return new WaitForEndOfFrame();

        m_loading.fillAmount = 1f;
        gameObject.SetActive(false);
    }
}
