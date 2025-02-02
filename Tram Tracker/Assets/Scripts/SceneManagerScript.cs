using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public void LoadScene(string UniversityMapScene)
    {
        Debug.Log($"{UniversityMapScene}");
        SceneManager.LoadScene(UniversityMapScene);
    }

}
