﻿$(function () {
    $("#subBtn").click(function () {
        var userName = $("#userName").val();
        var password = $("#password").val();
        var IsTeacher = $("#isTeacher").is(":checked");
        $.post("/User/Register", { userName: userName, password: password, isTeacher: IsTeacher }, function (data) {
            if (data.isSucceed) {
               $('#myModal').modal();  
            }
        });
    });
});