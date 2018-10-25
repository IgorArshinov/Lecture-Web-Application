
var LecturesController = function(attendanceService) {
    var button;
    var toggleAttendance;
    var init = function(container) {
        $(container)
            .on("click", ".js-toggle-attendance", toggleAttendance);
    };
    var fail;
    var done;
    toggleAttendance = function(e) {
        button = $(e.target);
        var lectureId = button.attr("data-lecture-id");
        if (button.hasClass("btn-default"))
            attendanceService.createAttendance(lectureId, done, fail);
        else
            attendanceService.deleteAttendance(lectureId, done, fail);
    };
    done = function() {
        var text = button.text() === "Attending" ? "Enroll" : "Attending";
        button.toggleClass("btn-info")
            .toggleClass("btn-default")
            .text(text);
    };
    fail = function() {
        alert("Something failed");
    };
    return {
        init: init
    };
}(AttendanceService);