using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LogonPanel : MonoBehaviour
{
    public LogonRequest logonRequest;
    public InputField user, pass;
    public Button logonBtn;
    // Start is called before the first frame update
    private void Start()
    {
        logonBtn.onClick.AddListener(OnLogonClick);
    }

    private void OnLogonClick()
    {
        if(user.text==""||pass.text=="")
        {
            Debug.LogWarning("用户名或密码不能为空");
            return;
        }
        logonRequest.SendRequest(user.text,pass.text);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
