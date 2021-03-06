using UnityEngine;
 
using System;
using System.Collections;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class NetworkInterface : Dispatcher {
  private UdpClient client;
  private IPEndPoint endpoint;

  public NetworkInterface(string ipAddress, int port) {
    // endpoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
    // this.EnableTimedTriggers();

    endpoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
    client = new UdpClient();
    client.Connect(endpoint);
    client.BeginReceive(new AsyncCallback(OnMessageRecieved), null);
  }

  ~NetworkInterface(){
    client.Close();
  }

  private void OnMessageRecieved(IAsyncResult ar) {
    Byte[] receiveBytes  = client.EndReceive(ar, ref endpoint);
    string receiveString = Encoding.ASCII.GetString(receiveBytes);

    UModel payload = UModel.Parse(receiveString);
    payload.Set("source_ip", endpoint.Address.ToString());
    payload.Set("source_port", endpoint.Port);

    this.Trigger(payload);
    client.BeginReceive(new AsyncCallback(OnMessageRecieved), null);
  }

  public void Send(string eventName) {
    this.Send(eventName, new UModel( ));
  }
  public void Send(string eventName, UModel payload) {
    payload.Set("event", eventName);
    this.Send(payload);
  }
  public void Send(UModel payload) {
    this.SendRaw(payload);
  }

  public void SendRaw(UModel payload) {
    string json = payload.Serialize();
    SendRaw(json);
  }

  public void SendRaw(string message) {
    byte[] data = Encoding.UTF8.GetBytes(message);
    SendRaw(data);
  }

  public void SendRaw(byte[] data){
    client.Send(data, data.Length);
  }
}
