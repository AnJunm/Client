using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocketGameProtocol;

public class RequestManager:BaseManager
{
    public RequestManager(GameFace face) : base(face) { }

    private Dictionary<ActionCode, BaseRequest> requestDict = new Dictionary<ActionCode, BaseRequest>();

   
    public void AddRequest(BaseRequest request)
    {
        requestDict.Add(request.GetActionCode,request);

    }

    public void RemoveRequest(ActionCode action)
    {
        requestDict.Remove(action);
    }
}

