using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

using Ukulele;

namespace Ukulele
{
    public delegate void EachIterator(string key, object value);
    public delegate T ReduceIterator<T>(T memo, object key, object value);


    public class DataView
    {
        public Hashtable data; // best not to directly modify the data directly, but doesn't hurt anything if you do.
        public Dispatcher dispatcher;
    // Constructors
        public DataView()
        {
            this.data = new Hashtable();
            this.dispatcher = new Dispatcher();
        }

        public DataView(Hashtable data)
        {
            this.data = data;
        }

    // Public Methods
        public object Get(string key)
        {
            List<string> path = new List<string>(key.Split('.'));

            if(path.Count==1)
            {
                return this.data.Get(key);
            }
            else
            {
                string finalKey = path.Pop();
                string pathKey = path.Join(".");
                return this.Find(pathKey).Get(finalKey);
            }
        }

        public T Get<T>(string key)
        {
            return (T)this.Get(key);
        }
        
        public object Get(object key)
        {
            return this.Get(key.ToString());
        }

        public T Get<T>(object key)
        {
            return this.Get<T>(key.ToString());
        }

        public void Set(string key, object value)
        {
            List<string> path = new List<string>(key.Split('.'));
            if(path.Count==1)
            {
                this.data.Set(key, value);
            }
            else
            {
                string finalKey = path.Pop();
                string pathKey = path.Join(".");
                this.Find(pathKey).Set(finalKey, value);
            }

            string eventName = key + ".changed";
            this.dispatcher.Trigger(eventName, value);
        }
        
        public void Set(object key, object value)
        {
            this.Set(key.ToString(), value);
        }

        public DataView Find(string key)
        {
            List<string> path = new List<string>(key.Split('.'));

            if(path.Count == 0)
            {
                return null;
            }
            else if(path.Count == 1)
            {
                return this;
            }
            else
            {
                string subName = path.Shift();
                string newKey = path.Join(".");
                return this.GetSub(subName).Find(newKey);
            }
        }

        public DataView GetSub(string subName)
        // WARNING - DESTRUCTIVE
        {
            return new DataView(this.data.GetSub(subName));
        }

        public DispatcherListener On(string eventName, Callback callback)
        {
            return this.dispatcher.On(eventName, callback);
        }

        // public void Set(object key)
    ///////////////////////////////////////////////////////////////////////////////
    // old
        // private Dictionary<string, bool> locks = new Dictionary<string, bool>();
        // private Dictionary<string, object> children = new Dictionary<string, object>();
        // private int length = 0;

        // public Hashtable data {
        //   get
        //   {
        //     return new Hashtable(children);
        //   }
        // }

        // public DataView(){}

        // public DataView(object value)
        // {
        //   this.Set("value", value);
        // }

        // public DataView(DataView model)
        // {
        //   this.Merge(model);
        // }

        // public static DataView Load(string path)
        // {
        //   path = Application.persistentDataPath + "/" + path;
        //   if (!Exists(path)) return null;

        //   StreamReader r = new StreamReader(path);
        //   string data = r.ReadToEnd();
        //   r.Close();
            
        //   return DataView.Parse(data);
        // }
        
        // public static bool Exists(string path)
        // {
        //   path = Application.persistentDataPath + "/" + path;
        //   return File.Exists(path);
        // }

        // public void Save(string path, DataView payloa)
        // {
        //   path = Application.persistentDataPath + "/" + path;
        //   StreamWriter w = new StreamWriter(path);
        //   w.Write(this.Serialize());
        //   w.Close();
        // }

        // public DataView Merge(DataView model)
        // {
        //   model.Each(this.CopyChild);
        //   return this;
        // }

        // private void CopyChild(string key, object value)
        // {
        //   this.Set(key, value);
        // }

        // public DataView GetChild(int index)
        // {
        //   return this.GetChild(index.ToString());
        // }

        // public DataView GetChild(float key)
        // {
        //   return this.GetChild(key.ToString());
        // }

        // public DataView GetChild(string key)
        // {
        //   if (!this.Has(key)) this.Set(key, new DataView());
        //   return this.Get<DataView>(key);
        // }

        // public T Default<T>(string key)
        // {
        //   if (!this.Has(key)) this.Set(key, default(T));
        //   return this.Get<T>(key);
        // }

        // public DataView Default<T>(string key, T value)
        // {
        //   if(!this.Has(key)) this.Set(key, value);
        //   return this;
        // }

        // public DataView Lock(string key, object value)
        // {
        //   if(this.locks.ContainsKey(key)) return this;
        //   this.Set(key, value);
        //   return this.Lock(key);
        // }

        // public DataView Lock(string key)
        // {
        //   this.locks[key] = true;
        //   return this;
        // }

        // public DataView Default(string key, object value)
        // {
        //   if (!this.Has(key)) { this.Set(key, value); }
        //   return this;
        // }

        // // public DataView Set(int index, object value)
        // // {
        // //   if (index < 0) index = this.length - index;
        // //   if (index > length) length = index + 1;
        // //   this.Set(index.ToString(), value);
        // //   return this;
        // // }

