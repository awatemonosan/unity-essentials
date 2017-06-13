using UnityEngine;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

using Ukulele;

namespace Ukulele
{
    class Impact
    {
        public Vector3 normal;
        public Collider otherCollider;
        public Vector3 point;
        public Collider thisCollider;
        
        public GameObject thisGameObject;
        public GameObject otherGameObject;
        public Vector3 relativeVelocity;
    }

    public class UkuleleController : MonoBehaviour
    {
    // static variables
        static int nextID = 0;

    // configuration
        public float maxSlope = 45;
        public bool isHead = false;

    // public
        public Dispatcher dispatcher = new Dispatcher( );
        public DataView dataView = new DataView();

    // private
        private int ID;
        private Vector3 groundNormal = Physics.gravity.normalized;
        private Hashtable impacts = new Hashtable( );

    // Unity Messages
        void Awake()
        {
            this.ID = nextID++;
            this.dataView.On("all", this.Trigger);
        }

        // private Dictionary<System.Type, Component> typeLookupTable = new Dictionary<System.Type, Component>();
        // public void Update()
        // {
        //   this.typeLookupTable = new Dictionary<System.Type, Component>();
        // }

        public void FixedUpdate()
        {
            // foreach (Component component in this.GetComponents<Component>())
            // {
            //     if(component.GetType() == typeof(Rigidbody)
            //     || component.GetType() == typeof(Transform))
            //     { continue; }

            //     Type type = component.GetType();
            //     PropertyInfo[] properties = type.GetProperties(
            //         BindingFlags.Public
            //       | BindingFlags.Instance
            //       | BindingFlags.DeclaredOnly
            //     );

            //     foreach (PropertyInfo property in properties)
            //     {
            //         string key = type.ToString() + "." + property.Name;
            //         object value = property.GetValue(component, null);
            //         this.Set(key, value);
            //     }
            // }
            if(this.GetComponent<Rigidbody>() != null)
            {
                if(this.GetComponent<Rigidbody>().IsSleeping() == false)
                {
                    groundNormal = Vector3.Lerp(groundNormal, Physics.gravity.normalized, 0.1f);
                }
            }
        }

        void OnTriggerEnter(Collider other)
        {
            other.Trigger("TriggerEntered", this.gameObject);
        }

        void OnTriggerExit(Collider other)
        {
            other.Trigger("TriggerExited", this.gameObject);
        }

        void OnTriggerStay(Collider other)
        {
            other.Trigger("TriggerStayed", this.gameObject);
        }

        private void OnCollisionStay (Collision collision)
        {
            foreach( ContactPoint contact in collision.contacts )
            {
                //Register impact
                Impact impact = new Impact();

                impact.thisGameObject   = gameObject;
                impact.otherGameObject  = collision.gameObject;
                impact.relativeVelocity = collision.relativeVelocity;
                impact.normal           = contact.normal;
                impact.point            = contact.point;
                impact.thisCollider     = contact.thisCollider;
                impact.otherCollider    = contact.otherCollider;

                impacts[impact.otherGameObject] = impact;
                impacts[impact.otherCollider] = impact;
                if(collision.rigidbody) { impacts[collision.rigidbody] = impact; }

                //Update OnGround
                float oldDot = Vector3.Dot(groundNormal, Physics.gravity.normalized * -1);
                float newDot = Vector3.Dot(contact.normal, Physics.gravity.normalized * -1);
                if(newDot > oldDot)
                    groundNormal = contact.normal;
            }
        }

    // public methods
        public void Set(object key, object value)
        {
            this.dataView.Set(key, value);
        }

        public object Get(object key)
        {
            return this.dataView.Get(key);
        }

        public T Get<T>(object key)
        {
            object value = this.Get(key);
            if(value == null)
            { return default(T); }
            return (T)value;
        }

        public GameObject SetExclusiveChild(int id)
        {
            GameObject child = this.GetChildObject(id);
            return SetExclusiveChild(child);
        }

        public GameObject SetExclusiveChild(string name)
        {
            GameObject child = this.GetChildObject(name);
            return SetExclusiveChild(child);
        }

        public GameObject SetExclusiveChild(Component component)
        {
            GameObject child = this.GetChildObject(name);
            return SetExclusiveChild(child);
        }

