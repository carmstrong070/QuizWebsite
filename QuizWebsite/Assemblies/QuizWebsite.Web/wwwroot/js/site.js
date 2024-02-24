//const { Alert } = require("../lib/bootstrap/dist/js/bootstrap.bundle"); //-- I commented it out because some stuff broke for me - JB
var clockInterval;
var clockStartTime;
var $clock;

function startClock(elementSelector) {
    $clock = $("#" + elementSelector);

    if ($clock.length === 0)
        return;

    clockStartTime = new Date();

    clockInterval = setInterval(function () {
        let now = Date.now();
        let delta = now - clockStartTime;
        $clock.text(delta / 1000);
    }, 33.33);
}