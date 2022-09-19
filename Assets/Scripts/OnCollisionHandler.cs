using UnityEngine;
using UnityEngine.SceneManagement;

public class OnCollisionHandler : MonoBehaviour
{
    [SerializeField] AudioClip seccess;
    [SerializeField] AudioClip death;

    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem deathParticle;

    AudioSource audioSource;

    bool isTransition = false;
    bool collisionDisble = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        CheatKeys();   
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransition || collisionDisble) { return; }
        switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Для движения вверх нажмите пробел. Для поворота ракеты нажмите кнопки A и D");
                break;
            case "Finish":
                NextLevelAfterTime();
                break;
            default:
                ReloadLevelAfterTime();
                break;
        }
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void ReloadLevelAfterTime()
    {
        if (isTransition == false)
        {
            audioSource.PlayOneShot(death);
            deathParticle.Play();
            isTransition = true;
        }
            GetComponent<Movement>().enabled = false;
            Invoke("ReloadLevel", 1f);
        
    }

    public void NextLevelAfterTime()
    {
        if (isTransition == false)
        {
            audioSource.PlayOneShot(seccess);
            successParticle.Play();
            isTransition = true;
        }
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel", 1f);
    }

    public void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void CheatKeys()
    {
        if (Input.GetKey(KeyCode.L))
        {
            NextLevel();
        }
        else if (Input.GetKey(KeyCode.C))
        {
            collisionDisble = !collisionDisble;    
        }
    }
    
}

