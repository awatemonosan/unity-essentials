module.exports = (function(mongoose){
  schema = mongoose.Schema({
    _id: String,
    version: Number,
    name: String,
    data: String
  });

  model = mongoose.model('Voxel', schema);

  model.schema = schema;

  model.indicies = function(callback, error) {
    // this finds every voxel table in the database
    this.find(function(err, voxels){
      if (err) {
        console.error (err);
        if (error!==undefined) error (err)
        return;
      }

      ids = voxels.map( function(voxel){
        return voxel.id;
      });

      callback (ids);

    });
  };

  return model;
});
