$(document).ready(function () {
    function createcalendar(events,defaultdate) {
        $('#calendar').fullCalendar({
            header: {
                left: 'prev,next today',
                center: 'title',
                right: 'month,basicWeek,basicDay'
            },
            defaultDate: defaultdate,
            navLinks: true, // can click day/week names to navigate views
            editable: true,
            eventLimit: true, // allow "more" link when too many events
            events: events
        });
    }
    let today = new Date()
    let todayString = today.getFullYear() + "-" + ("0" + (today.getMonth() + 1)).slice(-2) + "-" + ("0" + today.getDate()).slice(-2)
    fetch("/api/LiftScheduleApi").then(response => response.json()).then(data => createcalendar(data,todayString))
});