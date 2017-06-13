using UnityEngine;

using System.Collections;
using System.Collections.Generic;

using Ukulele;

namespace Ukulele
{
    public class UkuleleCore : Singleton<UkuleleCore>
    {
        private GameObject hovered;
        private Hashtable hitModelBuffer = new Hashtable();

        void Awake ()
        {
            UInput.Initialize();
        }

        void Start()
        {
            this.Emit("start");

            UInput.WithInstance().On("mouse_button", HandleGameObjectClicked);
        }

        void Update()
        { 
            this.Emit("update");

            UpdateHoveredGameObject();
        }

        private void HandleGameObjectClicked(Hashtable payload)
        {
            Hashtable combinedPayload = hitModelBuffer.Copy().Merge(payload);
            if(this.hovered != null){
                this.hovered.Emit(combinedPayload);
            }
        }

        private void UpdateHoveredGameObject()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo = new RaycastHit();
            GameObject subject = null;

            bool hit = Physics.Raycast(ray, out hitInfo);

            this.hitModelBuffer = new Hashtable();
            if(hit){
                hitModelBuffer.Set("oldGameObject", this.hovered);
                hitModelBuffer.Set("gameObject", hitInfo.transform.gameObject);

                hitModelBuffer.Set("barycentricCoordinate", hitInfo.barycentricCoordinate);
                hitModelBuffer.Set("collider", hitInfo.collider);
                hitModelBuffer.Set("distance", hitInfo.distance);
                hitModelBuffer.Set("lightmapCoord", hitInfo.lightmapCoord);
                hitModelBuffer.Set("normal", hitInfo.normal);
                hitModelBuffer.Set("point", hitInfo.point);
                hitModelBuffer.Set("rigidbody", hitInfo.rigidbody);
                hitModelBuffer.Set("textureCoord", hitInfo.textureCoord);
                hitModelBuffer.Set("textureCoord2", hitInfo.textureCoord2);
                hitModelBuffer.Set("transform", hitInfo.transform);
                hitModelBuffer.Set("triangleIndex", hitInfo.triangleIndex);
                
                subject = hitInfo.transform.gameObject;
            }

            if(this.hovered != subject){
                if(this.hovered != null){ this.hovered.Trigger("OnUnhover", hitModelBuffer); }
                if(subject != null){ subject.Trigger("OnHover", hitModelBuffer); }
                this.hovered = subject;
            }
        }

    // Event emitters
        void FixedUpdate()
        { this.Emit("FixedUpdate"); }
        void LateUpdate()
        { this.Emit("LateUpdate"); }

        void OnConnectedToServer()
        { this.Emit("OnConnectedToServer"); }
        void OnDisconnectedFromServer()
        { this.Emit("OnDisconnectedFromServer"); }
        void OnFailedToConnect()
        { this.Emit("OnFailedToConnect"); }
        void OnFailedToConnectToMasterServer()
        { this.Emit("OnFailedToConnectToMasterServer"); }
        void OnServerInitialized()
        { this.Emit("OnServerInitialized"); }
        void OnPlayerConnected()
        { this.Emit("OnPlayerConnected"); }
        void OnPlayerDisconnected()
        { this.Emit("OnPlayerDisconnected"); }

        void OnPreCull()
        { this.Emit("OnPreCull"); }
        void OnPreRender()
        { this.Emit("OnPreRender"); }
        void OnRenderObject()
        { this.Emit("OnRenderObject"); }
        void OnPostRender()
        { this.Emit("OnPostRender"); }
        void OnRenderImage(RenderTexture src, RenderTexture dest)
        { this.Emit("OnRenderImage"); }

        void OnGUI()
        {
            Event e = Event.current;
            do
            {
                this.Emit("gui_"+e.type);
            } while( Event.PopEvent(e) );
        }
    }
}
