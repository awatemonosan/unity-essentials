using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public delegate void EachIterator(string key, object value);
public delegate T ReduceIterator<T>(T memo, string key, object value);


public class UData
{
  private Dictionary<string, bool> locks = new Dictionary<string, bool>();
  private Dictionary<string, object> children = new Dictionary<string, object>();
  private int length = 0;

  public Hashtable data {
    get
    {
      return new Hashtable(children);
    }
  }

  public UData(){}

  public UData(object value)
  {
    this.Set("value", value);
  }

  public UData(UData model)
  {
    this.Merge(model);
  }

  public static UData Load(string path)
  {
    path = Application.persistentDataPath + "/" + path;
    if (!Exists(path)) return null;

    StreamReader r = new StreamReader(path);
    string data = r.ReadToEnd();
    r.Close();
    
    return UData.Parse(data);
  }
  
  public static bool Exists(string path)
  {
    path = Application.persistentDataPath + "/" + path;
    return File.Exists(path);
  }

  public void Save(string path, UData payloa)
  {
    path = Application.persistentDataPath + "/" + path;
    StreamWriter w = new StreamWriter(path);
    w.Write(this.Serialize());
    w.Close();
  }

  public UData Merge(UData model)
  {
    model.Each(this.CopyChild);
    return this;
  }

  private void CopyChild(string key, object value)
  {
    this.Set(key, value);
  }

  public UData GetChild(int index)
  {
    return this.GetChild(index.ToString());
  }

  public UData GetChild(float key)
  {
    return this.GetChild(key.ToString());
  }

  public UData GetChild(string key)
  {
    if (!this.Has(key)) this.Set(key, new UData());
    return this.Get<UData>(key);
  }

  public T Default<T>(string key)
  {
    if (!this.Has(key)) this.Set(key, default(T));
    return this.Get<T>(key);
  }

  public UData Default<T>(string key, T value)
  {
    if(!this.Has(key)) this.Set(key, value);
    return this;
  }

  public UData Lock(string key, object value)
  {
    if(this.locks.ContainsKey(key)) return this;
    this.Set(key, value);
    return this.Lock(key);
  }

  public UData Lock(string key)
  {
    this.locks[key] = true;
    return this;
  }

  public UData Default(string key, object value)
  {
    if (!this.Has(key)) { this.Set(key, value); }
    return this;
  }

  // public UData Set(int index, object value)
  // {
  //   if (index < 0) index = this.length - index;
  //   if (index > length) length = index + 1;
  //   this.Set(index.ToString(), value);
  //   return this;
  // }

  public UData Set(object key, object value)
  {
    return this.Set(key.ToString(), value);
  }

  public UData Set(string key, object value)
  {
    if (this.locks.ContainsKey(key)) return this;

    UData payload = new UData();
    if (this.Has(key))
    {
      payload.Set("oldValue", this.children[key]);
      this.Delete(key);
    } 
    this.children[key] = value;

    if (value != null && value.GetType() == typeof(UData))
    {
      UData child = (UData)this.children[key];

      UData defaultPayload = new UData();
      defaultPayload.Set("key", key);
    }
    return this;
  }

  public bool Has(object key)
  {
    return this.Has(key.ToString());
  }

  public bool Has(string key)
  {
    return this.children.ContainsKey(key);
  }

  public UData Clone()
  {
    return new UData(this);
  }

  private void PassUp(UData payload)
  {
    string eventName = payload.Get<string>("eventName");
    string key = payload.Get<string>("key");
  }

  // public T Get<T>(int index)
  // {
  //   if (index < 0) index = this.length - index;
  //   if (index >= this.length) return default(T);
  //   return this.Get<T>(index.ToString());
  // }

  public T Get<T>(object key)
  {
    return this.Get<T>(key.ToString());
  }

  public T Get<T>(string key)
  {
    return (T)this.Get(key);
  }

  public T Get<T>()
  {
    return (T)this.Get("value");
  }

  // public object Get(int index)
  // {
  //   if (index < 0) index = this.length - index;
  //   if (index >= this.length) return null;
  //   return this.Get(index.ToString());
  // }
  
  public object Get(object key)
  {
   return this.Get(key.ToString());
  }

  public object Get(string key)
  {
    if (!this.Has(key)) { return null; }
    return this.children[key];
  }

  public T Pop<T>()
  {
    return this.Pick<T>(-1);
  }

  public UData Push(object value)
  {
    this.Insert(-1, value);
    return this;
  }

  public T Dequeue<T>()
  {
    return this.Pick<T>(0);
  }

  public UData Enqueue(object value)
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

  public UData Insert(int index, object value)
  {
    if(index < 0) index = this.length + index;
    
    for(int i = index+1; i < this.length; i++)
    {
      this.Set(i, this.Get(i-1));
    }

    this.Set(index, value);

    return this;
  }
  public UData Remove(float key)
  {
    this.Remove(key.ToString());

    return this;
  }

  public UData Remove(int index)
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

  public UData Remove(string key)
  {
    if (!this.Has(key)) return this;

    this.Delete(key);

    return this;
  }

  private void Delete(int key)
  {
    this.Delete(key.ToString());
  }

  private UData Delete(string key)
  {
    if(this.children.ContainsKey(key))
    {
      this.children.Remove(key);
    }
    return this;
  }

  public string Serialize()
  {
    return "{" + (string)this.Reduce<string>(serializeChild, "") + "}";
  }

  private string serializeChild(string serialized, string key, object value)
  {
    if (value == null) return serialized;
    if (key[0] == '_') return serialized;

    if(serialized != "") serialized += ",";
    serialized += "'" + key + "':";

    if (value.GetType() == typeof(UData))
    { serialized += ((UData)value).Serialize(); }

    else if (value.GetType() == typeof(int) 
    || value.GetType() == typeof(float)
    || value.GetType() == typeof(double))
    { serialized += value.ToString(); }

    else
    { serialized += "'" + value.ToString() + "'"; }

    return serialized;
  }


  public UData Each(EachIterator eachIterator)
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
      memo = reduceIterator(memo, kvp.Key, kvp.Value);
    }

    return memo;
  }

  public static UData Parse(string aJSON)
  {
    Stack<UData> stack = new Stack<UData>();
    UData ctx = null;
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

          stack.Push(new UData());
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
          stack.Push(new UData());
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

  static void ParseElement(UData ctx, string token, string tokenName, bool quoted)
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
