(function () {


    function verifyNotEmpty() {
        var button = document.getElementById("submit");
        var name = document.getElementById("Name").value.length;
        var seats = document.getElementById("Seats").value.length;
        var lon = document.getElementById("lon").value.length;
        var lat = document.getElementById("lat").value.length;
        var desc = document.getElementById("Description").value.length;

        if (name === 0 || seats === 0 || lon === 0 || lat === 0 || desc === 0) {
            button.disabled = true;
        }
        else button.disabled = false;
    }

    document.getElementById("Name").addEventListener("keyup", verifyNotEmpty);
    document.getElementById("Seats").addEventListener("keyup", verifyNotEmpty);
    document.getElementById("lon").addEventListener("keyup", verifyNotEmpty);
    document.getElementById("lat").addEventListener("keyup", verifyNotEmpty);
    document.getElementById("Description").addEventListener("keyup", verifyNotEmpty);
})();