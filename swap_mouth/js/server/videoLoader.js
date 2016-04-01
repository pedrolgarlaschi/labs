var fs = require('fs');
var youtubedl = require('youtube-dl');

function VideoLoader(){

}

VideoLoader.prototype  = {
	init:function()
	{
		
	},
	load:function(id , onLoad)
	{
		var file = 'videos/' +  id + '.mp4';

		console.log('load: ' , file);

		fs.exists(file, function(exists) {

			console.log('exists:  ' , exists)
  			
  			if(exists)
  			{
  				onLoad(id);
  				return;
  			}
  				

			//GBnxfTkICs
			var video = youtubedl('http://www.youtube.com/watch?v=' + id,['--format=18'],{ cwd: __dirname });
			video.on('info', function(info) {
			  console.log('Download started');
			  console.log('filename: ' + info.filename);
			  console.log('size: ' + info.size);
			  this.info = info;
			});

			video.on('data', function data(chunk) {
			  
			});

			video.on('end', function end() {
				onLoad(this.videoId);
			})
			video.videoId = id;
			video.pipe(fs.createWriteStream(file));

		});

		
	}
}

VideoLoader.prototype.constructor = VideoLoader;
module.exports = VideoLoader;
