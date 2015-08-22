using UnityEngine;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Selector {
  private Dictionary<Vector3, bool> points = new Dictionary<Vector3, bool>( );

  public void Add(Vector3 point) {
    this.points[point] = true;
  }

  public void Remove(Vector3 point) {
    this.points.Remove(point);
  }
  
  public void Clear() {
    this.points = new Dictionary<Vector3, bool>( );
  }
  
  public List<Vector3> List() {
    List<Vector3> list = new List<Vector3>();
    
    foreach(KeyValuePair<Vector3, bool> entry in this.points){
      list.Add(entry.Key);
    }

    return list;
  }
}

public class VoxelModel : MonoBehaviour {
  Dictionary<Vector3, Voxel> voxels = new Dictionary<Vector3, Voxel>();
  public List< List<Color> > pallets = new List< List<Color> >();
  public int currentPallet = 0;

  public string log = "";
  public string fileName;
  public bool save=false;
  public bool load=false;

  byte[] GetBytes(string str) {
    byte[] bytes = new byte[str.Length * sizeof(char)];
    System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
    return bytes;
  }

  string GetString(byte[] bytes){
    char[] chars = new char[bytes.Length / sizeof(char)];
    System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
    return new string(chars);
  }

  public void Update() {
    if(save){
      File.WriteAllBytes( fileName, GetBytes(this.ToJSON().Print()) );
      save=false;
    }
    if(load){
      this.Load( GetString(File.ReadAllBytes(fileName)) );
      load=false;
    }
  }

  private void UpdateVisAt(Vector3 point) {
    Voxel voxel = this.Get(point);
    if(!voxel) return;

    int blockedSides = 0;

    blockedSides += this.Get(point+Vector3.up)      ? 1 : 0;
    blockedSides += this.Get(point+Vector3.down)    ? 1 : 0;
    blockedSides += this.Get(point+Vector3.forward) ? 1 : 0;
    blockedSides += this.Get(point+Vector3.back)    ? 1 : 0;
    blockedSides += this.Get(point+Vector3.left)    ? 1 : 0;
    blockedSides += this.Get(point+Vector3.right)   ? 1 : 0;
    
    voxel.gameObject.SetActive(blockedSides != 6);
  }

  private void UpdateVisAround(Vector3 point) {
    point = point.Floor();

    UpdateVisAt(point+Vector3.up);
    UpdateVisAt(point+Vector3.down);
    UpdateVisAt(point+Vector3.left);
    UpdateVisAt(point+Vector3.right);
    UpdateVisAt(point+Vector3.forward);
    UpdateVisAt(point+Vector3.back);

    UpdateVisAt(point);
  }

  private void UpdateAround(Vector3 point) {
    this.log = this.ToJSON().Print();

    UpdateVisAround(point);
  }
  public void Clear() {
    List<Vector3> list = new List<Vector3>();
    foreach(KeyValuePair<Vector3, Voxel> pair in this.voxels) {
      list.Add(pair.Key);
    }
    foreach(Vector3 point in list){
      this.Remove(point);
    }
  }
  public void Remove(Vector3 point){
    point = point.Floor();

    Voxel voxel = this.Get(point);
    if(!voxel) return;

    Destroy(voxel.gameObject);
    voxels.Remove(point);

    UpdateAround(point);
  }

  public void Remove(Selector selection){
    foreach(Vector3 point in selection.List()) {
      this.Remove(point);
    }
  }

  public Voxel Get(Vector3 point) {
    point = point.Floor();
    if(!voxels.ContainsKey(point)) return null;
    return voxels[point];
  }
  
  public void Set(Vector3 point, string prefabName){
    this.Set(point, prefabName, 1);
  }
  public void Set(Vector3 point, string prefabName, int colorIndex){
    this.Set(point, Resources.Load(prefabName) as GameObject, colorIndex);
  }

  public void Set(Vector3 point, GameObject prefab) {
    this.Set(point, prefab, 1);
  }
  public void Set(Vector3 point, GameObject prefab, int colorIndex) {
    point = point.Floor();

    this.Remove(point);
    
    Voxel voxel = voxels[point]   = Instantiate(prefab).GetComponent<Voxel>();

    voxel.gameObject.name = prefab.name;

    voxel.transform.parent        = transform;
    voxel.transform.localPosition = point;
    voxel.transform.localRotation = Quaternion.identity;

    this.UpdateAround(point);

    this.Set(point, colorIndex);
  }
  public void Set(Vector3 point, int colorIndex){
    point = point.Floor();

    this.Get(point).colorIndex = colorIndex;
  }

  public void Set(Selector selection, GameObject prefab) {
    this.Set(selection, prefab, 1);
  }
  public void Set(Selector selection, GameObject prefab, int colorIndex){
    foreach(Vector3 point in selection.List()) {
      this.Set(point, prefab, colorIndex);
    }
  }
  public void Set(Selector selection, int colorIndex){
    foreach(Vector3 point in selection.List()) {
      this.Set(point, colorIndex);
    }
  }

  public GameObject Detatch(Vector3 point) {
    point = point.Floor();

    Voxel voxel = this.Get(point);
    if(!voxel) return null;

    voxels.Remove(point);

    voxel.transform.parent = null;
    voxel.gameObject.AddComponent<Rigidbody>();

    UpdateVisAround(point);

    return voxel.gameObject;
  }

