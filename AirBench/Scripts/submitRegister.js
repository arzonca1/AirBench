(function () {


    function verifyNotEmpty() {
        var button = document.getElementById("submit");
        var email = document.getElementById("Email").value.length;
        var pass = document.getElementById("Password").value.length;
        var fn = document.getElementById("FirstName").value.length;
        var ln = document.getElementById("LastName").value.length;

        if (email === 0 || pass === 0 || fn === 0 || ln === 0) {
            button.disabled = true;
        }
        else button.disabled = false;
    }

    document.getElementById("Email").addEventListener("keyup", verifyNotEmpty);
    document.getElementById("Password").addEventListener("keyup", verifyNotEmpty);
    document.getElementById("FirstName").addEventListener("keyup", verifyNotEmpty);
    document.getElementById("LastName").addEventListener("keyup", verifyNotEmpty);
    console.log("seems to have run properly");
})();