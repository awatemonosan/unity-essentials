using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Selection
{
  private List<GameObject> selection;
  
  public Selection ()
  {
     this.selection = new List<GameObject>();
  }
  public Selection (Transform transform)
  {
     this.selection.Add(transform.gameObject);
  }
  public Selection (GameObject gameObject)
  {
     this.selection = new List<GameObject>();
     this.selection.Add(gameObject);
  }
  public Selection (GameObject[] gameObjects)
  {
     this.selection = new List<GameObject>(gameObjects);
  }
  public Selection (Selection selection)
  {
     this.selection = selection.GetAll();
  }

  public Selection Add (Transform transform)
  {
    this.selection.Add(transform.gameObject);
    return this;
  }
  public Selection Add( GameObject gameObject)
  {
    this.selection.Add(gameObject);
    return this;
  }
  public Selection Add( GameObject[] gameObjects)
  {
    this.Add(new List<GameObject>(gameObjects));
    return this;
  }
  public Selection Add (Selection selection)
  {
    this.Add(selection.GetAll());
    return this;
  }
  public Selection Add (List<GameObject> gameObjects)
  {
    this.selection.AddRange(gameObjects);
    return this;
  }

  public Selection Children ()
  {
    Selection result = new Selection();
    foreach (GameObject gameObject in this.selection)
    {
      foreach (Transform child in gameObject.transform)
      {
        result.Add(child);
      }
    }
    return result;
  }

  public Selection Parent ()
  {
    Selection result = new Selection();
    foreach (GameObject gameObject in this.selection)
    {
      if (gameObject.transform.parent)
      {
        result.Add(gameObject.transform.parent);
      }
    }
    return result;
  }

  public Selection Create ()
  {
    this.CreateThen();
    return this;
  }
  public Selection Create (GameObject prefab)
  {
    this.CreateThen(prefab);
    return this;
  }

  public Selection CreateAnd ()
  {
    return this.Add(this.CreateThen());
  }
  public Selection CreateAnd(GameObject prefab)
  {
    return this.Add(this.CreateThen(prefab));
  }

  public Selection CreateThen ()
  {
    Selection children = new Selection();
    foreach (GameObject gameObject in selection)
    {
      GameObject newGameObject = new GameObject();
      newGameObject.transform.parent = gameObject.transform;
      children.Add(newGameObject);
    }
    return children;
  }
  public Selection CreateThen (GameObject prefab)
  {
    Selection children = new Selection();
    foreach (GameObject gameObject in selection)
    {
      GameObject newGameObject = GameObject.Instantiate(prefab);
      newGameObject.transform.parent = gameObject.transform;
      children.Add(newGameObject);
    }
    return children;
  }

  public Selection AddComponent<T>() where T : Component
  {
    foreach (GameObject gameObject in selection)
    {
      gameObject.AddComponent<T>();
    }
    return this;
  }

  public Selection AddClass (string className)
  {
    foreach (GameObject gameObject in selection)
    {
      UQueryClassName classNameComponent = gameObject.AddComponent<UQueryClassName>();
      classNameComponent.className = className;
    }
    return this;
  }

  public Selection RemoveClass (string className)
  {
    foreach (GameObject gameObject in selection)
    {
      foreach (UQueryClassName classNameComponent in gameObject.GetComponents(typeof(UQueryClassName)))
      {
        if (classNameComponent.className == className)
          GameObject.Destroy(classNameComponent);
      }
    }
    return this;
  }

  public Selection SetID (string id)
  {
    foreach (GameObject gameObject in selection)
    {
      UQueryID idComponent = gameObject.AddComponent<UQueryID>();
      idComponent.id = id;
    }
    return this;
  }

  public Selection SetState (string state)
  {
    foreach (GameObject gameObject in selection)
    {
      UQueryState stateComponent = gameObject.AddComponent<UQueryState>();
      stateComponent.state = state;
    }
    return this;
  }

  public Selection On (string evnt, Callback callback)
  {
    foreach (GameObject gameObject in selection)
    {
      gameObject.On(evnt, callback);
    }
    return this;
  }

  public Selection Off (int reference)
  {
    foreach (GameObject gameObject in selection)
    {
      gameObject.Off(reference);
    }
    return this;
  }

  public Selection Trigger (string evnt)
  {
    foreach (GameObject gameObject in selection)
    {
      gameObject.Trigger(evnt);
    }
    return this;
  }
  public Selection Trigger (string evnt, Hashtable payload)
  {
    foreach (GameObject gameObject in selection)
    {
      gameObject.Trigger(evnt, payload);
    }
    return this;
  }
  public Selection Trigger (Hashtable payload)
  {
    foreach (GameObject gameObject in selection)
    {
      gameObject.Trigger(payload);
    }
    return this;
  }

  public List<GameObject> GetAll ()
  {
    return new List<GameObject>(this.selection);
  }

  public int Count ()
  {
    return this.selection.Count;
  }

  public Selection Query (string queryString)
  {
    string[] stringSeparators = new string[]
    {" "};
    string[] queryArray = queryString.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
    return this.Query(queryArray);
  }

  public Selection Query (string[] queryArray)
  {
    queryArray = (string[]) queryArray.Clone();
    Selection queryResult = new Selection();

    if(this.Count() > 1)
    {
      foreach (GameObject gameObject in this.selection)
      {
        Selection element = new Selection(gameObject);
        queryResult.Add(element.Query(queryArray));
      }

    }
    else if (this.Count() == 1)
    {
      if(queryArray.Length == 0)
        return new Selection(this);

      GameObject gameObject = this.selection[0];

      string[] newQueryArray = new string[queryArray.Length-1];
      Array.Copy(queryArray, 1, newQueryArray, 0, queryArray.Length-1);

      string query = queryArray[0];

      if (query[0] == '^')
      {
        query = query.Substring(1, query.Length-1);
        if ((!this.isMatch(gameObject, query)) && (this.Parent().Count()>0))
          return this.Parent().Query(queryArray);
        queryArray[0] = query;
      }

      queryResult.Add(this.Children().Query(queryArray));

      if (this.isMatch(gameObject, query))
        return this.Query(newQueryArray);
      
    }
    return queryResult;
  }

  public bool isMatch (GameObject gameObject, string query)
  {
    int begining = 0;
    char[] selectors = new char[]{'@','#','.','&', '^',' '};
    for (int index = 0; index < query.Length; index++)
    {
      if (Array.IndexOf(selectors, query[index]) != -1)
      {
        if (index > begining)
        {
          string subQuery = query.Substring(begining, index);
          if (subQuery[0] == '@') // TAG
          {
            string tag = subQuery.Substring(1, subQuery.Length-1);
            if (gameObject.tag != tag)
              return false;
          }
          else if (subQuery[0] == '#') // ID
          {
            string id = subQuery.Substring(1, subQuery.Length-1);
            if(!gameObject.GetComponent<UQueryID>())
              return false;
            if(gameObject.GetComponent<UQueryID>().id != id)
              return false;
          }
          else if (subQuery[0] == '.') // CLASS
          {
            string className = subQuery.Substring(1, subQuery.Length-1);
            if (!gameObject.GetComponent<UQueryClassName>())
              return false;
            if (gameObject.GetComponent<UQueryClassName>().className != className)
              return false;
          }
          else if (subQuery[0] == '$') // STATE
          {
            string state = subQuery.Substring(1, subQuery.Length-1);
            if (!gameObject.GetComponent<UQueryClassName>())
              return false;
            if (gameObject.GetComponent<UQueryState>().state != state)
              return false;
          }
          else if (subQuery[0] == '&') // COMPONENT
          {
            string component = subQuery.Substring(1, subQuery.Length-1);
            if (!gameObject.GetComponent(component))
              return false;
          }
          else
            return false;
        }
        begining = index;
      }

    }
    return true;
  }
}

public class UQuery : Singleton<Broadcaster>
{
  public static Selection Body ()
  {
    Selection body = new Selection();
    foreach (GameObject obj in GameObject.FindObjectsOfType(typeof(GameObject)))
    {
      if (obj.transform.parent == null)
        body.Add(obj);
    }
    return body;
  }

  public static Selection Query (string queryString)
  {
    return Query(UQuery.Body(), queryString);
  }
  public static Selection Query (Selection root, string queryString)
  {
    return root.Query(queryString);
  }
}
