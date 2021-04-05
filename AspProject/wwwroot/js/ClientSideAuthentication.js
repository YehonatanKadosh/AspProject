$("#Password_Verification").on("change", () => {
    if (!($("#Password").val() === $("#Password_Verification").val())) {
        $("#Password_messenger")
            .removeClass("valid-feedback")
            .addClass("invalid-feedback")
            .text("Passwords does'nt match!")
            .fadeIn();
        $("#Submit_New_User_Button").prop("disabled", true);
    }
    else {
        $("#Password_messenger")
            .removeClass("invalid-feedback")
            .addClass("valid-feedback")
            .text("Passwords match!")
            .fadeIn();
        if ($("#Username_messenger").text() === "Valid username!") {
            $("#Submit_New_User_Button").prop("disabled", false);
        }
    }
});

$("#Password").on("change", () => {
    if (!($("#Password").val() === $("#Password_Verification").val())) {
        $("#Password_messenger")
            .removeClass("valid-feedback")
            .addClass("invalid-feedback")
            .text("Passwords does'nt match!")
            .fadeIn();
        $("#Submit_New_User_Button").prop("disabled", true);
    }
    else {
        $("#Password_messenger")
            .removeClass("invalid-feedback")
            .addClass("valid-feedback")
            .text("Passwords match!")
            .fadeIn();
        if ($("#Username_messenger").text() === "Valid username!") {
            $("#Submit_New_User_Button").prop("disabled", false);
        }
    }
});

$("#Username").on("change", async () => {
    await Promise.resolve($.get("/User/UsernameCheck", { Username: $("#Username").val() }))
        .then((Success) => {
            $("#Username_messenger")
                .removeClass("valid-feedback")
                .addClass("invalid-feedback")
                .text("Username exists!")
                .fadeIn();
            $("#Submit_New_User_Button").prop("disabled", true);
        })
        .catch((Error) => {
            $("#Username_messenger")
                .removeClass("invalid-feedback")
                .addClass("valid-feedback")
                .text("Valid username!")
                .fadeIn();
            if ($("#Password_messenger").text() === "Passwords match!") {
                $("#Submit_New_User_Button").prop("disabled", false);
            }
        });
});