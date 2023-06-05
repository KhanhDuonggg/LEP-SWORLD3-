using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class ForgotPassword : MonoBehaviour
{
    public TMP_InputField edtUser, edtOTP, edtNewPass, edtRePass;
    public TMP_Text txterror;
    public GameObject forgotPassword, resetPassword, login;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendOTP()
    {
        StartCoroutine(SendOTPAPI());
        SendOTPAPI();
    }

    IEnumerator SendOTPAPI()
    {
        var user = edtUser.text;
  
            OTPModel score = new OTPModel(user);
            string jsonStringRequest = JsonConvert.SerializeObject(score);

            var request = new UnityWebRequest("https://hoccungminh.dinhnt.com/fpt/send-otp", "POST");
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
                ScoreResponseModel scoreResponseMoel = JsonConvert.DeserializeObject<ScoreResponseModel>(jsonString);
                if (scoreResponseMoel.status == 1)
                {
                    forgotPassword.SetActive(false);
                    resetPassword.SetActive(true);
                }
                else
                {
                    txterror.text = scoreResponseMoel.notification;
                }
            }
            request.Dispose();
    }

    public void ResetPassword()
    {
        StartCoroutine(ResetPasswordAPI());
        ResetPasswordAPI();
    }

    IEnumerator ResetPasswordAPI()
    {
        var user = edtUser.text;
        var edtnewPass = edtNewPass.text;
        var edtrePass = edtRePass.text;
        int otp = int.Parse(edtOTP.text);

        if(edtnewPass.Equals(edtrePass))
        {
            ResetPasswordModel score = new ResetPasswordModel(user, otp, edtnewPass);
            string jsonStringRequest = JsonConvert.SerializeObject(score);

            var request = new UnityWebRequest("https://hoccungminh.dinhnt.com/fpt/reset-password", "POST");
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
                ScoreResponseModel scoreResponseMoel = JsonConvert.DeserializeObject<ScoreResponseModel>(jsonString);
                if (scoreResponseMoel.status == 1)
                {
                    login.SetActive(true);
                    resetPassword.SetActive(false);
                }
                else
                {
                    txterror.text = scoreResponseMoel.notification;
                }
            }
            request.Dispose();
        }
        else
        {
            txterror.text = "Mật khẩu không trùng!";
        }

    }
}
