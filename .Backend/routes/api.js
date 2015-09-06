module.exports = function(app, db, path){
  routes = {};

  routes.voxel = require('./api/voxel')(app, db, path+"/voxel");
  
  return routes;
};
