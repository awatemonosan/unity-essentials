using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class UQuery : Singleton<UQuery>
{
//   public static USelection Body ()
//   {
//     USelection body = new USelection();
//     foreach (GameObject obj in GameObject.FindObjectsOfType(typeof(GameObject)))
//     {
//       if (obj.transform.parent == null)
//         body.Add(obj);
//     }
//     return body;
//   }

//   public static USelection Query (string[] queryStrings)
//   {
//     return Query(UQuery.Body(), queryStrings);
//   }
//   public static USelection Query (USelection root, string[] queryStrings)
//   {
//     return root.Query(queryStrings);
//   }
//   public static USelection Query (GameObject gameObject, string[] queryStrings)
//   {
//     return (new USelection(gameObject)).Query(queryStrings);
//   }
//   public static USelection Query (Component component, string[] queryStrings)
//   {
//     return (new USelection(component)).Query(queryStrings);
//   }

//   public static USelection Query (string queryString)
//   {
//     return Query(UQuery.Body(), queryString);
//   }
//   public static USelection Query (GameObject gameObject, string queryString)
//   {
//     return (new USelection(gameObject)).Query(queryString);
//   }
//   public static USelection Query (Component component, string queryString)
//   {
//     return (new USelection(component)).Query(queryString);
//   }
//   public static USelection Query (USelection root, string queryString)
//   {
//     return root.Query(queryString);
//   }
// }

// public class USelection
// {
//   private List<GameObject> contents;
  
//   public USelection ()
//   {
//      this.contents = new List<GameObject>();
//   }
//   public USelection (Transform transform)
//   {
//      this.contents.Add(transform.gameObject);
//   }
//   public USelection (Component component)
//   {
//      this.contents.Add(component.gameObject);
//   }
//   public USelection (GameObject gameObject)
//   {
//      this.contents = new List<GameObject>();
//      this.contents.Add(gameObject);
//   }
//   public USelection (GameObject[] gameObjects)
//   {
//      this.contents = new List<GameObject>(gameObjects);
//   }
//   public USelection (USelection defaultContents)
//   {
//      this.contents = defaultContents.List();
//   }

//   public USelection Add (Transform transform)
//   {
//     this.contents.Add(transform.gameObject);
//     return this;
//   }
//   public USelection Add (Component component)
//   {
//     this.contents.Add(component.gameObject);
//     return this;
//   }
//   public USelection Add( GameObject gameObject)
//   {
//     this.contents.Add(gameObject);
//     return this;
//   }
//   public USelection Add( GameObject[] gameObjects)
//   {
//     return this.Add(new List<GameObject>(gameObjects));
//   }
//   public USelection Add (USelection otherSelection)
//   {
//     return this.Add(otherSelection.List());
//   }
//   public USelection Add (List<GameObject> gameObjects)
//   {
//     this.contents.AddRange(gameObjects);
//     return this;
//   }

//   public GameObject First ()
//   {
//     return contents[0];
//   }

//   public USelection Children ()
//   {
//     USelection result = new USelection();

//     foreach (GameObject gameObject in this.contents)
//     {
//       foreach (Transform child in gameObject.transform)
//       {
//         result.Add(child);
//       }
//     }
//     return result;
//   }

//   public USelection Parent ()
//   {
//     USelection result = new USelection();
//     foreach (GameObject gameObject in this.contents)
//     {
//       if (gameObject.transform.parent)
//         result.Add(gameObject.transform.parent);
//     }
//     return result;
//   }

//   public USelection Create ()
//   {
//     this.CreateThen();
//     return this;
//   }
//   public USelection Create (GameObject prefab)
//   {
//     this.CreateThen(prefab);
//     return this;
//   }

//   public USelection CreateAnd ()
//   {
//     return this.Add(this.CreateThen());
//   }
//   public USelection CreateAnd(GameObject prefab)
//   {
//     return this.Add(this.CreateThen(prefab));
//   }

//   public USelection CreateThen ()
//   {
//     USelection children = new USelection();
//     foreach (GameObject gameObject in contents)
//     {
//       GameObject newGameObject = new GameObject();
//       newGameObject.transform.parent = gameObject.transform;
//       children.Add(newGameObject);
//     }
//     return children;
//   }
//   public USelection CreateThen (GameObject prefab)
//   {
//     USelection children = new USelection();
//     foreach (GameObject gameObject in contents)
//     {
//       GameObject newGameObject = GameObject.Instantiate(prefab);
//       newGameObject.transform.parent = gameObject.transform;
//       children.Add(newGameObject);
//     }
//     return children;
//   }

//   public USelection AddClass (string className)
//   {
//     foreach (GameObject gameObject in contents)
//     {
//       gameObject.AddClass(className);
//     }
//     return this;
//   }

//   public USelection RemoveClass (string className)
//   {
//     foreach (GameObject gameObject in contents)
//     {
//       gameObject.RemoveClass(className);
//     }
//     return this;
//   }

//   public USelection AddID (string id)
//   {
//     foreach (GameObject gameObject in contents)
//     {
//       gameObject.AddID(id);
//     }
//     return this;
//   }

//   public USelection RemoveID (string id)
//   {
//     foreach (GameObject gameObject in contents)
//     {
//       gameObject.RemoveID(id);
//     }
//     return this;
//   }

//   public USelection SetState (string stateName, string state)
//   {
//     foreach (GameObject gameObject in contents)
//     {
//       gameObject.SetState(stateName, state);
//     }
//     return this;
//   }

//   public USelection On (string evnt, Callback callback)
//   {
//     foreach (GameObject gameObject in contents)
//     {
//       gameObject.GetDispatcher().On(evnt, callback);
//     }
//     return this;
//   }

//   public USelection Trigger (string evnt)
//   {
//     foreach (GameObject gameObject in contents)
//     {
//       gameObject.GetDispatcher().Trigger(evnt);
//     }
//     return this;
//   }
//   public USelection Trigger (string evnt, UData payload)
//   {
//     foreach (GameObject gameObject in contents)
//     {
//       gameObject.GetDispatcher().Trigger(evnt, payload);
//     }
//     return this;
//   }
//   public USelection Trigger (UData payload)
//   {
//     foreach (GameObject gameObject in contents)
//     {
//       gameObject.GetDispatcher().Trigger(payload);
//     }
//     return this;
//   }

//   public USelection AddComponent<T>() where T : Component
//   {
//     foreach (GameObject gameObject in contents)
//     {
//       gameObject.AddComponent<T>();
//     }
//     return this;
//   }

//   public List<GameObject> List ()
//   {
//     return new List<GameObject>(this.contents);
//   }

//   public int Count ()
//   {
//     return this.contents.Count;
//   }

//   public USelection Query (string queryString)
//   {
//     string[] stringSeparators = new string[]{" "};
//     string[] queryArray = queryString.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
//     return this.Query(queryArray);
//   }

//   public USelection Query (string[] queryArray)
//   {
//     //TODO:
//     // Create issue with the following:
//     // DETERMIN IF THE FOLLOWING IS POSSIBLE. IF NOT, REDESIGN.
//     // Treat queryArray as an imutible object
//     // [] pass reference to full array and current index. (instead of array reference)
//     // [] don't clone the array and deque things.
//     // [] repeat pattern elsewhere.
//     // [√] be embarassed haha

//     queryArray = (string[]) queryArray.Clone();
//     USelection queryResult = new USelection();

//     if(this.Count() > 1)
//     {
//       foreach (GameObject gameObject in this.contents)
//       {
//         USelection element = new USelection(gameObject);
//         queryResult.Add(element.Query(queryArray));
//       }
//     }
//     else if (this.Count() == 1)
//     {
//       queryResult.Add(this.Children().Query(queryArray));

//       if(queryArray.Length == 0)
//         return new USelection(this);

//       GameObject gameObject = this.contents[0];

//       string[] newQueryArray = new string[queryArray.Length-1];
//       Array.Copy(queryArray, 1, newQueryArray, 0, queryArray.Length-1);

//       string query = queryArray[0];

//       if (this.isMatch(gameObject, query))
//         queryResult.Add(this.Query(newQueryArray));
      
//     }
//     return queryResult;
//   }

//   public bool isMatch (GameObject gameObject, string query)
//   {
//     int begining = 0;
//     int end = 0;
//     string selectors = "\"@#.?";
//     while (begining < query.Length)
//     {
//       end = query.IndexOf(selectors, begining);
//       if (end == -1) { end = query.Length; }
//       string subQuery = query.Substring(begining, end);
//       if (subQuery[0] == '"') // name
//       {
//         string name = subQuery.Substring(1, subQuery.Length-1);
//         if (gameObject.name != name)
//           return false;
//       }
//       else if (subQuery[0] == '@') // TAG
//       {
//         string tag = subQuery.Substring(1, subQuery.Length-1);
//         if (gameObject.tag != tag)
//           return false;
//       }
//       else if (subQuery[0] == '#') // ID
//       {
//         string id = subQuery.Substring(1, subQuery.Length-1);
//         if(!gameObject.HasID(id))
//           return false;
//       }
//       else if (subQuery[0] == '.') // CLASS
//       {
//         string className = subQuery.Substring(1, subQuery.Length-1);
//         if(!gameObject.HasClass(className))
//           return false;
//       }
//       else if (subQuery[0] == '?') // STATE
//       {
//         string[] keyvalue = subQuery.Substring(1, subQuery.Length-1).Split(':');
//         string stateName = keyvalue[0];
//         string state = keyvalue[1];

//         if(gameObject.GetState(stateName).Equals(state))
//         {
//           return false;
//         }
//       }
//       // else if (subQuery[0] == '&') // COMPONENT
//       // {
//       //   string component = subQuery.Substring(1, subQuery.Length-1);
//       //   if (!gameObject.HasComponent(component))
//       //     return false;
//       // }
//       else
//         return false;
//       begining = end;
//     }
//     return true;
//   }
// OLD
  // public bool isMatch_old (GameObject gameObject, string query)
  // {
  //   int begining = 0;
  //   for (int index = 0; index < query.Length; index++)
  //   {
  //     if (query.IndexOf(selectors, query[index]) != -1)
  //     {
  //       if (index > begining)
  //       {
  //         string subQuery = query.Substring(begining, index);
  //         if (subQuery[0] == '"') // name
  //         {
  //           string name = subQuery.Substring(1, subQuery.Length-1);
  //           if (gameObject.name != name)
  //             return false;
  //         }
  //         else if (subQuery[0] == '@') // TAG
  //         {
  //           string tag = subQuery.Substring(1, subQuery.Length-1);
  //           if (gameObject.tag != tag)
  //             return false;
  //         }
  //         else if (subQuery[0] == '#') // ID
  //         {
  //           string id = subQuery.Substring(1, subQuery.Length-1);
  //           if(!gameObject.HasID(id))
  //             return false;
  //         }
  //         else if (subQuery[0] == '.') // CLASS
  //         {
  //           string className = subQuery.Substring(1, subQuery.Length-1);
  //           if(!gameObject.HasClass(className))
  //             return false;
  //         }
  //         else if (subQuery[0] == '?') // STATE
  //         {
  //           string[] keyvalue = subQuery.Substring(1, subQuery.Length-1).Split(':');
  //           string stateName = keyvalue[0];
  //           string state = keyvalue[1];

  //           if(gameObject.GetState(stateName).Equals(state))
  //           {
  //             return false;
  //           }
  //         }
  //         // else if (subQuery[0] == '&') // COMPONENT
  //         // {
  //         //   string component = subQuery.Substring(1, subQuery.Length-1);
  //         //   if (!gameObject.HasComponent(component))
  //         //     return false;
  //         // }
  //         else
  //           return false;
  //       }
  //       begining = index;
  //     }

  //   }
  //   return true;
  // }
}
