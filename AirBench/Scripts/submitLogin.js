(function () {


    function verifyNotEmpty() {
        var button = document.getElementById("submit");
        var email = document.getElementById("Email").value.length;
        var pass = document.getElementById("Password").value.length;


        if (email === 0 || pass === 0 ) {
            button.disabled = true;
        }
        else button.disabled = false;
    }

    document.getElementById("Email").addEventListener("keyup", verifyNotEmpty);
    document.getElementById("Password").addEventListener("keyup", verifyNotEmpty);
})();