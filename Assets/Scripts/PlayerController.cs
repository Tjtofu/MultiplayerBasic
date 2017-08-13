using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Linq;

public class PlayerController : NetworkBehaviour {
    float speed = 5f;
    Text inputText;

    public override void OnStartClient() {
        List<GameObject> orderedObjects = GameObject.FindGameObjectsWithTag("NetworkPlayer").ToList().OrderBy(x => x.GetComponent<PlayerController>().netId.Value).ToList();
        for (var x = 0; x < orderedObjects.Count(); x++) {
            orderedObjects[x].name = "Player" + x;
        }
    }

    void Start() {
        inputText = GameObject.Find("MessageInputBoxText").GetComponent<Text>();
        GameObject.Find("NameText").GetComponent<Text>().text = this.name;
    }
    
    void Update () {
        if (isLocalPlayer) {
            transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Time.deltaTime * speed;
            if (Input.GetKeyDown(KeyCode.Return)) {
                print(inputText.text);
                SendMessage(this.name, inputText.text);
                inputText.text = "";
            }
        }
    }

    public void SendMessage(string name, string message) {
        if (isServer) {
            RpcSendMessage(name, message);
        } else {
            CmdSendMessage(name, message);
        }
    }

    [Command]
    void CmdSendMessage(string name, string message) {
        RpcSendMessage(name, message);
    }

    [ClientRpc]
    void RpcSendMessage(string name, string message) {
        GameObject.Find("MessageOutput").GetComponent<Text>().text += name + ": " + message + "\n";
    }
}
