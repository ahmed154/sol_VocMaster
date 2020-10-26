function playSound(id) {
    var sound = document.getElementById(id);
    sound.play();
}
function FocusElement(id) {
    document.getElementById(id).focus();
} 

var MovieId;
var StartTime;
// 2. This code loads the IFrame Player API code asynchronously.
var tag = document.createElement('script');

tag.src = "https://www.youtube.com/iframe_api";
var firstScriptTag = document.getElementsByTagName('script')[0];
firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);

// 3. This function creates an <iframe> (and YouTube player)
//    after the API code downloads.
var player;
function onYouTubeIframeAPIReady(startTime) {
    player = new YT.Player('player', {
        height: '390',
        width: '640',
        videoId: '_hrguJlsZ90',
        playerVars: { start: startTime, controls:0, fs:0},
        events: {
            //'onReady': onPlayerReady,
            'onStateChange': onPlayerStateChange
        }
    });
}
function onYouTubeIframeAPIReady2() {
    player = new YT.Player('player', {
        height: '390',
        width: '640',
        playerVars: {controls: 0, fs: 0 },
        events: {
            //'onReady': onPlayerReady,
            'onStateChange': onPlayerStateChange
        }
    });
}

function setId(id, startTime) {

    StartTime = startTime;
    player = null;
    player = new YT.Player('player', {
        height: '390',
        width: '640',
        videoId: id,
        playerVars: { start: startTime },
        events: {
            'onReady': onPlayerReady,
            'onStateChange': onPlayerStateChange
        }
    });
}

// 4. The API will call this function when the video player is ready.
function onPlayerReady(event) {
    event.target.playVideo();
    seekTo(StartTime);
}

// 5. The API calls this function when the player's state changes.
//    The function indicates that when playing a video (state=1),
//    the player should play for six seconds and then stop.
var done = false;
var replay
function onPlayerStateChange(event)
{

    //    if (event !== null)
    //    {
    //        if (event.data !== null)
    //        {
    //            if (event.data == 1)
    //            {
    //                replay = setInterval(function ()
    //                {
    //                    console.log("playing ...");
    //                    if (player.getCurrentTime() >= 182 && player.getCurrentTime() < 187) {
    //                    }
    //                    else {
    //                        console.log("seek");
    //                        player.seekTo(182, true);
    //                    }
    //                }, 500);  
    //            }
    //            else {
    //                clearInterval(replay);
    //                player.playVideo();
    //            }
    //        }
    //        else {
    //            clearInterval(replay);
    //        }
    //    }
        
    //    else
    //    {
    //        clearInterval(replay);
    //    }
      

    //if (event.data == YT.PlayerState.PLAYING && !done) {
    //    setTimeout(stopVideo, 6000);
    //    done = true;
    //}
}


function playVideo() {
    player.playVideo();
}
function stopVideo() {
    player.stopVideo();
}
function seekTo(t) {
    if (YT.PlayerState.PLAYING) {
        player.seekTo(t, true);
    }
}
function getCurrentTime() {
     return player.getCurrentTime();
}




