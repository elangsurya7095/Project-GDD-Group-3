using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public GameObject popup;
    public bool aktif;

    void Start()
    {
        // ...
    }

    void OnMouseDown()
    {
        popup.SetActive(aktif); // Aktifkan pop-up
    }

    void Update()
    {
        // ...
    }
}
