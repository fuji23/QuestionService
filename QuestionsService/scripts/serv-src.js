
$(document).ready(function () {
    $("#menu").menu();

    //Get requsted amount of questions from service
    StartTesting = function () {
        $.ajax({
            type: 'GET',
            url: 'http://localhost:1950/service.svc/questions?id=5',
            dataType: 'json',
            success: function (json) {
                TestProcess(json);
            }
        });
    }

    //Start Testing
    TestProcess = function (json) {
        $("#accordion").html("");
        $.each(json, function (k, v) {
            var _arr = [];
            var id = JSON.stringify(v.Id);
            var quest = JSON.stringify(v.Content);
            var A = JSON.stringify(v.OptionA);
            var B = JSON.stringify(v.OptionB);
            var C = JSON.stringify(v.OptionC);
            //Shuffle questions
            _arr.push(A, B, C);
            var _final = _arr.slice();
            for (var i = 0; i < _final.length; i++) {
                var x = Math.floor(Math.random() * _arr.length - 1) + 1;
                _final[i] = _arr[x];
                _arr.splice(x, 1);
            }

            $("#accordion").append("<h3>Question " + (k + 1) + "</h3> " +
            "<div> <p id='qc" + id + "'>" + quest + "</p></div>");
            $.each(_final, function (ind, val) {
                $("#qc" + id).append("<p><input type='radio' name='var" + k + "' id=" + id + " " + "value=" + _final[ind] + ">" + _final[ind] + "<br></p>");
            });
        });

        $(document).ready(function () {
            $("#accordion").accordion();
            $("#start").css("display", "none");
            $("#end").css("display", "block");
        });
    };

    //Send results to server and receive a score value
    SendResults = function () {
        var name = prompt("Please enter your name");
        var jsn = [];

        $(function () {
            $('input:checked').each(function () {
                jsn.push({
                    Id: $(this).attr('id'),
                    Proper: $(this).attr('value')
                });
            });
        });

        $.ajax({
            type: "POST",
            url: "http://localhost:1950/service.svc/getresult?name=" + name,
            data: JSON.stringify(jsn),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                if ($('input:checked').length == 0) $(".ui-widget").css("display", "block");
            },
            success: function (call) {
                ProcessResult(call, name);
            }
        });
    };

    //Score box vizualization
    ProcessResult = function (call, name) {
        $("#accordion").accordion("destroy").html("");
        $("#start").css("display", "block");
        $("#end, .ui-widget").css("display", "none");
        $("#dialog").html("<p>" + name + " Your score is: " + call.Total + "</p>").dialog({
            dialogClass: "no-close",
            modal: true, title: "Dialog Title",
            resizable: false,
            buttons: [
            {
                text: "OK",
                click: function () {
                    window.location.replace(window.location.origin + "/Pages/Results.html");
                }
            }
            ]
        });
    };

    // Save question in the service database
    SaveQuestion = function (question) {
        $.ajax({
            type: "POST",
            url: "http://localhost:1950/service.svc/addquestion",
            data: JSON.stringify(question),
            contentType: "application/json; charset=utf-8",
            dataType: "json"
            //,
            //success: function () { $(".ui-widget").css("display", "block"); }
        });
    }

    //Gather data from the form
    formHandle = function (event) {
        event.preventDefault();
        var _question = {
            "Content": $("#Content").val(),
            "OptionA": $("#OptionA").val(),
            "OptionB": $("#OptionB").val(),
            "OptionC": $("#OptionC").val()
        };
        SaveQuestion(_question);
        $("#myFormId")[0].reset();
    }

    GetResults = function () {
        $.ajax({
            type: 'GET',
            url: 'http://localhost:1950/service.svc/allresults?id=15',
            dataType: 'json',
            success: function (json) {
                $.each(json, function (k, v) {
                    $("#resultstab").append("<tr><td>" + v.Recipient + "</td> <td>" + v.Date + "</td><td>" + v.Total + "</td></tr>");
                });
            }
        });
    }


    //AJAX Start-stop
    $(document).ajaxStart(function () {
        $("#ajaxloader").css("display", "block");
    });

    $(document).ajaxStop(function () {
        $("#ajaxloader").css("display", "none");
    });

});



