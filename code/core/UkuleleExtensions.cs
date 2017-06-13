using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Ukulele;
namespace Ukulele
{
    public static class UkuleleExtensions
    {
        static public Rigidbody Detach(this GameObject that)
        {
            Rigidbody parentRigidbody = that.GetComponentInParent<Rigidbody>();

            that.AddComponent<Rigidbody>();
            that.GetComponent<Rigidbody>().velocity = parentRigidbody.GetRelativePointVelocity(that.transform.localPosition);
            that.GetComponent<Rigidbody>().angularVelocity = parentRigidbody.angularVelocity;

            that.transform.parent = null;

            return that.GetComponent<Rigidbody>();
        }
        static public Rigidbody Detach(this Component that)
        {
            return that.gameObject.Detach();
        }

        public static T WithComponent<T>(this GameObject that) where T : Component
        {
            T component = that.GetComponent<T>();
            if (component == null) {
                component = that.gameObject.AddComponent<T>();
            }
            return component;
        }
        public static T WithComponent<T>(this Component that) where T : Component
        {
            return that.gameObject.WithComponent<T>();
        }

    // Advanced Proxies
        static public GameObject GetChildObject(this GameObject that, int id)
        {
            return that.GetComponent<Transform>().GetChild(id).gameObject;
        }
        static public GameObject GetChildObject(this Component that, int id)
        {
            return that.GetComponent<Transform>().GetChild(id).gameObject;
        }

        static public GameObject GetChildObject(this GameObject that, string name)
        {
            return that.GetComponent<Transform>().Find(name).gameObject;
        }
        static public GameObject GetChildObject(this Component that, string name)
        {
            return that.GetComponent<Transform>().Find(name).gameObject;
        }

    // Simple Proxies
        static public int GetIDForChild(this GameObject that, GameObject child)
        {
            return that.WithComponent<UkuleleController>().GetIDForChild(child);
        }
        static public int GetIDForChild(this Component that, GameObject child)
        {
            return that.WithComponent<UkuleleController>().GetIDForChild(child);
        }

        static public int GetChildCount(this GameObject that)
        {
            return that.WithComponent<UkuleleController>().GetChildCount();
        }
        static public int GetChildCount(this Component that)
        {
            return that.WithComponent<UkuleleController>().GetChildCount();
        }

        static public GameObject SetExclusiveChild(this GameObject that, int id)
        {
            return that.WithComponent<UkuleleController>().SetExclusiveChild(id);
        }
        static public GameObject SetExclusiveChild(this Component that, int id)
        {
            return that.WithComponent<UkuleleController>().SetExclusiveChild(id);
        }

        static public GameObject SetExclusiveChild(this GameObject that, string name)
        {
            return that.WithComponent<UkuleleController>().SetExclusiveChild(name);
        }
        static public GameObject SetExclusiveChild(this Component that, string name)
        {
            return that.WithComponent<UkuleleController>().SetExclusiveChild(name);
        }
        
        // static public void SetRoot(this GameObject that)
        // {
        //   that.WithComponent<UkuleleController>().SetRoot();
        // }
        // static public void SetRoot(this Component that)
        // {
        //   that.WithComponent<UkuleleController>().SetRoot();
        // }

        // static public Transform FindRoot(this GameObject that)
        // {
        //   that.WithComponent<UkuleleController>().FindRoot();
        // }
        // static public Transform FindRoot(this Component that)
        // {
        //   that.WithComponent<UkuleleController>().FindRoot();
        // }

        // static public bool IsRoot(this GameObject that)
        // {
        //   that.WithComponent<UkuleleController>().IsRoot();
        // }
        // static public bool IsRoot(this Component that)
        // {
        //   that.WithComponent<UkuleleController>().IsRoot();
        // }
        
        static public void Trigger(this GameObject that, string eventName)
        {
            that.WithComponent<UkuleleController>().Trigger(eventName);
        }
        static public void Trigger(this Component that, string eventName)
        {
            that.WithComponent<UkuleleController>().Trigger(eventName);
        }

        static public void Trigger(this GameObject that, string eventName, object payload)
        {
            that.WithComponent<UkuleleController>().Trigger(eventName, payload);
        }
        static public void Trigger(this Component that, string eventName, object payload)
        {
            that.WithComponent<UkuleleController>().Trigger(eventName, payload);
        }

        static public void Trigger(this GameObject that, object eventData)
        {
            that.WithComponent<UkuleleController>().Trigger(eventData);
        }
        static public void Trigger(this Component that, object eventData)
        {
            that.WithComponent<UkuleleController>().Trigger(eventData);
        }

        static public void Emit(this GameObject that, string eventName)
        {
            that.WithComponent<UkuleleController>().Emit(eventName);
        }
        static public void Emit(this Component that, string eventName)
        {
            that.WithComponent<UkuleleController>().Emit(eventName);
        }

