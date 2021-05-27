using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SocketGameProtocol;
public class BaseRequest : MonoBehaviour 
{
    protected RequestCode requestCode;
    protected ActionCode actionCode;
    protected GameFace face;
    public ActionCode GetActionCode
    {
        get
        {
            return actionCode;
        }
    }
    public virtual void Awake()
    {
   
    }

    public virtual void Start()
    {
        Debug.Log(actionCode.ToString());
        face = GameFace.Face;
        face.AddRequest(this);
    }

    public virtual void onDestroy()
    {
        face.RemoveRequest(actionCode);
    }
    public virtual void OnResponse(MainPack pack)
    {

    }
    public virtual void SendRequest(MainPack pack)
    {
        face.Send(pack);
    }
}