        public GameObject SetExclusiveChild(GameObject child)
        {
            for(int index = 0; index < this.GetChildCount(); index++)
            {
                this.GetChildObject(index).SetActive( this.GetChildObject(index) == child );
            }
            return child;
        }

        public GameObject GetExclusiveChild()
        {
            for(int index = 0; index < this.GetChildCount(); index++)
            {
                GameObject child = this.GetChildObject(index).gameObject;
                if(child.activeSelf)
                {
                    return child;
                }
            }
            return null;
        }

        public int GetIDForChild(GameObject child)
        {
            for(int index = 0; index < this.GetChildCount(); index++)
            {
                if(this.GetChildObject(index) == child)
                {
                    return index;
                }
            }
            return -1;
        }

        public int GetChildCount()
        {
            return this.transform.childCount;
        }

        public bool IsOnGround() {
            float upLimit = ((1f-maxSlope/180)*2f-1f);
            float dot = Vector3.Dot(groundNormal, Physics.gravity.normalized * -1);
            bool isOnGround = dot > upLimit;

            return isOnGround;
        }

        public bool IsTouching(object other)
        {
            return impacts.ContainsKey(other);
        }

        public bool HasComponent<T>() where T : Component {
            return this.GetComponent<T>( ) != null;
        }

        public bool AssertHasComponent<T>() where T : Component {
            if(!this.HasComponent<T>())
            {
                string message = "";
                message += this.gameObject.name;
                message += " does not contain component ";
                message += typeof(T).ToString();
                Debug.Log(message);
            }
            return this.HasComponent<T>();
        }

        public bool HasComponentInParent<T>() where T : Component {
            return this.GetComponentInParent<T>( ) != null;
        }
        
        public bool AssertHasComponentInParent<T>() where T : Component {
            if(!this.HasComponentInParent<T>())
            {
                string message = "";
                message += this.gameObject.name;
                message += " parents do not contain component ";
                message += typeof(T).ToString();
                Debug.Log(message);
            }
            return this.HasComponentInParent<T>();
        }

        public bool HasComponentInChildren<T>() where T : Component {
            return this.GetComponentInChildren<T>( ) != null;
        }
        
        public bool AssertHasComponentInChildren<T>() where T : Component {
            if(!this.HasComponentInChildren<T>())
            {
                string message = "";
                message += this.gameObject.name;
                message += " children do not contain component ";
                message += typeof(T).ToString();
                Debug.Log(message);
            }
            return this.HasComponentInChildren<T>();
        }

        public void RemoveComponent<T>() where T : Component {
            if(!this.HasComponent<T>()) {return;}
            Destroy(this.GetComponent<T>( ));
        }

        // public T FindComponent<T>() where T : Component {
        //   if(!typeLookupTable.Has(T)) { typeLookupTable.Set(T, this.GetComponent<T>()); }
        //   return typeLookupTable.Get(T);
        // }

    // dispatcher methods
        public void Trigger(string eventName, object value)
        {
            // MESSAGE
            if(value.GetType() == typeof(Hashtable))
            {
                if(((Hashtable)value).Has("_auto"))
                {
                    value = ((Hashtable)value).Get("value");
                }
            }
            string unityMessageName = "On" +
                new List<string>(eventName.Split('.'))
                    .Map(delegate(int index, string element, List<string> list)
                    {
                        return element.UppercaseFirst();
                    }).Join();

            this.SendMessage( unityMessageName, value, SendMessageOptions.DontRequireReceiver );

            // EMIT
            Hashtable payload;
            if(value.GetType() == typeof(Hashtable))
            { payload = (Hashtable)value; }
            else
            { (payload = new Hashtable()).Set("value", value); }
            this.Emit( eventName, payload );
        }

        public void Trigger(string eventName)
        {
            this.Trigger(eventName, null);
        }

        public void Trigger(Hashtable eventData)
        {
            this.Trigger(eventData.Get<string>("event"), eventData);
        }

        public void Emit(string eventName, Hashtable payload)
        {
            this.dispatcher.Trigger(eventName, payload);
        }
        
        public void Emit(string eventName)
        {
            this.dispatcher.Trigger(eventName);
        }
        
        public void Emit(Hashtable eventData)
        {
            this.dispatcher.Trigger(eventData);
        }

        public DispatcherListener On(string eventName, Callback callback)
        {
            return this.dispatcher.On(eventName, callback);
        }
    }
}
