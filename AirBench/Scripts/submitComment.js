(function () {


    function verifyNotEmpty() {
        var button = document.getElementById("submit");
        var text = document.getElementById("Text").value.length;
        var rating = document.getElementById("Rating").value.length;


        if (text === 0 || rating === 0) {
            button.disabled = true;
        }
        else button.disabled = false;
    }

    document.getElementById("Text").addEventListener("keyup", verifyNotEmpty);
    document.getElementById("Rating").addEventListener("keyup", verifyNotEmpty);
})();