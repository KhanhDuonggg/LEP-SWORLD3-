using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Newtonsoft.Json;
using System.Text;
using UnityEngine.Networking;
using UnityEditor.PackageManager.Requests;

public class RegisterUser : MonoBehaviour
{
    public TMP_InputField edtName, edtPassword, edtRePass;
    public TMP_Text txtError;
    public Selectable fisrt;
    private EventSystem eventSystem;
    public Button btnRegister;
    public GameObject login, register;
    // Start is called before the first frame update
    void Start()
    {
        eventSystem = EventSystem.current;
        fisrt.Select();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            btnRegister.onClick.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (next != null)
            {
                next.Select();
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Selectable next = eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
            if (next != null)
            {
                next.Select();
            }
        }
    }

    public void CheckRegister()
    {
        StartCoroutine(Register());
        Register();


    }

    IEnumerator Register()
    {
        //…
        var user = edtName.text;
        var pass = edtPassword.text;
        var repass = edtRePass.text;

        if(user.Equals("") || pass.Equals("") || repass.Equals(""))
        {
            txtError.text = "Vui lòng nhập đủ thông tin";
        }else if(pass == repass)
        {
            UserModel userModel = new UserModel(user, pass);
            string jsonStringRequest = JsonConvert.SerializeObject(userModel);

            var request = new UnityWebRequest("https://hoccungminh.dinhnt.com/fpt/register", "POST");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStringRequest);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
            }
            else
            {
                var jsonString = request.downloadHandler.text.ToString();
                RegisterResponseMoel registerResponseMoel = JsonConvert.DeserializeObject<RegisterResponseMoel>(jsonString);
                if (registerResponseMoel.status == 1)
                {
                    register.SetActive(false);
                    login.SetActive(true);
                }
                else
                {
                    txtError.text = registerResponseMoel.notification;
                }
            }
            request.Dispose();
        }
        else
        {
            txtError.text = "Mật khẩu chưa đúng";
        }
    }

}
