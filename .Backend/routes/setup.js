module.exports = function(app, db){
  routes = {}

  routes.api = require('./api')(app, db, "/api");

  return routes;
};