  public List<GameObject> Detatch(Selector selection){
    List<GameObject> list = new List<GameObject>();

    foreach(Vector3 point in selection.List()) {
      list.Add(this.Detatch(point));
    }

    return list;
  }
///////////////////////////////////////////////////////////////////////////////
  public JSONObject ToJSON(){
    return ToJSON(0);
  }
  public JSONObject ToJSON(version){
    switch(version){
      case 1:
        return ToJSON_V1();

      case 0:
      default:
        return ToJSON_V0();
    }
  }

  public void Load( string str ){
    this.Load( new JSONObject(str) );
  }
  public void Load( JSONObject json ){
    if(json.HasField("version")){
      switch (json.GetField("version")) {
        case 1:
          Load_V1(json);
          break;

        case 0:
        default:
          Load_V1(json);
      }
    } else {
      Load_V1(json);
    }
  }
///////////////////////////////////////////////////////////////////////////////
  public JSONObject ToJSON_V0(){
    JSONObject json = new JSONObject(JSONObject.Type.OBJECT);

    foreach(KeyValuePair<Vector3, Voxel> pair in this.voxels) {

      Vector3 point = pair.Key;
      Voxel voxel = pair.Value;

      string name = voxel.gameObject.name;
      int colorIndex = voxel.colorIndex;

      if(!json.HasField((int)point.x))
        json.SetField( (int)point.x, new JSONObject(JSONObject.Type.OBJECT) );
      
      JSONObject x = json.GetField((int)point.x);
      if(!x.HasField((int)point.y))
        x.SetField((int)point.y, new JSONObject(JSONObject.Type.OBJECT) );

      JSONObject y = x.GetField((int)point.y);
      if(!y.HasField((int)point.z))
        y.SetField( (int)point.z, new JSONObject(JSONObject.Type.OBJECT) );

        JSONObject jsonVoxel = y.GetField((int)point.z);

        jsonVoxel.SetField("name", name);
        jsonVoxel.SetField("color_index", colorIndex);
    }

    return json;
  }

  public void Load_V0( JSONObject jsonX ){
    this.Clear();

    for(int i = 0; i < jsonX.list.Count; i++){
      int x = int.Parse(jsonX.keys[i]);
      JSONObject jsonY = (JSONObject)jsonX.list[i];

      for(int j = 0; j < jsonY.list.Count; j++){
        int y = int.Parse(jsonY.keys[j]);
        JSONObject jsonZ = (JSONObject)jsonY.list[j];

        for(int k = 0; k < jsonZ.list.Count; k++){
          int z = int.Parse(jsonZ.keys[k]);
          JSONObject jsonVoxel = ((JSONObject)jsonZ.list[k]);

          string voxelName = jsonVoxel.GetField("name").str;
          index colorIndex = jsonVoxel.GetField("color_index").str;

          this.Set(new Vector3(x,y,z), voxelName, colorIndex);
        }
      }
    }
  }
///////////////////////////////////////////////////////////////////////////////
  public JSONObject ToJSON_V1(){
    // TODO: REFACTOR THIS FUNCTION TO UTILIZE PALLETS
    JSONObject json = new JSONObject(JSONObject.Type.OBJECT);

    foreach(KeyValuePair<Vector3, Voxel> pair in this.voxels) {

      Vector3 point = pair.Key;
      Voxel voxel = pair.Value;

      string name = voxel.gameObject.name;
      int colorIndex = voxel.colorIndex;

      if(!json.HasField((int)point.x))
        json.SetField( (int)point.x, new JSONObject(JSONObject.Type.OBJECT) );
      
      JSONObject x = json.GetField((int)point.x);
      if(!x.HasField((int)point.y))
        x.SetField((int)point.y, new JSONObject(JSONObject.Type.OBJECT) );

      JSONObject y = x.GetField((int)point.y);
      if(!y.HasField((int)point.z))
        y.SetField( (int)point.z, new JSONObject(JSONObject.Type.OBJECT) );

        JSONObject jsonVoxel = y.GetField((int)point.z);

        jsonVoxel.SetField("name", name);
        jsonVoxel.SetField("color_index", colorIndex);
    }

    return json;
  }

  public void Load_V1( JSONObject json ){
    this.Clear();

    JSONObject pallets = json.GetField("pallets");

    foreach(JSONObject pallet in pallets){
      int palletID = this.NewPallet();

      this.AddColor(palletID,new Color(1,1,1,0)); // 0: Transparent
      this.AddColor(palletID,new Color(1,1,1,1)); // 1: White
      this.AddColor(palletID,new Color(0,0,0,1)); // 2: Black

      foreach(JSONObject color in pallet){
        r = color.GetField("r").f;
        g = color.GetField("g").f;
        b = color.GetField("b").f;
        a = color.GetField("a").f;

        this.AddColor(palletID, new Color(r, g, b, a));
        // TODO: Consider a pallet/color inheritance system
      }
    }
    JSONObject jsonX = json.GetField("voxels");
    for(int i = 0; i < jsonX.list.Count; i++){
      int x = int.Parse(jsonX.keys[i]);
      JSONObject jsonY = (JSONObject)jsonX.list[i];

      for(int j = 0; j < jsonY.list.Count; j++){
        int y = int.Parse(jsonY.keys[j]);
        JSONObject jsonZ = (JSONObject)jsonY.list[j];

        for(int k = 0; k < jsonZ.list.Count; k++){
          int z = int.Parse(jsonZ.keys[k]);
          JSONObject jsonVoxel = ((JSONObject)jsonZ.list[k]);

          string voxelName = jsonVoxel.GetField("name").str;
          index colorIndex = jsonVoxel.GetField("color_index").str;

          this.Set(new Vector3(x,y,z), voxelName, colorIndex);
        }
      }
    }
  }
///////////////////////////////////////////////////////////////////////////////
}