        // public DataView Set(object key, object value)
        // {
        //   return this.Set(key.ToString(), value);
        // }

        // public DataView Set(string key, object value)
        // {
        //   if (this.locks.ContainsKey(key)) return this;

        //   DataView payload = new DataView();
        //   if (this.Has(key))
        //   {
        //     payload.Set("oldValue", this.children[key]);
        //     this.Delete(key);
        //   } 
        //   this.children[key] = value;

        //   if (value != null && value.GetType() == typeof(DataView))
        //   {
        //     DataView child = (DataView)this.children[key];

        //     DataView defaultPayload = new DataView();
        //     defaultPayload.Set("key", key);
        //   }
        //   return this;
        // }

        // public bool Has(object key)
        // {
        //   return this.Has(key.ToString());
        // }

        // public bool Has(string key)
        // {
        //   return this.children.ContainsKey(key);
        // }

        // public DataView Clone()
        // {
        //   return new DataView(this);
        // }

        // private void PassUp(DataView payload)
        // {
        //   string eventName = payload.Get<string>("eventName");
        //   string key = payload.Get<string>("key");
        // }

        // // public T Get<T>(int index)
        // // {
        // //   if (index < 0) index = this.length - index;
        // //   if (index >= this.length) return default(T);
        // //   return this.Get<T>(index.ToString());
        // // }

        // public T Get<T>(object key)
        // {
        //   return this.Get<T>(key.ToString());
        // }

        // public T Get<T>(string key)
        // {
        //   return (T)this.Get(key);
        // }

        // public T Get<T>()
        // {
        //   return (T)this.Get("value");
        // }

        // // public object Get(int index)
        // // {
        // //   if (index < 0) index = this.length - index;
        // //   if (index >= this.length) return null;
        // //   return this.Get(index.ToString());
        // // }
        
        // public object Get(object key)
        // {
        //  return this.Get(key.ToString());
        // }

        // public object Get(string key)
        // {
        //   if (!this.Has(key)) { return null; }
        //   return this.children[key];
        // }

        // public T Pop<T>()
        // {
        //   return this.Pick<T>(-1);
        // }

        // public DataView Push(object value)
        // {
        //   this.Insert(-1, value);
        //   return this;
        // }

        // public T Dequeue<T>()
        // {
        //   return this.Pick<T>(0);
        // }

        // public DataView Enqueue(object value)
        // {
        //   this.Insert(0, value);
        //   return this;
        // }

        // public T Pick<T>(int index)
        // {
        //   if(index < 0) index = this.length + index;
        //   T value = this.Get<T>(index);
        //   this.Remove(index);
        //   return value;
        // }

        // public DataView Insert(int index, object value)
        // {
        //   if(index < 0) index = this.length + index;
            
        //   for(int i = index+1; i < this.length; i++)
        //   {
        //     this.Set(i, this.Get(i-1));
        //   }

        //   this.Set(index, value);

        //   return this;
        // }
        // public DataView Remove(float key)
        // {
        //   this.Remove(key.ToString());

        //   return this;
        // }

        // public DataView Remove(int index)
        // {
        //   if(index < 0) index = this.length + index;

        //   if (!this.Has(index)) return this;

        //   this.Remove(index.ToString());
            
        //   for(int i=index; i < this.length; i++)
        //   {
        //     this.Set(i, this.Get(i+1));
        //   }
        //   this.Delete(this.length - 1);
        //   this.length--;

        //   return this;
        // }

        // public DataView Remove(string key)
        // {
        //   if (!this.Has(key)) return this;

        //   this.Delete(key);

        //   return this;
        // }

        // private void Delete(int key)
        // {
        //   this.Delete(key.ToString());
        // }

        // private DataView Delete(string key)
        // {
        //   if(this.children.ContainsKey(key))
        //   {
        //     this.children.Remove(key);
        //   }
        //   return this;
        // }

        // public string Serialize()
        // {
        //   return "{" + (string)this.Reduce<string>(serializeChild, "") + "}";
        // }

        // private string serializeChild(string serialized, string key, object value)
        // {
        //   if (value == null) return serialized;
        //   if (key[0] == '_') return serialized;

        //   if(serialized != "") serialized += ",";
        //   serialized += "'" + key + "':";

        //   if (value.GetType() == typeof(DataView))
        //   { serialized += ((DataView)value).Serialize(); }

        //   else if (value.GetType() == typeof(int) 
        //   || value.GetType() == typeof(float)
        //   || value.GetType() == typeof(double))
        //   { serialized += value.ToString(); }

        //   else
        //   { serialized += "'" + value.ToString() + "'"; }

        //   return serialized;
        // }


        // public DataView Each(EachIterator eachIterator)
        // {
        //   foreach( KeyValuePair<string, object> kvp in this.children )
        //   {
        //     eachIterator(kvp.Key, kvp.Value);
        //   }

        //   return this;
        // }

