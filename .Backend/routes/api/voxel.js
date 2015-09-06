module.exports = function(app, db, path){
  routes = {}

  routes.index = app.get(path+'/', function(req, res) {
    db.voxel.indicies(function(result){
      res.json(result);
    });
  });

  app.param('id', function(req, res, next, id) {
    req.id = id;
    next();
  });

  routes.index = app.get(path+'/:id', function(req, res) {
    // TODO: Abstrat this into voxel controller
    db.voxel.findById(req.id, function(err, voxel) {
      if(err || voxel===undefined)
        res.json({success:"false"});
      else {
        res.json(voxel);
      }
    });
  });

  routes.index = app.post(path+'/:id', function(req, res) {
    db.set("voxel",req.id,{
      _id: req.id.toString(),
      name: req.body.name,
      data: req.body.data
    },function(err){
      if(err) res.json({err:err});
      else res.json({});
    });
  });

  return routes;
}
