


function Tracker(lib)
{
	this.lib = lib;
}

Tracker.prototype = {

	init:function(w , h)
	{
		this.videoW = w;
		this.videoH = h;


		this.cameraResolution	= new this.lib.Rectangle(   0,   0,  640, 480)	// Camera resolution
		this.brfResolution		= new this.lib.Rectangle(   0,   0,  640, 480)	// BRF BitmapData size
		this.brfRoi				= new this.lib.Rectangle(  80,  40,  480, 400)	// BRF region of interest within BRF BitmapData size
		this.faceDetectionRoi	= new this.lib.Rectangle( 160,  80,  320, 320)	// BRF face detection region of interest within BRF BitmapData size
		this.screenRect			= new this.lib.Rectangle(   0,   0,  640, 480)	// Shown video screen rectangle

	
	},
	initBRF:function() {
		if(this.brfManager == null) {
			this.brfManager = new this.lib.BRFManager({width: this.brfResolution.width, height: this.brfResolution.height}, this.brfRoi, {});
			this.brfManager.addEventListener("ready", function(event){this.onReadyBRF(event)}.bind(this));
		}
	},
	onReadyBRF:function(event) {

			// The following settings are completely optional.
			// BRF is by default set up to do the complete tracking
			// (including candide and its actionunits).
			this.brfManager.setFaceDetectionVars(5.0, 1.0, 14.0, 0.06, 6, false);
			this.brfManager.setFaceDetectionROI(
					this.faceDetectionRoi.x, this.faceDetectionRoi.y,
					this.faceDetectionRoi.width, this.faceDetectionRoi.height);
			this.brfManager.setFaceTrackingVars(80, 500, 1);

			// If you don't need 3d engine support or don't want to use
			// the candide vertices, you can turn that feature off, 
			// which saves CPU cycles.
			this.brfManager.candideEnabled = false;
			this.brfManager.candideActionUnitsEnabled = false;

			// Face Tracking? Face Tracking!
			this.brfManager.mode = this.lib.BRFMode.FACE_TRACKING;
			
			// Set BRF ready and start, if camera is ready, too.
			this.brfReady = true;
	},
	initCamera:function(caalBack) {
		this.camera = this.lib.Camera.getCamera("0", this.cameraResolution.width, this.cameraResolution.height);

		if(this.camera != null) {
			// Firefox currently doesn't support these contraints. 
			// In about:config search for media to set default_width and default_height
			// to get higher resolutions. It works in Chrome though.
			var constraints = {
				audio: false,
				video: 
				{
					mandatory: {
						minWidth: this.cameraResolution.width, 
						minHeight: this.cameraResolution.height
					},
					optional: [
					    { width: { max: this.cameraResolution.width }},
					    { height: { max: this.cameraResolution.height }},
					    { facingMode: "user" },
					    { minFrameRate: 30 }
					  ]
					}
			};
			
			var getUserMedia =
				window.navigator.getUserMedia ||
				window.navigator.mozGetUserMedia ||
				window.navigator.webkitGetUserMedia ||
				window.navigator.msGetUserMedia ||
				function(options, success, error) {
					error();
				};

			getUserMedia.call(window.navigator, constraints, function(stream){
				
				caalBack(this.camera);
				this.onCameraAvailable(stream);

			}.bind(this), this.onCameraUnavailable);
		}

		return this.camera != null;
	},
	onCameraAvailable:function(stream) {

		window.stream = stream; // stream available to console
		var url = window.URL || window.webkitURL;

		this.camera.src = url.createObjectURL(stream);
		this.camera.play();

		this.camera.addEventListener("playing", function () {
			
				setTimeout(function(){this.onStreamDimensionsAvailable()}.bind(this), 500);
		}.bind(this));
	},
	onStreamDimensionsAvailable:function() {
			
		if(this.camera.videoWidth == 0) {
			setTimeout(function(){this.onStreamDimensionsAvailable()}.bind(this), 500);
		} else {
			//init rest
			console.log("Stream dimensions: " +  this.camera.videoWidth + "x" + this.camera.videoHeight);
			
			// false: leave _screenRect as it was meant to be.
			// true: set _screenRect to _cameraResolution values.
			this.updateCameraResolution(this.camera.videoWidth, this.camera.videoHeight, true);

			// Set Caemra ready and start, if BRF is ready, too.
			// (which it won't, because initBRF is done afterwards ;))
			this.cameraReady = true;
			
			this.initBRF();
		}
	},
	updateCameraResolution:function(width, height, resizeScreenResolution) {
		this.resizeScreenResolution = this.lib.defaultValue(resizeScreenResolution, false);
		
		this.cameraResolution.width = width;
		this.cameraResolution.height = height;
		
		if(resizeScreenResolution) {
			this.screenRect.width = width;
			this.screenRect.height = height;
		}
	},
	onCameraUnavailable:function(stream) {
		console.log("unavailable");
	},
	updateInput:function() {
		this.videoBitmapData.drawImage(this.camera, 0, 0, this.cameraResolution.width, this.cameraResolution.height);
		this.brfBmd.drawImage(this.camera, 0, 0, this.cameraResolution.width, this.cameraResolution.height);
	},
	updateBRF:function(image) {

		this.brfManager.update(image);

		var faceShape = this.brfManager.faceShape;
		var state = this.brfManager.state;

		if(state == this.lib.BRFState.FACE_TRACKING_START || state == this.lib.BRFState.FACE_TRACKING)
		{
			return faceShape.points;
		}
		else
			return [];
	}
}

Tracker.prototype.videoW,
Tracker.prototype.videoH,
Tracker.prototype.resizeScreenResolution,
Tracker.prototype.cameraReady,
Tracker.prototype.cameraResolution,
Tracker.prototype.brfResolution,
Tracker.prototype.brfRoi,
Tracker.prototype.faceDetectionRoi,
Tracker.prototype.screenRect,
Tracker.prototype.maskContainer,
Tracker.prototype.webcamInput,
Tracker.prototype.brfReady,

Tracker.prototype.constructor = Tracker;

