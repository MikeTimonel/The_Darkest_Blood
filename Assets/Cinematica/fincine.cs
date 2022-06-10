using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
public class fincine : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        finalcine();
    }
    public async void finalcine()
    {
        await Task.Delay(47000);
        SceneManager.LoadScene(1);
    }
}
