﻿let alert = {
    DeleteConfirmation : function (callback) {
        Swal.fire({
            title: "Are you sure?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Yes, delete it!"
        }).then((result) => {
            if (result.isConfirmed) {
                callback(result);
            }
        });
    },

    Success : function (title, text) {
        Swal.fire({
            title: title,
            text: text,
            icon: "success"
        });
    },

    Error : function (title, text) {
        Swal.fire({
            title: title,
            text: text,
            icon: "error"
        });
    }
}