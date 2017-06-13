using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

namespace Ukulele {
    public delegate void Callback(Hashtable payload);

    public class Dispatcher
    {
        private Dictionary<string, List<DispatcherListener> > callbackCatagories = new Dictionary<string, List<DispatcherListener> >( );
        
        private static int eventID = 0;

        private int lastEventID = -1;

        public static void Log(Hashtable payload)
        {
            Debug.Log(JSON.Serialize(payload));
        }

        public void Trigger(string eventName)
        {
            this.Trigger(eventName, new Hashtable());
        }

        public void Trigger(string eventName, Hashtable payload)
        {
            payload.Set("event", eventName);
            this.Trigger(payload);
        }

        public void Trigger(string eventName, object obj)
        {
            Hashtable payload = new Hashtable();
            payload.Set("event", eventName);
            payload.Set("value", obj);
            this.Trigger(payload);
        }

        public void Trigger(Hashtable payload) {
            if(!payload.Has("eventID")) {
                payload.Set("eventID", eventID);
                eventID++;
            } else if(this.lastEventID == payload.Get<int>("eventID")){ return; }
            
            this.lastEventID = payload.Get<int>("eventID");

            string eventName = payload.Get<string>("event");
            if(eventName == null) { return; }
            payload.Set("event", eventName = eventName.ToLower());

            // if(this.bindings.ContainsKey(eventName))
            //   Trigger(this.bindings[eventName]);

            // If all
            if(eventName=="all")
            {
                // retrigger all
                foreach(KeyValuePair<string, List<DispatcherListener>> entry in this.callbackCatagories)
                {
                    List<DispatcherListener> dispatcherListeners = entry.Value;
                    foreach(DispatcherListener dispatcherListener in dispatcherListeners)
                    {
                        dispatcherListener.Trigger(payload);
                    }
                }
            // Else
            }
            else
            {
                // Trigger event
                if(this.callbackCatagories.ContainsKey(eventName))
                {
                    foreach(DispatcherListener dispatcherListener in this.callbackCatagories[eventName])
                    {
                        dispatcherListener.Trigger(payload);
                    }
                }
                // Trigger "all"
                if(this.callbackCatagories.ContainsKey("all"))
                {
                    foreach(DispatcherListener dispatcherListener in this.callbackCatagories["all"])
                    {
                        dispatcherListener.Trigger(payload);
                    }
                }
            }

            // Recurse over parent callbackCatagories if '.' is present
            int lastPeriod = eventName.LastIndexOf('.');
            if(lastPeriod != -1)
            {
                eventName = eventName.Substring(0, lastPeriod);
                Hashtable newPayload = new Hashtable(payload);
                newPayload.Remove("eventID");
                this.Trigger(eventName, newPayload);
            }
        }

        public DispatcherListener On(string eventName, Callback callback)
        {
            return this.On(eventName, callback, new Hashtable());
        }

        public DispatcherListener On(string eventName, Callback callback, Hashtable payload)
        {
            eventName = eventName.ToLower();

            if (!this.callbackCatagories.ContainsKey(eventName))
            {
                this.callbackCatagories[eventName] = new List<DispatcherListener>();
            }

            DispatcherListener dispatcherListener = new DispatcherListener(callback, payload);
            this.callbackCatagories[eventName].Add(dispatcherListener);

            return dispatcherListener;
        }
    /*
        private int updateDispatcherListenerReference = -1;

        ~Dispatcher()
        {
            DisableTimedTriggers();
        }

        public void EnableTimedTriggers()
        {
            Debug.Log("Timed Triggers enabled");
            if(updateDispatcherListenerReference == -1)
                updateDispatcherListenerReference = Broadcaster.On("update", ProcessTimedTriggers);
        }

        public void DisableTimedTriggers()
        {
            if(updateDispatcherListenerReference >= 0)
            {
                Broadcaster.Off(updateDispatcherListenerReference);
                updateDispatcherListenerReference = -1;
            }
        }

        private Dictionary<long, ArrayList> timedTriggers = new Dictionary<long, ArrayList>( );
        public void ProcessTimedTriggers(Hashtable _)
        {
            long currentTime = DateTime.Now.Ticks;

            List<long> toTrigger = new List<long>();

            foreach(KeyValuePair<long, ArrayList> entry in timedTriggers)
            {
                if(currentTime > entry.Key)
                    toTrigger.Add(entry.Key);
            }

            foreach(long key in toTrigger)
            {
                foreach(Hashtable payload in timedTriggers[key])
                    Trigger(payload);

                timedTriggers.Remove(key);
            }
        }

        public void TriggerIn(Hashtable payload, float time)
        {
            TriggerIn(payload, (long)(time*1000));
        }
        public void TriggerAt(Hashtable payload, float time)
        {
            TriggerAt(payload, (long)(time*1000));
        }
        public void TriggerIn(Hashtable payload, long time)
        {
            TriggerAt(payload, DateTime.Now.Ticks+time);
        }
        public void TriggerAt(Hashtable payload, long time)
        {
            if(!timedTriggers.ContainsKey(time)) timedTriggers[time] = new ArrayList();

            timedTriggers[time].Add(payload);
        }

        public DispatcherListener Bridge(Dispatcher other)
        {
            return other.On("all", this.Trigger);
        }

        public void Off(int reference)
        {
            this.callbackCatagories[eventName].Remove(reference);
        }
    */
    }

    public class DispatcherListener
    {
        public Callback callback;
        public Hashtable defaultPayload;

        public DispatcherListener(Callback callback, Hashtable payload)
        {
            this.callback = callback;
            this.defaultPayload = payload;
        }

        public void Trigger(Hashtable payload)
        {
            if(this.callback != null)
            {
                Hashtable combinedPayload = this.defaultPayload.Copy().Merge(payload);
                this.callback(combinedPayload);
            }
        }

        public void Release()
        {
            this.callback = null;
        }
    }

}
