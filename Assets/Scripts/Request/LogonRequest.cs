using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SocketGameProtocol;
public class LogonRequest:BaseRequest
{
    public override void Awake()
    {
        requestCode = RequestCode.User;
        actionCode = ActionCode.Logon;
        base.Awake();
        
    }

    public override void OnResponse(MainPack pack)
    {
        switch(pack.Returncode)
        {
            case ReturnCode.Succeed:
                Debug.Log("注册成功");
                break;
            case ReturnCode.Fail:
                Debug.LogWarning("注册失败");
                break;
            case ReturnCode.ReturnNone:
                break;
        }
   
    }

    public void SendRequest(string user,string pass)
    {
        MainPack pack = new MainPack();
        pack.Requestcode = requestCode;
        pack.Actioncode = actionCode;
        LoginPack loginPack = new LoginPack();
        loginPack.Username = user;
        loginPack.Password = pass;
        pack.Loginpack = loginPack;
        base.SendRequest(pack);
    }


}