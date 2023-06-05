using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool nen;
    private Animator animation;
    private bool isPaues, isThong;
    public GameObject paues, dongCua, thong, win;
    public bool nhaydoi;
    public GameObject qua_Thong, la, la1, la2;
    private bool isRight;
    private int qua_thong, isLive;
    public TMP_Text qua_thongText, txtError, lose;
    public ParticleSystem psBui;
    public TMP_InputField edtoldpass, edtnewpass, edtrepass;
    private float timeSpawn;
    private float times;

    // dem thoi gian
    private int time; // thoi gian choi game tinh bang s
    public TMP_Text timeText; // hien thi thoi gian choi
    private bool isAlive; // kiem tra nhan vat con tuong tac
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animation = GetComponent<Animator>();
        isPaues = true;
        isRight = true;
        isLive = 3;
        isThong = true;
        timeSpawn = 4;
        times = timeSpawn;
        //// set position
        if (LoginUser.loginResponseMoel.positionX != "")
        {
            float x = float.Parse(LoginUser.loginResponseMoel.positionX);
            float y = float.Parse(LoginUser.loginResponseMoel.positionY);
            float z = float.Parse(LoginUser.loginResponseMoel.positionZ);
            transform.position = new Vector3(x, y, z);
        }

        // time
        isAlive = true;
        time = 0;
        timeText.text = time + "s";
        StartCoroutine(UpdateTime());


    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotation = psBui.transform.localRotation;
        if (Input.GetKey(KeyCode.RightArrow)) 
        { 
            transform.Translate(Vector3.right * 5f * Time.deltaTime);
            transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            animation.SetBool("run", true);
            isRight= true;
            psBui.Play();
            rotation.y = 180;
            psBui.transform.localRotation = rotation;
        }else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * 5f * Time.deltaTime);
            transform.localScale = new Vector3(-1.2f, 1.2f, 1.2f);
            animation.SetBool("run", true);
            isRight= false;
            psBui.Play();
            rotation.y = 0;
            psBui.transform.localRotation = rotation;
        }
        else
        {
            animation.SetBool("run", false);
        }

        if(nen  && !Input.GetKey(KeyCode.Space)) {
            nhaydoi= false;
            
        }

        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(nen || nhaydoi)
            {
               // animation.SetTrigger("isJump");
                rb.AddForce(new Vector2(0, 290));
                nen = false;
               nhaydoi= !nhaydoi;
            }
            
        }

        if (nen)
        {
            animation.SetBool("nhay", false);
        }
        else
        {
            animation.SetBool("nhay", true);
            animation.SetBool("run", false);
        }

        if (Input.GetKeyDown(KeyCode.P)) 
        {
            Pause();
           
        }

        if(qua_thong > 0)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                GameObject quaThong = Instantiate(qua_Thong);
                quaThong.transform.localScale = new Vector3(1, 1, 1);
                quaThong.transform.position = new Vector3(
                    transform.position.x + (isRight ? 0.4f : -0.4f),
                    transform.position.y,
                    transform.position.z);
                quaThong.GetComponent<Choi_Thong>().Setspeed(isRight ? 10 : -10);
                qua_thong--;
                qua_thongText.text = qua_thong + "  x";
            }
        }

        // so lan choi
        if(isLive == 3)
        {
            la.SetActive(true);
            la1.SetActive(true);
            la2.SetActive(true);
        }else if(isLive == 2)
        {
            la.SetActive(true);
            la1.SetActive(true);
            la2.SetActive(false);
        }
        else if(isLive==1)
        {
            la.SetActive(true);
            la1.SetActive(false);
            la2.SetActive(false);
        }
        else
        {
            la.SetActive(false);
            la1.SetActive(false);
            la2.SetActive(false);
            gameObject.SetActive(false);
            win.SetActive(true);
            lose.text = "you lose";
        }

        times -= Time.deltaTime;
        if (times <= 0)
        {
            times = timeSpawn;

            if(isThong == false)
            {
                GameObject thongg = Instantiate(thong);
                thongg.transform.position = new Vector2(74.76f, 1.089286f);
                isThong = true;
            }
           
        }
    }

    // time
    // cap nhat thoi gian 1s chay 1 lan
    IEnumerator UpdateTime()
    {
        while (isAlive)
        {
            time++;
            timeText.text = time + "s";
            yield return new WaitForSeconds(1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("nen"))
        {
            //Vector2 direction = collision.GetContact(0).normal;        
                nen = true;    
        }

        if (collision.gameObject.CompareTag("quai") )
        {
            var direction = collision.GetContact(0).normal;
            if(direction.x == 1 || direction.x == -1)
            {
                isLive--;         
                StartCoroutine(playerDie());
            }
        }

        if (collision.gameObject.CompareTag("la"))
        {
            Destroy(collision.gameObject);
            if (isLive == 3)
            {
                return;
            }
            isLive++;
            
        }

        if(collision.gameObject.CompareTag("gai") || collision.gameObject.CompareTag("dung_nham"))
        {
            Time.timeScale = 0;
           win.SetActive(true);
            lose.text = "you lose";
        }
    }

    IEnumerator playerDie()
    {
        int i = 10;
        while (true)
        { 
            animation.SetTrigger("die");
            if(i < 0)
            {
                break;               
            }
            i--;
            yield return null;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("qua_thong"))
        {
            qua_thong++;
            qua_thongText.text = qua_thong + "  x";
            Destroy(collision.gameObject);
            isThong = false;
        }

        if(collision.gameObject.CompareTag("savePosition"))
        {
            SavePosition();
            SaveScoer();
        }

        if (collision.gameObject.CompareTag("load"))
        {
            StartCoroutine(SavePositionAPIload());
            SavePositionAPIload();
        }

        if(collision.gameObject.CompareTag("fire"))
        {
            isLive--;
            StartCoroutine(playerDie());
        }

        if (collision.gameObject.CompareTag("dongcua"))
        {
            dongCua.SetActive(true);
        }
     
    }

  
    
    public void Pause()
    {
        if(isPaues)
        {
            Time.timeScale = 0;
            isPaues= false;
            paues.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            isPaues = true;
            paues.SetActive(false);
        }
    }

    public void SaveScoer()
    {
        StartCoroutine(Save());
        Save();
    }

    // API lưu score

    IEnumerator Save()
    {
        var user = LoginUser.loginResponseMoel.username;
        var countCoin = CoinText.countCoin;
            ScoreModel score = new ScoreModel(user, countCoin);
            string jsonStringRequest = JsonConvert.SerializeObject(score);

            var request = new UnityWebRequest("https://hoccungminh.dinhnt.com/fpt/save-score", "POST");
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
                Debug.Log("save core");
                }
                else
                {
                Debug.Log(">>>>Loi");
                }
            }
            request.Dispose();
        
    }

    public void SavePosition()
    {
        StartCoroutine(SavePositionAPI());
        SavePositionAPI();
    }

    // Api luu position
    IEnumerator SavePositionAPIload()
    {
        var user = LoginUser.loginResponseMoel.username;
        var x = 0;
        var y = 0;
        var z = 0;
        PositionModel posistionModel = new PositionModel(user, x.ToString(),y.ToString(),z.ToString());
        string jsonStringRequest = JsonConvert.SerializeObject(posistionModel);

        var request = new UnityWebRequest("https://hoccungminh.dinhnt.com/fpt/save-position", "POST");
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
                Debug.Log("posistion");
            }
            else
            {
                Debug.Log("loi"+ scoreResponseMoel.status);
            }
        }
        request.Dispose();

    }  IEnumerator SavePositionAPI()
    {
        var user = LoginUser.loginResponseMoel.username;
        var x = transform.position.x;
        var y = transform.position.y;
        var z = transform.position.z;
        PositionModel posistionModel = new PositionModel(user, x.ToString(),y.ToString(),z.ToString());
        string jsonStringRequest = JsonConvert.SerializeObject(posistionModel);

        var request = new UnityWebRequest("https://hoccungminh.dinhnt.com/fpt/save-position", "POST");
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
                Debug.Log("posistion");
            }
            else
            {
                Debug.Log("loi"+ scoreResponseMoel.status);
            }
        }
        request.Dispose();

    }

    public void changePassword()
    {
        StartCoroutine(changePasswordAPI());
        changePasswordAPI();
    }
    // changepassword
    IEnumerator changePasswordAPI()
    {
        var user = LoginUser.loginResponseMoel.username;
        var newpass = edtnewpass.text;
        var repass = edtrepass.text;
        var oldpass = edtoldpass.text;
       
       if(newpass.Equals(repass))
        {
            ChangePasswordModel changePasswordModel = new ChangePasswordModel(user, oldpass, newpass);
            string jsonStringRequest = JsonConvert.SerializeObject(changePasswordModel);

            var request = new UnityWebRequest("https://hoccungminh.dinhnt.com/fpt/change-password", "POST");
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
                    SceneManager.LoadScene(0);
                    Time.timeScale = 1;
                }
                else
                {
                    Debug.Log("loi" + scoreResponseMoel.status);
                }
            }
            request.Dispose();
        }
        else
        {
            txtError.text = "Mật khầu không khớp";
        }

    }
}
