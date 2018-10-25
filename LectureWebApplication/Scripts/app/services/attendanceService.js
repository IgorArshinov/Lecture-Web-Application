var AttendanceService = function() {
    var createAttendance = function(lectureId, done, fail) {
        $.post("/api/attendances", { lectureId: lectureId })
            .done(done)
            .fail(fail);
    };
    var deleteAttendance = function(lectureId, done, fail) {
        $.ajax({
                url: "/api/attendances/" + lectureId,
                method: "DELETE"
            })
            .done(done)
            .fail(fail);
    };
    return {
        createAttendance: createAttendance,
        deleteAttendance: deleteAttendance
    };
}();