        // public T Reduce<T>(ReduceIterator <T>reduceIterator, T memo)
        // {
        //   foreach( KeyValuePair<string, object> kvp in this.children )
        //   {
        //     memo = reduceIterator(memo, kvp.Key, kvp.Value);
        //   }

        //   return memo;
        // }

        // public static DataView Parse(string aJSON)
        // {
        //   Stack<DataView> stack = new Stack<DataView>();
        //   DataView ctx = null;
        //   int i = 0;
        //   StringBuilder token = new StringBuilder();
        //   string tokenName = "";
        //   bool quoteMode = false;
        //   bool tokenIsQuoted = false;
        //   while (i < aJSON.Length)
        //   {
        //     switch (aJSON[i])
        //     {
        //       case '[':
        //         if (quoteMode)
        //         {
        //           token.Append(aJSON[i]);
        //           break;
        //         }

        //         stack.Push(new DataView());
        //         if (ctx != null)
        //         {
        //           ctx.Set(tokenName, stack.Peek());
        //         }
        //         tokenName = "";
        //         token.Length = 0;
        //         ctx = stack.Peek();
        //         break;

        //       case '{':
        //         if (quoteMode)
        //         {
        //           token.Append(aJSON[i]);
        //           break;
        //         }
        //         stack.Push(new DataView());
        //         if (ctx != null)
        //         {
        //           ctx.Set(tokenName, stack.Peek());
        //         }
        //         tokenName = "";
        //         token.Length = 0;
        //         ctx = stack.Peek();
        //         break;


        //       case ']':
        //       case '}':
        //         if (quoteMode)
        //         {

        //           token.Append(aJSON[i]);
        //           break;
        //         }
        //         if (stack.Count == 0)
        //           return null; //throw new Exception("JSON Parse: Too many closing brackets");

        //         stack.Pop();
        //         if (token.Length > 0)
        //         {
        //           ParseElement(ctx, token.ToString(), tokenName, tokenIsQuoted);
        //           tokenIsQuoted = false;
        //         }
        //         tokenName = "";
        //         token.Length = 0;
        //         if (stack.Count > 0)
        //           ctx = stack.Peek();
        //         break;

        //       case ':':
        //         if (quoteMode)
        //         {
        //           token.Append(aJSON[i]);
        //           break;
        //         }
        //         tokenName = token.ToString().Trim();
        //         token.Length = 0;
        //         tokenIsQuoted = false;
        //         break;

        //       case '"':
        //         quoteMode ^= true;
        //         tokenIsQuoted |= quoteMode;
        //         break;

        //       case ',':
        //         if (quoteMode)
        //         {
        //           token.Append(aJSON[i]);
        //           break;
        //         }
        //         if (token.Length > 0)
        //         {
        //           ParseElement(ctx, token.ToString(), tokenName, tokenIsQuoted);
        //           tokenIsQuoted = false;
        //         }
        //         tokenName = "";
        //         token.Length = 0;
        //         tokenIsQuoted = false;
        //         break;

        //       case '\r':
        //       case '\n':
        //         break;

        //       case ' ':
        //       case '\t':
        //         if (quoteMode)
        //           token.Append(aJSON[i]);
        //         break;

        //       case '\\':
        //         ++i;
        //         if (quoteMode)
        //         {
        //           char C = aJSON[i];
        //           switch (C)
        //           {
        //             case 't':
        //               token.Append('\t');
        //               break;
        //             case 'r':
        //               token.Append('\r');
        //               break;
        //             case 'n':
        //               token.Append('\n');
        //               break;
        //             case 'b':
        //               token.Append('\b');
        //               break;
        //             case 'f':
        //               token.Append('\f');
        //               break;
        //             case 'u':
        //               {
        //                 string s = aJSON.Substring(i + 1, 4);
        //                 token.Append((char)int.Parse(
        //                   s,
        //                   System.Globalization.NumberStyles.AllowHexSpecifier));
        //                 i += 4;
        //                 break;
        //               }
        //             default:
        //               token.Append(C);
        //               break;
        //           }
        //         }
        //         break;

        //       default:
        //         token.Append(aJSON[i]);
        //         break;
        //     }
        //     ++i;
        //   }
        //   if (quoteMode)
        //   {
        //     return null; //throw new Exception("JSON Parse: Quotation marks seems to be messed up.");
        //   }
        //   return ctx;
        // }

        // static void ParseElement(DataView ctx, string token, string tokenName, bool quoted)
        // {
        //   if (quoted)
        //   {
        //     ctx.Set(tokenName, token);
        //     return;
        //   }
        //   string tmp = token.ToLower();
        //   //check if bool
        //   if (tmp == "false" || tmp == "true")
        //     ctx.Set(tokenName, tmp == "true");
        //   //check if Vector3
        //   //check if null
        //   else if (tmp == "null")
        //     ctx.Remove(tokenName);
        //   else
        //   {
        //     double val;
        //     if (double.TryParse(token, out val))
        //       ctx.Set(tokenName, val);
        //     else
        //       ctx.Set(tokenName, token);
        //   }
        // }
    }
}
