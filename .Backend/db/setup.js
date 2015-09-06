module.exports = function(start, error){
  db = {};

  db.config = config = require('./config');
  db.mongoose = mongoose = require('mongoose');

  db.set = function(table, id, payload){
    if(this[table] === undefined) return false;
    if(this[table].schema === undefined) return false;

    model = this[table].findById(id);
    if(model === undefined) {
      new this[table](payload).save();
    } else {
      model.update(payload);
    }
    
    return true;
  };

  db.get = function(table, id, callback){
    if(this[table] === undefined) return;
    if(this[table].schema === undefined) return;

    this[table].findById(id, callback);
  };

  mongoose.connection.once('open', start || console.log.bind(console, 'connection success:'));
  mongoose.connection.on('error', error || console.error.bind(console, 'connection error:'));

  db.voxel = require('./models/voxel')(mongoose);

  mongoose.connect('mongodb://localhost/demo');

  return db;
};