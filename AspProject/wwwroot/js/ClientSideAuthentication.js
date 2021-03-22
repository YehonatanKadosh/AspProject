
$("#Submit_New_User_Button").on("click", () => {
    if (($("#Password_messenger").text() === "Passwords does'nt match!" || $("#Username_messenger").text() === "Username already exists!")) {
        return false;
    }
});

$("#Password_Verification").on("change", () => {
    if (!($("#Password").val() === $("#Password_Verification").val())) {
        $("#Password_messenger")
            .removeClass("valid-feedback")
            .addClass("invalid-feedback")
            .text("Passwords does'nt match!")
            .fadeIn();
    }
    else {
        $("#Password_messenger")
            .removeClass("invalid-feedback")
            .addClass("valid-feedback")
            .text("Passwords match!")
            .fadeIn();
    }
});

$("#Password").on("change", () => {
    if (!($("#Password").val() === $("#Password_Verification").val())) {
        $("#Password_messenger")
            .removeClass("valid-feedback")
            .addClass("invalid-feedback")
            .text("Passwords does'nt match!")
            .fadeIn();
    }
    else {
        $("#Password_messenger")
            .removeClass("invalid-feedback")
            .addClass("valid-feedback")
            .text("Passwords match!")
            .fadeIn();
    }
});

$("#Username").on("change", async () => {
    await Promise.resolve($.post("/User/UsernameCheck", { Username: $("#Username").val() }))
        .then((Success) => {
            $("#Username_messenger")
                .removeClass("valid-feedback")
                .addClass("invalid-feedback")
                .text("Username already exists!")
                .fadeIn();
        })
        .catch((Error) => {
            $("#Username_messenger")
                .removeClass("invalid-feedback")
                .addClass("valid-feedback")
                .text("Username not used!")
                .fadeIn();
        });
});