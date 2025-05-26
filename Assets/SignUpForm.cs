using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using TMPro;
using System.Text.RegularExpressions;

[Serializable]
public class SignUpData
{
    public string email;
    public string password;
    public string createDate;

    public SignUpData(string email, string password)
    {
        this.email = email;
        this.password = password;
        this.createDate = DateTime.Now.ToString();
    }
}

public class SignUpForm : MonoBehaviour
{
    public TMP_InputField emailField;
    public TMP_InputField passwordField;
    public TMP_InputField confirmPasswordField;
    public Button submit;

    private void Start()
    {
        submit.onClick.AddListener(OnSubmit);
    }

    private void OnSubmit()
    {
        string email = emailField.text;
        string pass = passwordField.text;
        string cpass = confirmPasswordField.text;

        if (!ValidateEmail(email))
        {
            return;
        }

        if (!ValidatePass(pass))
        {
            return;
        }

        if (pass != cpass)
        {
            Debug.LogWarning("Password do not match!");
            return;
        }

        SignUpData singUp = new SignUpData(email, pass);
        string json = JsonUtility.ToJson(singUp);
        Debug.Log("Json = " + json);

    }

    private bool ValidateEmail(string email)
    {
        email = email.ToLower();
        string pattern = @"^[^@\s]+@[^@\s]+\.(com|id)$";

        if (!Regex.IsMatch(email, pattern))
        {
            Debug.LogWarning("Please use valid email!");
            return false;
        }
        
        return true;
    }

    private bool ValidatePass(string pass)
    {
        if(pass.Length < 8)
        {
            Debug.LogWarning("Password must be at least 8 characters long!");
            return false;
        }

        bool hasLower = Regex.IsMatch(pass, "[a-z]");
        bool hasUpper = Regex.IsMatch(pass, "[A-Z]");
        bool hasDigit = Regex.IsMatch(pass, @"\d");
        bool hasSymbol = Regex.IsMatch(pass, @"[^a-zA-Z0-9]");

        if (!hasLower)
        {
            Debug.LogWarning("Password must contain at least one lowercase!");
            return false;
        }
        if (!hasUpper)
        {
            Debug.LogWarning("Password must contain at least one uppercase!");
            return false;
        }
        if (!hasDigit)
        {
            Debug.LogWarning("Password must contain at least one number!");
            return false;
        }
        if (!hasSymbol)
        {
            Debug.LogWarning("Password must contain at least one symbol!");
            return false;
        }

        return true;
    }

}
