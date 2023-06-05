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
using static UnityEditor.ShaderData;

public class LoginUser : MonoBehaviour
{
    public TMP_InputField edtName, edtPassword;
    public TMP_Text txtError;
    public Selectable fisrt;
    private EventSystem eventSystem;
    public Button btnLongin;
    public static LoginResponseMoel loginResponseMoel;
    // Start is called before the first frame update
    void Start()
    {
        eventSystem= EventSystem.current;
        fisrt.Select();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Return)) {
            btnLongin.onClick.Invoke();
        }

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if(next != null)
            {
                next.Select();
            }
        }
        
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            Selectable next = eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
            if(next != null)
            {
                next.Select();
            }
        }
    }

    public void CheckLogin()
    {
        StartCoroutine(Login());
        Login();

        
    }

    IEnumerator Login()
    {
        //…
        var user = edtName.text;
        var pass = edtPassword.text;

        if (user.Equals("") || pass.Equals("") )
        {
            txtError.text = "Vui lòng nhập đủ thông tin";
        }
        else
        {
            UserModel userModel = new UserModel(user, pass);
            string jsonStringRequest = JsonConvert.SerializeObject(userModel);

            var request = new UnityWebRequest("https://hoccungminh.dinhnt.com/fpt/login", "POST");
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
                loginResponseMoel = JsonConvert.DeserializeObject<LoginResponseMoel>(jsonString);
                if (loginResponseMoel.status == 1)
                {
                   
                    SceneManager.LoadScene(1);
                    
                }
                else
                {
                    txtError.text = loginResponseMoel.notification;
                }
            }
            request.Dispose();
        }
    }

}
