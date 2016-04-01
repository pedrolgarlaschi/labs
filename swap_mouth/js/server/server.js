var path = require('path');
var id = 0;
var bodyParser = require('body-parser');


function Server(app , videoLoader){
	this.app = app;
	this.app.use( bodyParser.json() ); 
	this.videoLoader = videoLoader;
}

Server.prototype  = {
	getRouts : [
		{name:'/' , handler:'indexHandler'},
		{name:'/video' , handler:'videoHandler'},
	],
	handlers : {
		indexHandler:function(req , res){
			res.sendFile(path.resolve('./www/index.html'));
		},
		videoHandler:function(req , res){

			var id = req.query.id

			this.videoLoader.load(id , function(data){

				//console.log('retur: ' , data);

				//var url = path.resolve('videos/' + data + '.mp4');

				if(data)
					res.send(JSON.stringify({url:'videos/' + data + '.mp4'}));

			})
		}
	},
	init:function()
	{
		this.getRouts.forEach(function(r) {
             this.app.get(r.name , this.handlers[r.handler].bind(this));
        }, this);
	}
}
Server.prototype.constructor = Server;
module.exports = Server;