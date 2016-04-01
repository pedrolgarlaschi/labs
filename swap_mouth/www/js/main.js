/*var tracker = new Tracker(nxtjs);
var video;
var capture;
var img;
var canvas;

var w = 640;
var h = 480;

function setup() {
  
  	
	canvas = createCanvas(window.innerWidth, window.innerHeight);

	loadVideo();	

}

function loadVideo()
{
	var xhttp = new XMLHttpRequest();
	xhttp.onreadystatechange=function() {

		initVideo(JSON.parse(xhttp.responseText).url);

	};

	xhttp.open("GET", "/video?id=_JHHfsRWOro", true);
	xhttp.send();
}

function initVideo(url)
{
	// specify multiple formats for different browsers
	video = createVideo([url]);
	video.size(w,h)
	video.hide();
	video.loop();
	video.volume(0);

	capture = createCapture(VIDEO);
	capture.hide();

	img = createImage(w,h)
	img.loadPixels();

	tracker.init(video.width , video.height);
	tracker.initBRF();
}

function draw()
{
	if(!video)return;

	background(0);

  	//image(video,w,0 , w, h);
  	video.loadPixels();

  	image(capture, 0, 0, w, h);
  	capture.loadPixels();

  	
  	
  	img.copy(video,0,0,video.width,video.height,0,0,w,h);
  	img.loadPixels();

  	image(img , w , 0)



  	if(tracker.brfReady);
    {
    	

    	if(capture.pixels[0] != 0  && capture.pixels.length > 100)
    	{
    		//drawFace(tracker.updateBRF(capture.pixels) , 0);	


    		console.log('!!!');

    		drawFace(tracker.updateBRF(img.pixels) , w);
    	}
    		
    		
	}
}

function drawFace(points , plusX)
{
	var len = points.length;

	console.log('len: ' , len);

	for (var i = 0; i < len; i++)
		ellipse(points[i].x + plusX, points[i].y, 3, 3);

}

*/



var video;
var capture;
var img;

var frameW = 640 * 0.5;
var frameH = 480 * 0.5;

var ctrack = new clm.tracker({useWebGL : true});
ctrack.init(pModel);

var ctrack2 = new clm.tracker({useWebGL : true});
ctrack2.init(pModel);

var canvas;

function setup() {
  
 
	canvas = createCanvas(window.innerWidth, window.innerHeight);
	img = createImage(frameW,frameH);
	img.loadPixels();

	maskImage = loadImage("images/gradient.png");

	loadVideo();	

}

function loadVideo()
{
	var xhttp = new XMLHttpRequest();
	xhttp.onreadystatechange=function() {

		initVideo(JSON.parse(xhttp.responseText).url);

	};

	xhttp.open("GET", "/video?id=_JHHfsRWOro", true);
	xhttp.send();
}

function initVideo(url)
{
	// specify multiple formats for different browsers
	video = createVideo([url]);
	video.size(frameW, frameH);
    video.position(0, 0);
	video.hide();
	video.loop();
	video.volume(0);

	capture = createCapture(VIDEO);
	capture.size(frameW, frameH);
    capture.position(0, 0);
	capture.hide();

	console.log('capture.elt: ' , capture.elt);
	console.log('capture.elt: ' , video.elt);

	ctrack.start(capture.elt);
	ctrack2.start(video.elt);
	
}


function draw()
{
	clear();

	if(!capture)return;
	if(!video)return;

    // get array of face marker positions [x, y] format
    var positions1 = ctrack.getCurrentPosition();
    var positions2 = ctrack2.getCurrentPosition();
    var len1 = positions1.length;
    var len2 = positions2.length;

    image(capture, 0, 0, frameW, frameH);
    image(video, frameW, 0, frameW, frameH);
    

    if(!positions1 || !positions2)
    	return;

    if(len1 == 0 || len2 == 0)
    	return;

    var w = (positions1[50][0] - positions1[44][0])
    var h = (positions1[53][1] - positions1[46][1])
    
    var x = positions1[44][0] - ((w * 0.6) / 2);
    var y = positions1[46][1] - ((h * 0.6) / 2);

    w *= 1.6;
    h *= 1.6;

	capture.loadPixels();

	img.copy(capture , x * 2,y * 2, w * 2,h * 2, 0,0,frameW,frameH);

	var scale = h;

	w = (positions2[50][0] - positions2[44][0]) * 4;
    h = (positions2[53][1] - positions2[46][1]) * 4;

    scale /= h;
    scale *= 0.3;

    if(scale <= 1)
    	scale = 1;
    
    x = positions2[60][0] - w / 2;
    y = positions2[46][1] - h / 2;


    img.mask(maskImage);
    image(img,x + frameW,y,w,h * scale  )


    for (var i=0; i<len1; i++) {

      fill(color(255, 255, 255, 200))
      ellipse(positions1[i][0], positions1[i][1], 3, 3);
    }

    for (var i=0; i<len2; i++) {
      //ellipse(positions2[i][0] + frameW, positions2[i][1], 1, 1);
    }

}

