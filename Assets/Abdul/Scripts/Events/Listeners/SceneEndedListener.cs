using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneEndedListener : FadeListener
{

    protected override void _FadeEnd()
    {
        base._FadeEnd();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
