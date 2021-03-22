(() => {
    let H = new Date().getHours();
    if (H >= 22 && H < 6) {
        $("#ScedualedRespond").text("Night");
    }
    else if (H >= 6 && H < 12) {
        $("#ScedualedRespond").text("Morning");
    }
    else {
        $("#ScedualedRespond").text("Afternoon");
    }
})()