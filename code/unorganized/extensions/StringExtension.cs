using UnityEngine;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public static class StringExtension {
  public static string UppercaseFirst(this string that)
  {
    // Check for empty string.
    if (string.IsNullOrEmpty(that))
    {
      return string.Empty;
    }
    // Return char and concat substring.
    return char.ToUpper(that[0]) + that.Substring(1);
  }

  public static byte[] ToBytes(this string str)
  {
    byte[] bytes = new byte[str.Length * sizeof(char)];
    System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
    return bytes;
  }
  // TODO: Make part of string library or make a byte library?
  public static string SetBytes(this WWW that, byte[] bytes)
  {
    char[] chars = new char[bytes.Length / sizeof(char)];
    System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
    return new string(chars);
  }

  public static int IndexOf(this string that, string chars, int startAt)
  {
    var bestIndex = -1;
    chars.Each( delegate(char character) {
      int index = that.IndexOf(character, startAt);
      if (index != -1 && index < bestIndex) { bestIndex = index; }
    });
    return bestIndex;
  }

  public static int IndexOf(this string that, char target, int startAt)
  {
    int bestIndex = -1;

    that.Each(startAt, delegate(char character, int index) {
      if(character == target && (bestIndex == -1 || index < bestIndex)){ bestIndex = index; }
    });

    return bestIndex;
  }

  public delegate void StringEachIteratorOneArgument(char character);
  public delegate void StringEachIteratorTwoArgument(char character, int index);
  public delegate void StringEachIteratorThreeArgument(char character, int index, string str);
  public static void Each( this string that,  StringEachIteratorOneArgument iterator )
  {
    that.Each(0, iterator);
  }

  public static void Each( this string that, StringEachIteratorTwoArgument iterator )
  {
    that.Each(0, iterator);
  }

  public static void Each( this string that, StringEachIteratorThreeArgument iterator )
  {
    that.Each(0, iterator);
  }

  public static void Each( this string that, int startAt, StringEachIteratorOneArgument iterator )
  {
    that.Each(startAt, delegate( char character, int index, string str ){
      iterator(character);
    });
  }

  public static void Each( this string that, int startAt, StringEachIteratorTwoArgument iterator )
  {
    that.Each(startAt, delegate( char character, int index, string str ){
      iterator(character, index);
    });
  }

  public static void Each( this string that, int startAt, StringEachIteratorThreeArgument iterator )
  {
    for( int i = startAt; i < that.Length; i ++ )
    {
      iterator(that[i], i, that);
    }
  }
}