config = require('./config');

var db = require('./db/setup.js')(function(){
  // This is what I use to serve data. its basicly the whole server. Necessary
  var express = require('express');

  // This is used to... uh... i forget.. I think its to parse json from the client. I'll keep it...
  var bodyParser = require('body-parser');

  // This sets up the express app. Its how I setup what happens when people access the server
  var app = express();

  // This sets an internal variable in express
  app.set('port', (process.env.PORT || 3000));

  // This tells express to use the bodyParser plugin
  app.use( bodyParser.json() );       // to support JSON-encoded bodies
  app.use(bodyParser.urlencoded({     // to support URL-encoded bodies
    extended: true
  }));

  var routes = require('./routes/setup')(app, db);

  // This makes the app begin listening now that the routes are all setup. this actually turns the server on.
  app.listen(app.get('port'), function() {
    console.log('Node app is running at localhost:' + app.get('port'));
  });
});