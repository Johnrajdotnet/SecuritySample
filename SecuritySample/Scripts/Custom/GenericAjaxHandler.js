//var token = $('[name=__RequestVerificationToken]').val();
//var headers = {};
//headers["__RequestVerificationToken"] = token;


JQAjax = function (httpType, linkUrl, postdata, onSucessFlag, dataflag, redirectToUrl) {
    // startProgress();
   
    var headers1 = { '__RequestVerificationToken': $("input[name='__RequestVerificationToken']").val() };
    $.ajax({
        type: httpType,
        url: linkUrl,
        cache: false,
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(postdata),
        headers: headers1,
        success: function (data) {
            onSucessEvent(data, onSucessFlag, flag, redirectToUrl);
           // stopProgress();
        },
        error: function (req, status, error) {
            onErrorHandler(req, status, error);
            //if (req != null && req.status != 401) { alert("Error occured on ajax call : " + req.status + " : " + req.statusText); stopProgress(); }
        }
    });
}

//=====================
JQAjaxStringifyInput = function (httpType, linkUrl, postdata, onSucessFlag, dataflag, redirectToUrl) {
    //startProgress();
   
    $.ajax({
        type: httpType,
        url: linkUrl,
        cache: false,
        contentType: 'application/json;charset=utf-8"',
        data: "{inputPara: '" + JSON.stringify(postdata) + "'}",
        success: function (data) {
            onSucessEvent(data, onSucessFlag, dataflag, redirectToUrl);
            //stopProgress();
        },
        error: function (req, status, error) {
            if (req != null && req.status != 401) { alert("Error occured on ajax call : " + req.status + " : " + req.statusText); stopProgress(); }
        }
    });

}

onSucessEvent = function (data, onSucessFlag, dataflag, redirectToUrl) {
    //alert(data); 
    debugger;
    switch (onSucessFlag) {
        case 'account': $(".container.body-content").html(data); break;
        case 'contact': $(".container.body-content").html(data); break;
    }
}

startProgress = function () {
    //$("#divLoading").removeClass('hide');
    //$("div#divLoading").addClass('show');
}
stopProgress = function () {
    //$("#divLoading").removeClass('show');
    //$("#divLoading").addClass('hide');
}

onErrorHandler = function (req, status, error) {
    //debugger;
    if (req != null && (req.status === 401 || req.status === 403)) {
        window.location.href = baseUrl + 'Home/Index';
    }
    else if (req != null) {
        alert("Error occured on ajax call : " + req.status + " : " + req.statusText);
    }
    else {
        alert("Error occured on ajax call : req object is null");
    }
    stopProgress();
}

cisAppSession = (function () {

    var SessionTime = 10000;
    var tickDuration = 1000;
    var myInterval = setInterval(function () {
        setupDialog();
        SessionTime = SessionTime - tickDuration;
        if (SessionTime <= 5000) {
            $("#lblcountDown").text(SessionTime);
        }
    }, 1000);

    var myTimeOut = setTimeout(sessionExpireEvent, SessionTime);

    //$("input").click(function () {
    //    clearTimeout(myTimeOut);
    //    SessionTime = 10000;
    //    myTimeOut = setTimeout(sessionExpireEvent, SessionTime);
    //});

    function sessionExpireEvent() {
        clearInterval(myInterval); alert("Session expired");
    };

    function  setupDialog() {
        var self = this;
        //self.destroyDialog();
        var settings = {
            timeout: 60,
            countdown: 30,
            title: 'Your session is about to expire!',
            message: 'You will be logged out in {0} seconds.',
            question: 'Do you want to stay signed in?',
            keep_alive_button_text: 'Yes, Keep me signed in',
            sign_out_button_text: 'No, Sign me out',
            keep_alive_url: '/keep-alive',
            logout_url: null,
            logout_redirect_url: '/',
            restart_on_yes: true,
            dialog_width: 350
        }

        //$('<div id="timeout-dialog">' +
        //    '<p id="timeout-message">' + settings.message.format('<span id="timeout-countdown">' + settings.countdown + '</span>') + '</p>' + 
        //    '<p id="timeout-question">' + settings.question + '</p>' +
        //  '</div>')
        //.appendTo('body')
        //.dialog({
        //    modal: true,
        //    width: settings.dialog_width,
        //    minHeight: 'auto',
        //    zIndex: 10000,
        //    closeOnEscape: false,
        //    draggable: false,
        //    resizable: false,
        //    dialogClass: 'timeout-dialog',
        //    title: settings.title,
        //    buttons : {
        //        'keep-alive-button' : { 
        //            text: settings.keep_alive_button_text,
        //            id: "timeout-keep-signin-btn",
        //            click: function() {
        //                alert('keep-alive')
        //            }
        //        },
        //        'sign-out-button' : {
        //            text: settings.sign_out_button_text,
        //            id: "timeout-sign-out-button",
        //            click: function() {
        //                alert('keep-out')
        //            }
        //        }
        //    }
        //});

       // self.startCountdown();
    }
    startCountdown= function() {
        var self = this,
            counter = settings.countdown;

        this.countdown = window.setInterval(function() {
            counter -= 1;
            $("#timeout-countdown").html(counter);

            if (counter <= 0) {
                window.clearInterval(self.countdown);
                self.signOut(false);
            }

        }, 1000);
    }
})