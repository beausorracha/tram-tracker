using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public void LoadScene(string UniversityMapScene)
    {
        SceneManager.LoadScene(UniversityMapScene);
    }

}
