using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour {
	void Start () {
        Text IPText = GameObject.Find("IPText").GetComponent<Text>();
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList) {
            if (ip.AddressFamily == AddressFamily.InterNetwork) {
                IPText.text = "IP: " + ip.ToString();
            }
        }
    }
}
