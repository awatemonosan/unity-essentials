using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public delegate void EachIterator(string key, object value);
public delegate T ReduceIterator<T>(T memo, string key, object value);

public class UModel {
  public Dispatcher _dispatcher = new Dispatcher();
  private Dictionary<string, bool> locks = new Dictionary<string, bool>();
  private Dictionary<string, object> children = new Dictionary<string, object>();
  private Dictionary<string, DispatcherListener> dispatcherListeners = new Dictionary<string, DispatcherListener>();
  private int length = 0;

  public Hashtable data {
    get
    {
      return new Hashtable(children);
    }
  }

  public Dispatcher dispatcher {
    get
    {
      return this._dispatcher;
    }
  }

  public UModel(){}

  public UModel(UModel model)
  {
    this.Merge(model);
  }

  public static UModel Load(string path)
  {
    path = Application.persistentDataPath + "/" + path;
    if (!Exists(path)) return null;

    StreamReader r = new StreamReader(path);
    string data = r.ReadToEnd();
    r.Close();
    
    return UModel.Parse(data);
  }
  
  public static bool Exists(string path)
  {
    path = Application.persistentDataPath + "/" + path;
    return File.Exists(path);
  }

  public void Save(string path, UModel payloa)
  {
    path = Application.persistentDataPath + "/" + path;
    StreamWriter w = new StreamWriter(path);
    w.Write(this.Serialize());
    w.Close();
  }

  public UModel Merge(UModel model)
  {
    model.Each(this.CopyChild);
    return this;
  }

  private void CopyChild(string key, object value) {
    this.Set(key, value);
  }

  public UModel GetChild(int index)
  {
    return this.GetChild(index.ToString());
  }

  public UModel GetChild(string key)
  {
    if (!this.Has(key)) this.Set(key, new UModel());
    return this.Get<UModel>(key);
  }

  public T Ensure<T>(string key)
  {
    if (!this.Has(key)) this.Set(key, default(T));
    return this.Get<T>(key);
  }

  public UModel Ensure<T>(string key, T value)
  {
    if(!this.Has(key)) this.Set(key, value);
    return this;
  }

  public UModel Lock(string key, object value)
  {
    if(this.locks.ContainsKey(key)) return this;
    this.Set(key, value);
    return this.Lock(key);
  }

  public UModel Lock(string key)
  {
    this.locks[key] = true;
    return this;
  }

  public UModel Set(int index, object value)
  {
    if (index < 0) index = this.length - index;
    if (index > length) length = index + 1;
    this.Set(index.ToString(), value);
    return this;
  }

  public UModel Set(string key, object value)
  {
    if (this.locks.ContainsKey(key)) return this;

    UModel payload = new UModel();
    if (this.Has(key))
    {
      payload.Set("oldValue", this.children[key]);
      this.Delete(key);
    } 
    this.children[key] = value;
    // this.dispatcher.Trigger(key + ".changed", payload);

    if (value != null && value.GetType() == typeof(UModel))
    {
      UModel child = (UModel)this.children[key];

      UModel defaultPayload = new UModel();
      defaultPayload.Set("key", key);

      dispatcherListeners[key] = child.dispatcher.On("all", this.PassUp, defaultPayload);
    }
    return this;
  }

  public bool Has(int index) {
    return this.Has(index.ToString());
  }

  public bool Has(string key) {
    return this.children.ContainsKey(key);
  }

  public UModel Clone() {
    return new UModel(this);
  }

  private bool PassUp(UModel payload) {
    string eventName = payload.Get<string>("eventName");
    string key = payload.Get<string>("key");
    // return this.dispatcher.Trigger(key + "." + eventName);
    return true;
  }

  public object Get(int index)
  {
    if (index < 0) index = this.length - index;
    if (index >= this.length) return null;
    return this.Get(index.ToString());
  }
  public object Get(string key)
  {
    if (!this.Has(key)) return null;
    return this.children[key];
  }

  public T Get<T>(int index)
  {
    if (index < 0) index = this.length - index;
    if (index >= this.length) return default(T);
    return this.Get<T>(index.ToString());
  }
  public T Get<T>(string key)
  {
    if (!this.Has(key)) return default(T);
    return (T)this.children[key];
  }

  public T Pop<T>()
  {
    return this.Pick<T>(-1);
  }

  public UModel Push(object value)
  {
    this.Insert(-1, value);
    return this;
  }

  public T Dequeue<T>()
  {
    return this.Pick<T>(0);
  }

  public UModel Enqueue(object value)
  {
    this.Insert(0, value);
    return this;
  }

  public T Pick<T>(int index)
  {
    if(index < 0) index = this.length + index;
    T value = this.Get<T>(index);
    this.Remove(index);
    return value;
  }

  public UModel Insert(int index, object value)
  {
    if(index < 0) index = this.length + index;
    
    for(int i = index+1; i < this.length; i++)
    {
      this.Set(i, this.Get(i-1));
    }

    this.Set(index, value);

    return this;
  }

  public UModel Remove(int index)
  {
    if(index < 0) index = this.length + index;

    if (!this.Has(index)) return this;

    this.Remove(index.ToString());
    
    for(int i=index; i < this.length; i++)
    {
      this.Set(i, this.Get(i+1));
    }
    this.Delete(this.length - 1);
    this.length--;

    return this;
  }

  public UModel Remove(string key)
  {
    if (!this.Has(key)) return this;

    this.Delete(key);

    // this.dispatcher.Trigger(key + ".removed");

    return this;
  }

