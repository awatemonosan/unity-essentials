// using UnityEngine;
// using System.Collections;
// using System.Collections.Generic;
// public static class JSON_EXT {
//   public static Hashtable Merge(this Hashtable that, JSONObject other)
//   {
//     if(obj.type==JSONObject.Type.OBJECT) {
//       for(int i = 0; i < other.list.Count; i++){
//         string key = (string)obj.keys[i];
//         JSONObject j = (JSONObject)obj.list[i];
//         that[key] = 
//         Debug.Log(key);
//         accessData(j);
//       }
//     } else if(obj.type==JSONObject.Type.ARRAY) {
//         foreach(JSONObject j in obj.list){
//           accessData(j);
//         }
//     return that;
//   }
//   public static JSONObject ToJSON(this Object that) {
//     return JSONObject(that);
//   }
//   public static JSONObject ToJSON(this bool that) {
//     return JSONObject(that);
//   }
//   public static JSONObject ToJSON(this int that) {
//     return JSONObject(that);
//   }
//   public static JSONObject ToJSON(this float that) {
//     return JSONObject(that);
//   }
//   public static JSONObject ToJSON(this string that) {
//     return JSONObject(that);
//   }
//   public static JSONObject ToJSON(this HashTable that) {
//     return JSONObject(that);
//     JSONObject json new JSONObject(JSONObject.Type.OBJECT);

//     foreach (DictionaryEntry entry in that)
//     {
//         json.AddField(entry.Key, entry.ToJSON());
//     }

//     return json;
//   }

//   public static void LoadJSON(this HashTable that, string json) {
//     that.LoadJSON(JSON.parse());
//   }

//   public static void LoadJSON(this HashTable that, JSONNode json) {

//   }

//   public static string JSON(this Hashtable that)
//   {
//     Debug.Log("[WARNING: DEPRECATED]");

//     return that.ToJSON();
//     /*
//     string s = "{";
//     bool first = true;

//     foreach(DictionaryEntry entry in that) {
//       s = s + ""; // TODO: New Line

//       if(!first){
//         s = s + ", ";
//       }

//       first = false;

//       s = s + entry.Key + ": \"" + entry.Value + "\"";
//     }

//     s = s + "\r}";

//     return s;
//     */
//   }
// }