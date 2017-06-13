using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

using Ukulele;

namespace Ukulele
{
  public static class JSON {
    public static string Serialize(Hashtable hashtable)
    {
      string jsonBody = hashtable.Reduce<string>(serializeChild, "");
      return "{" + jsonBody + "}";
    }

    public static Hashtable Parse(string aJSON)
    {
      Stack<Hashtable> stack = new Stack<Hashtable>();
      Hashtable ctx = null;
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

            stack.Push(new Hashtable());
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
            stack.Push(new Hashtable());
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

    private static string serializeChild(string memo, object key, object value)
    {
      string keyString = key.ToString();
      if (value == null) return memo;
      if (keyString[0] == '_') return memo;

      if(memo != "") memo += ",";
      memo += "'" + keyString + "':";

      if (value.GetType() == typeof(Hashtable))
      { memo += JSON.Serialize((Hashtable)value); }

      else if (value.GetType() == typeof(int) 
      || value.GetType() == typeof(float)
      || value.GetType() == typeof(double))
      { memo += value.ToString(); }

      else
      { memo += "'" + value.ToString() + "'"; }

      return memo;
    }

    private static void ParseElement(Hashtable ctx, string token, string tokenName, bool quoted)
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
}