  private void Delete(int key)
  {
    this.Delete(key.ToString());
  }

  private UModel Delete(string key)
  {
    if(this.children.ContainsKey(key))
    {
      this.children.Remove(key);
      if(dispatcherListeners.ContainsKey(key)) 
      {
        dispatcherListeners[key].Release();
        dispatcherListeners.Remove(key);
      }
    }
    return this;
  }

  public string Serialize()
  {
    return "{" + (string)this.Reduce(serializeChild, "") + "}";
  }

  private string serializeChild(string serialized, string key, object value) {
    if (value == null) return serialized;
    if (key[0] == '_') return serialized;

    if(serialized != "") serialized += ",";
    serialized += "'" + key + "':";

    if (value.GetType() == typeof(UModel))
    { serialized += ((UModel)value).Serialize(); }

    else if (value.GetType() == typeof(int) 
    || value.GetType() == typeof(float)
    || value.GetType() == typeof(double))
    { serialized += value.ToString(); }

    else
    { serialized += "'" + value.ToString() + "'"; }

    return serialized;
  }


  public UModel Each(EachIterator eachIterator)
  {
    foreach( KeyValuePair<string, object> kvp in this.children )
    {
      eachIterator(kvp.Key, kvp.Value);
    }

    return this;
  }

  public T Reduce<T>(ReduceIterator <T>reduceIterator, T memo)
  {
    foreach( KeyValuePair<string, object> kvp in this.children )
    {
      memo = reduceIterator<T>(memo, kvp.Key, kvp.Value);
    }

    return memo;
  }

  public static UModel Parse(string aJSON)
  {
    Stack<UModel> stack = new Stack<UModel>();
    UModel ctx = null;
    int i = 0;
    StringBuilder token = new StringBuilder();
    string tokenName = "";
    bool quoteMode = false;
    bool tokenIsQuoted = false;
    while (i < aJSON.Length)
    {
      switch (aJSON[i])
      {
        case '[':
          if (quoteMode)
          {
            token.Append(aJSON[i]);
            break;
          }

          stack.Push(new UModel());
          if (ctx != null)
          {
            ctx.Set(tokenName, stack.Peek());
          }
          tokenName = "";
          token.Length = 0;
          ctx = stack.Peek();
          break;

        case '{':
          if (quoteMode)
          {
            token.Append(aJSON[i]);
            break;
          }
          stack.Push(new UModel());
          if (ctx != null)
          {
            ctx.Set(tokenName, stack.Peek());
          }
          tokenName = "";
          token.Length = 0;
          ctx = stack.Peek();
          break;


        case ']':
        case '}':
          if (quoteMode)
          {

            token.Append(aJSON[i]);
            break;
          }
          if (stack.Count == 0)
            return null; //throw new Exception("JSON Parse: Too many closing brackets");

          stack.Pop();
          if (token.Length > 0)
          {
            ParseElement(ctx, token.ToString(), tokenName, tokenIsQuoted);
            tokenIsQuoted = false;
          }
          tokenName = "";
          token.Length = 0;
          if (stack.Count > 0)
            ctx = stack.Peek();
          break;

        case ':':
          if (quoteMode)
          {
            token.Append(aJSON[i]);
            break;
          }
          tokenName = token.ToString().Trim();
          token.Length = 0;
          tokenIsQuoted = false;
          break;

        case '"':
          quoteMode ^= true;
          tokenIsQuoted |= quoteMode;
          break;

        case ',':
          if (quoteMode)
          {
            token.Append(aJSON[i]);
            break;
          }
          if (token.Length > 0)
          {
            ParseElement(ctx, token.ToString(), tokenName, tokenIsQuoted);
            tokenIsQuoted = false;
          }
          tokenName = "";
          token.Length = 0;
          tokenIsQuoted = false;
          break;

        case '\r':
        case '\n':
          break;

        case ' ':
        case '\t':
          if (quoteMode)
            token.Append(aJSON[i]);
          break;

        case '\\':
          ++i;
          if (quoteMode)
          {
            char C = aJSON[i];
            switch (C)
            {
              case 't':
                token.Append('\t');
                break;
              case 'r':
                token.Append('\r');
                break;
              case 'n':
                token.Append('\n');
                break;
              case 'b':
                token.Append('\b');
                break;
              case 'f':
                token.Append('\f');
                break;
              case 'u':
                {
                  string s = aJSON.Substring(i + 1, 4);
                  token.Append((char)int.Parse(
                    s,
                    System.Globalization.NumberStyles.AllowHexSpecifier));
                  i += 4;
                  break;
                }
              default:
                token.Append(C);
                break;
            }
          }
          break;

        default:
          token.Append(aJSON[i]);
          break;
      }
      ++i;
    }
    if (quoteMode)
    {
      return null; //throw new Exception("JSON Parse: Quotation marks seems to be messed up.");
    }
    return ctx;
  }

  static void ParseElement(UModel ctx, string token, string tokenName, bool quoted)
  {
    if (quoted)
    {
      ctx.Set(tokenName, token);
      return;
    }
    string tmp = token.ToLower();
    //check if bool
    if (tmp == "false" || tmp == "true")
      ctx.Set(tokenName, tmp == "true");
    //check if Vector3
    //check if null
    else if (tmp == "null")
      ctx.Remove(tokenName);
    else
    {
      double val;
      if (double.TryParse(token, out val))
        ctx.Set(tokenName, val);
      else
        ctx.Set(tokenName, token);
    }
  }
}
