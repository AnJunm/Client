using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketGameProtocol;
public class GameFace : MonoBehaviour
{
    private ClientManager clientManager;
    private RequestManager requestManager;
    // Start is called before the first frame update
    private static GameFace face;
    public static GameFace Face
    {
        get
        {
            if (face == null)
            {
                face=GameObject.Find("GameFace").GetComponent<GameFace>();
            }
            return face;
        }
    }
    void Awake()
    {
        clientManager = new ClientManager(this);
        requestManager = new RequestManager(this);

        clientManager.OnInit();
        requestManager.OnInit();

    }

    private void OnDestroy()
    {
        clientManager.OnDestroy();
        requestManager.OnDestroy();
    }

    public void Send(MainPack pack)
    {
        clientManager.send(pack);
    }
    // Update is called once per frame
    public void HandleResponse(MainPack Pack)
    {
        
    }


    public void AddRequest(BaseRequest request)
    {
        requestManager.AddRequest(request);
    }

    public void RemoveRequest(ActionCode action)
    {
        requestManager.RemoveRequest(action);

    }
}
