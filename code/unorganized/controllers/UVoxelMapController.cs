using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Ukulele;

namespace Ukulele
{
    public class UVoxelMapController
    {
        Hashtable data, voxelMap, chunkMap;
        public Dispatcher dispatcher = new Dispatcher();
        // emits:
        // voxel_updated
        // chunk_created
        // chunk_destroyed

        public UVoxelMapController(Hashtable data)
        {
            // this.data = data;

            // this.data.Default<int>("chunkSize", 5);

            // this.data.Default<int>("xMin", 0);
            // this.data.Default<int>("yMin", 0);
            // this.data.Default<int>("zMin", 0);

            // this.data.Default<int>("xMax", 0);
            // this.data.Default<int>("yMax", 0);
            // this.data.Default<int>("zMax", 0);

            // this.voxelMap = this.data.GetSub("voxelMap");
            // this.chunkMap = this.data.GetSub("chunkMap");
        }
        
        public int GetChunkSize()
        { return this.data.Get<int>("chunkSize"); }

        public Vector3 GetLowerBound()
        { return new Vector3(this.GetXMin(), this.GetYMin(), this.GetZMin()); }
        public Vector3 GetUpperBound()
        { return new Vector3(this.GetXMin(), this.GetYMin(), this.GetZMin()); }

        public int GetXMin()
        { return this.data.Get<int>("xMin"); }
        public int GetXMax()
        { return this.data.Get<int>("xMax"); }

        public int GetYMin()
        { return this.data.Get<int>("yMin"); }
        public int GetYMax()
        { return this.data.Get<int>("yMax"); }

        public int GetZMin()
        { return this.data.Get<int>("zMin"); }
        public int GetZMax()
        { return this.data.Get<int>("zMax"); }

        public bool HasAt(int x, int y, int z)
        {
            if (!this.voxelMap.Has(x))
            { return false; }
            if (!this.voxelMap.GetSub(x).Has(y))
            { return false; }
            if (!this.voxelMap.GetSub(x).GetSub(y).Has(z))
            { return false; }
            return true;
        }

        public void SetAt(Hashtable voxelData, int x, int y, int z)
        {
            this.voxelMap.GetSub(x).GetSub(y).GetSub(z).Merge(voxelData);

            if(x < this.GetXMin())
            { this.data.Set("xMin", x); }
            if(x > this.GetXMin())
            { this.data.Set("xMax", x); }

            if(y < this.GetYMin())
            { this.data.Set("yMin", y); }
            if(y > this.GetYMin())
            { this.data.Set("yMax", y); }

            if(z < this.GetZMin())
            { this.data.Set("zMin", z); }
            if(z > this.GetZMin())
            { this.data.Set("zMax", z); }

            int xChunk = this.ToChunk(x);
            int yChunk = this.ToChunk(y);
            int zChunk = this.ToChunk(z);

            bool chunkExists = true;
            if (!this.chunkMap.Has(xChunk))
            { chunkExists = false; }
            if (!this.chunkMap.GetSub(xChunk).Has(yChunk))
            { chunkExists = false; }
            if (!this.chunkMap.GetSub(xChunk).GetSub(yChunk).Has(zChunk))
            { chunkExists = false; }
            if (!chunkExists)
            {
                this.chunkMap.GetSub(xChunk).GetSub(yChunk).Set(zChunk, true);

                Hashtable chunkEventData = new Hashtable();
                    chunkEventData.Set("x", xChunk);
                    chunkEventData.Set("y", yChunk);
                    chunkEventData.Set("z", zChunk);

                this.dispatcher.Trigger("chunk_created", chunkEventData);
            }
        }

        private Hashtable CreateVoxelAt(int x, int y, int z){
            Hashtable voxelData = new Hashtable();
                voxelData.Set("fill", 0f);

            this.SetAt(voxelData, x, y, z);
            return voxelData;
        }

        public Hashtable WithAt(int x, int y, int z)
        {
            if(!this.HasAt(x, y, z))
            { return CreateVoxelAt(x, y, z); }
            return this.voxelMap.GetSub(x).GetSub(y).GetSub(z);
        }

        public Hashtable GetAt(int x, int y, int z)
        {
            if(!this.HasAt(x, y, z))
            { return new Hashtable(); }
            return this.voxelMap.GetSub(x).GetSub(y).GetSub(z).Copy();
        }

        private int ToChunk(int value)
        {
            return value/ this.data.Get<int>("chunkSize");;
        }
    }
}
