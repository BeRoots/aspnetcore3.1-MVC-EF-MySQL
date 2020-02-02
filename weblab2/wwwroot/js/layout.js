jQuery(document).ready(function ($) {
    //active top navbar by toggle button navbar and top navbar
    $("#navbar-thumb").toggleClass('navbar-thumb-hidden')
    $(".navbar").toggleClass('navbar-hidden')
    
    // test browser support for compatibility and process GPDR and other requirements
    if (typeof(Storage) == "undefined") {
    	// browser doesn't support Storage.
    	displayNoBrowserStorageSupportLayer()
    }
    
    // test GDPR requirement
    if (getGDPRConsent() !== true) {
        	displayGdprLayer()
	}
})

//declarations
var lastScrollTop = 0;
var navbarThumbFramesMax = 16; //17 - 1
var navbarThumbStartLenght = $(".layer-navbar-thumb").css("width").length - 2; //1 for [0-9]
var navbarThumbWidth = parseInt($(".layer-navbar-thumb").css("width").substr(0, navbarThumbStartLenght));
$(".layer-page").scroll(function () {
    //toggle and animation controls
    if ($(this).scrollTop() == 0) {
        //if on top and not allready toggled
        if ($("#navbar-thumb").hasClass("navbar-thumb-hidden") == false) {
            $("#navbar-thumb").toggleClass('navbar-thumb-hidden');
            $(".navbar").toggleClass('navbar-hidden');
            //set currentScrollTop and background position to start position (reset)
            $(".layer-navbar-thumb").css("background-position-x", 0);
            lastScrollTop = 0;
        }
    }
    else {
        //if toggled by click
        if ($("#navbar-thumb").hasClass("navbar-thumb-hidden") == true) {
            $("#navbar-thumb").toggleClass('navbar-thumb-hidden');
            $(".navbar").toggleClass('navbar-hidden');
        }
        else {
        	if (window.document.documentMode) {
        		//alert("::::::::::::::::::IE::::::::::::::::::::")
        		var backgroundPositionX = 'backgroundPosition'
        	} else {
        		//alert("::::::::::::::::::NOT IE::::::::::::::::::::")
        		var backgroundPositionX = 'background-position-x'
        	}

        	var navbarThumbCurrentLenght = $(".layer-navbar-thumb").css(backgroundPositionX).length - 2;
            //if down
            if ($(this).scrollTop() > lastScrollTop) {
                //if position == max set 0 else set ++
                if (parseInt($(".layer-navbar-thumb").css(backgroundPositionX).substr(0, navbarThumbCurrentLenght)) == navbarThumbFramesMax * navbarThumbWidth) {
                    $(".layer-navbar-thumb").css("background-position-x", 0);
                }
                else {
                    $(".layer-navbar-thumb").css(
                        "background-position-x",
                        parseInt(parseInt($(".layer-navbar-thumb").css(backgroundPositionX).substr(0, navbarThumbCurrentLenght)) + navbarThumbWidth) + "px"
                    );
                }
            }
            else {
                // if position == Min set Max else set--
                if (parseInt($(".layer-navbar-thumb").css(backgroundPositionX).substr(0, navbarThumbCurrentLenght)) == 0) {
                    $(".layer-navbar-thumb").css(
                        "background-position-x",
                        navbarThumbFramesMax * navbarThumbWidth + "px"
                    );
                }
                else {
                    $(".layer-navbar-thumb").css(
                        "background-position-x",
                        parseInt(parseInt($(".layer-navbar-thumb").css(backgroundPositionX).substr(0, navbarThumbCurrentLenght)) - navbarThumbWidth) + 'px'
                    );
                }
            }
            lastScrollTop = $(this).scrollTop();
        }
    }
});

$("#navbar-thumb").click(function () {
    //reset rotation and reset currentScrollTop
    lastScrollTop = 0;
    //toggle to show navbar
    $("#navbar-thumb").toggleClass('navbar-thumb-hidden');
    $(".navbar").toggleClass('navbar-hidden');
});

function displayNoBrowserStorageSupportLayer() {
    //display the loading mask (hidden after data will be was updates)
    $("#storage-requirement").toggleClass('mask-box-hidden');
    $(".layer-dial-popup").toggleClass('mask-box-hidden');
}

function displayGdprLayer() {
    //display the loading mask (hidden after data will be was updates)
    $("#gdpr-requirement").toggleClass('mask-box-hidden');
    $(".layer-dial-popup").toggleClass('mask-box-hidden');
}

function validGDPRConsent() {
  var expDate = new Date();
  document.cookie = 'gdpr=weblab.consented.gdpr.cookie;path=/;expires='
    + expDate.setDate(expDate.getDate() + 365 * 5);
    + ';secure';
console.log('----------------------')
console.log('writing document.cookie')
console.log(document.cookie)
console.log('----------------------')

  // hide gdpr message and layer-dial-popup
  $("#gdpr-requirement").toggleClass('mask-box-hidden');
  $(".layer-dial-popup").toggleClass('mask-box-hidden');
  console.log("now is time to loading off ?")
  toggleLoadingLayerForTime();
}

function getGDPRConsent() {
	var result = false
	var ac = document.cookie.split(';').forEach(function (c) {
		if (c.split('=')[0] == 'gdpr' && c.split('=')[1] == 'weblab.consented.gdpr.cookie') {
			result = true
		}
	})
	return result
}

function toggleTermsOfUse() {
  $("#terms-of-use").toggleClass('mask-box-hidden');
  $("#gdpr-requirement").toggleClass('mask-box-hidden');
}

function toggleLoadingLayer() {
  $(".layer-loading").toggleClass('mask-box-hidden');
}

function toggleLoadingLayerForTime() {
  toggleLoadingLayer()
  setTimeout(function () {
    toggleLoadingLayer()
  }, 2000);
}
