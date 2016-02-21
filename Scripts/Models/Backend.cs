using UnityEngine;
 
using System;
using System.Collections;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class Backend : Dispatcher {
  private UdpClient client;
  private IPEndPoint endpoint;

  public Backend(string ipAddress, int port) {
    // endpoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
    this.EnableTimedTriggers();

    endpoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
    client = new UdpClient();
    client.Connect(endpoint);
    client.BeginReceive(new AsyncCallback(OnMessageRecieved), null);
  }
  ~Backend(){
    client.Close();
  }

  private void OnMessageRecieved(IAsyncResult ar) {
    Byte[] receiveBytes  = client.EndReceive(ar, ref endpoint);
    string receiveString = Encoding.ASCII.GetString(receiveBytes);

    Debug.Log("I GOT " + receiveString);

    Hashtable payload    = receiveString.Parse();
    payload["source_ip"]   = endpoint.Address.ToString();
    payload["source_port"] = endpoint.Port;

    this.TriggerIn(payload, 0);
    client.BeginReceive(new AsyncCallback(OnMessageRecieved), null);
  }

  public void Send(string evnt) {
    this.Send(evnt, new Hashtable( ));
  }
  public void Send(string evnt, Hashtable payload) {
    payload["event"] = evnt;
    this.Send(payload);
  }
  public void Send(Hashtable payload) {
    this.SendRaw(payload);
  }

  public void SendRaw(Hashtable payload) {
    string message = JSON.Serialize(payload);
    SendRaw(message);
  }
  public void SendRaw(string message) {
    byte[] data = Encoding.UTF8.GetBytes(message);
    SendRaw(data);
  }
  public void SendRaw(byte[] data){
    client.Send(data, data.Length);
  }
}