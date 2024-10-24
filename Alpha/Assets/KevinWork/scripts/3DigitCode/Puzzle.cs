using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class Puzzle : MonoBehaviour
{
    int firNum = 0;
    int secNum = 0;
    int thiNum = 0;

    public TextMeshProUGUI firDigit;
    public TextMeshProUGUI secDigit;
    public TextMeshProUGUI thiDigit;


    private void Update()
    {
        firDigit.text = firNum.ToString();
        secDigit.text = secNum.ToString();
        thiDigit.text = thiNum.ToString();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void AddFir() { firNum++; }
    public void AddSec() { secNum++; }
    public void AddThi() { thiNum++; }
    public void RemoveFir() { firNum--; }
    public void RemoveSec() { secNum--; }
    public void RemoveThi() { thiNum--; }

    public void Submit()
    {
        if (firNum == 3 && secNum == 4 && thiNum == 8)
        {
            Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");

        }
    }
}
