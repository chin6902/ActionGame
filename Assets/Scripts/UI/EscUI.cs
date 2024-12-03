using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EscUI : MonoBehaviour
{
    [SerializeField] private Button escButton;
    [SerializeField] private GameObject controlCanvas;


    private void Awake()
    {
        escButton.onClick.AddListener(() =>
        {
            controlCanvas.SetActive(false);
        });
    }
}
