using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class String_EXT {
  public static byte[] ToBytes(this string str) {
    byte[] bytes = new byte[str.Length * sizeof(char)];
    System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
    return bytes;
  }
  // TODO: Make part of string library or make a byte library?
  public static string SetBytes(this WWW that, byte[] bytes) {
    char[] chars = new char[bytes.Length / sizeof(char)];
    System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
    return new string(chars);
  }
}