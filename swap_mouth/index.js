var express = require('express');
var app = express();

app.use("/js", express.static(__dirname + '/js'));
app.use("/videos", express.static(__dirname + '/videos'));
app.use("/images", express.static(__dirname + '/www/images'));
app.use("/css", express.static(__dirname + 'www/css'));
app.use("/", express.static(__dirname + '/www'));

app.use(function(req, res, next) {
   res.header("Access-Control-Allow-Origin", "*");
   res.header('Access-Control-Allow-Methods', 'GET,PUT,POST,DELETE,OPTIONS');
   res.header("Access-Control-Allow-Headers", "X-Requested-With,Content-Type,Cache-Control");
   if (req.method === 'OPTIONS') {
    res.statusCode = 204;
    return res.end();
  } else {
    return next();
  }
});


var port = process.env.PORT || 3000;



var http = require('http').Server(app);
http.listen(port  , function(){
  console.log('http: ' , port);
});


var videoLoader = new (require(__dirname + '/js/server/videoLoader.js'))();
videoLoader.init();

var server = new (require(__dirname + '/js/server/server.js'))(app , videoLoader);
server.init();



