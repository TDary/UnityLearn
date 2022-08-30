using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

internal class ChangeCanvas : MonoBehaviour
{
    public Canvas changedCanva;
    public Canvas currentCanva;
    public Button btn;
    public Button btn2;
    private Canvas currentc;
    private Canvas changec;
    private void Start()
    {
        currentc = currentCanva.gameObject.GetComponent<Canvas>();
        changec = changedCanva.gameObject.GetComponent<Canvas>();
        btn.onClick.AddListener(change);
        btn2.onClick.AddListener(change2);
    }
    public void change()
    {
        if (currentCanva.gameObject.activeInHierarchy)
        {
            currentCanva.gameObject.SetActive(false);
            changedCanva.gameObject.SetActive(true);
        }
        else
        {
            currentCanva.gameObject.SetActive(true);
            changedCanva.gameObject.SetActive(false);
        }
    }

    public void change2()
    {
        if (currentc.enabled)
        {
            currentc.enabled = false;
            changec.enabled = true;
        }
        else
        {
            currentc.enabled = true;
            changec.enabled = false;
        }
    }

    private void Update()
    {
        
    }
}