        static public void Emit(this GameObject that, string eventName, object payload)
        {
            that.WithComponent<UkuleleController>().Emit(eventName, payload);
        }
        static public void Emit(this Component that, string eventName, object payload)
        {
            that.WithComponent<UkuleleController>().Emit(eventName, payload);
        }

        static public void Emit(this GameObject that, object eventData)
        {
            that.WithComponent<UkuleleController>().Emit(eventData);
        }
        static public void Emit(this Component that, object eventData)
        {
            that.WithComponent<UkuleleController>().Emit(eventData);
        }

        static public DispatcherListener On(this GameObject that, string eventName, Callback callback)
        {
            return that.WithComponent<UkuleleController>().On(eventName, callback);
        }
        static public DispatcherListener On(this Component that, string eventName, Callback callback)
        {
            return that.WithComponent<UkuleleController>().On(eventName, callback);
        }

        public static Dispatcher GetDispatcher(this GameObject that)
        {
            return that.WithComponent<UkuleleController>().GetDispatcher();
        }
        public static Dispatcher GetDispatcher(this Component that)
        {
            return that.WithComponent<UkuleleController>().GetDispatcher();
        }

        public static bool IsOnGround(this GameObject that)
        {
            return that.WithComponent<UkuleleController>().IsOnGround();
        }
        public static bool IsOnGround(this Component that)
        {
            return that.WithComponent<UkuleleController>().IsOnGround();
        }

        public static bool IsTouching(this GameObject that, object other)
        {
            return that.WithComponent<UkuleleController>().IsTouching(other);
        }
        public static bool IsTouching(this Component that, object other)
        {
            return that.WithComponent<UkuleleController>().IsTouching(other);
        }

        public static bool HasComponent<T>(this GameObject that) where T : Component
        {
            return that.WithComponent<UkuleleController>().HasComponent<T>();
        }
        public static bool HasComponent<T>(this Component that) where T : Component
        {
            return that.WithComponent<UkuleleController>().HasComponent<T>();
        }

        public static bool AssertHasComponent<T>(this GameObject that) where T : Component
        {
            return that.WithComponent<UkuleleController>().AssertHasComponent<T>();
        }
        public static bool AssertHasComponent<T>(this Component that) where T : Component
        {
            return that.WithComponent<UkuleleController>().AssertHasComponent<T>();
        }

        public static bool HasComponentInParent<T>(this GameObject that) where T : Component
        {
            return that.WithComponent<UkuleleController>().HasComponentInParent<T>();
        }
        public static bool HasComponentInParent<T>(this Component that) where T : Component
        {
            return that.WithComponent<UkuleleController>().HasComponentInParent<T>();
        }

        public static bool AssertHasComponentInParent<T>(this GameObject that) where T : Component
        {
            return that.WithComponent<UkuleleController>().AssertHasComponentInParent<T>();
        }
        public static bool AssertHasComponentInParent<T>(this Component that) where T : Component
        {
            return that.WithComponent<UkuleleController>().AssertHasComponentInParent<T>();
        }

        public static bool HasComponentInChildren<T>(this GameObject that) where T : Component
        {
            return that.WithComponent<UkuleleController>().HasComponentInChildren<T>();
        }
        public static bool HasComponentInChildren<T>(this Component that) where T : Component
        {
            return that.WithComponent<UkuleleController>().HasComponentInChildren<T>();
        }

        public static bool AssertHasComponentInChildren<T>(this GameObject that) where T : Component
        {
            return that.WithComponent<UkuleleController>().AssertHasComponentInChildren<T>();
        }
        public static bool AssertHasComponentInChildren<T>(this Component that) where T : Component
        {
            return that.WithComponent<UkuleleController>().AssertHasComponentInChildren<T>();
        }

        public static void RemoveComponent<T>(this GameObject that) where T : Component
        {
            that.WithComponent<UkuleleController>().RemoveComponent<T>();
        }
        public static void RemoveComponent<T>(this Component that) where T : Component
        {
            that.WithComponent<UkuleleController>().RemoveComponent<T>();
        }

        // public static T FindComponent<T>(this GameObject that) where T : Component
        // {
        //   return that.WithComponent<UkuleleController>().FindComponent<T>();
        // }
        // public static T FindComponent<T>(this Component that) where T : Component
        // {
        //   return that.WithComponent<UkuleleController>().FindComponent<T>();
        // }


        public static void Set(this GameObject that, object key, object value)
        {
            that.WithComponent<UkuleleController>().Set(key, value);
        }
        public static void Set(this Component that, object key, object value)
        {
            that.WithComponent<UkuleleController>().Set(key, value);
        }

        public static object Get(this GameObject that, object key)
        {
            return that.WithComponent<UkuleleController>().Get(key);
        }
        public static object Get(this Component that, object key)
        {
            return that.WithComponent<UkuleleController>().Get(key);
        }

        public static T Get<T>(this GameObject that, object key)
        {
            return that.WithComponent<UkuleleController>().Get<T>(key);
        }
        public static T Get<T>(this Component that, object key)
        {
            return that.WithComponent<UkuleleController>().Get<T>(key);
        }
    }
}
