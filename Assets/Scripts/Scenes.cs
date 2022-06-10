using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class Scenes : MonoBehaviour
{
    public Animator fondo;
    public GameObject canvasS;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async void StartGame() {
        canvasS.SetActive(false);
        fondo.SetInteger("Inicio", 1);
        await Task.Delay(2900);
        SceneManager.LoadScene("Tutorial");
        Destroy(this.gameObject);
    }
    public async void CloseGame() {
        canvasS.SetActive(false);
        fondo.SetInteger("Inicio", 1);
        await Task.Delay(2900);
        Application.Quit();
        Destroy(this.gameObject);
    }
    public async void Creditsscreen()
    {
        canvasS.SetActive(false);
        fondo.SetInteger("Inicio", 1);
        await Task.Delay(2900);
        SceneManager.LoadScene(4);
        Destroy(this.gameObject);
    }
}
